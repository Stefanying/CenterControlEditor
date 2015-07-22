using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net;
using System.Net.Sockets;
using System.Net.NetworkInformation;
using System.Threading;
using System.Text.RegularExpressions;
using System.Diagnostics;
using Microsoft.Win32;
using System.Runtime.InteropServices;

namespace SFLib
{
    public struct Ip
    {
        public string Address;
        public string SubnetMask;
    }

    /// <summary>
    /// 网络相关的一些常用函数
    /// </summary>
   public  class NetLib
    {
       public static string GetLocalIpString()
       {
           string result = null;
           NetworkInterface[] nics = NetworkInterface.GetAllNetworkInterfaces();
           foreach (NetworkInterface adapter in nics)
           {
               //从有线或无线网络连接中选则第一个找到的网络适配器
               if ((adapter.NetworkInterfaceType == NetworkInterfaceType.Ethernet) || (adapter.NetworkInterfaceType == NetworkInterfaceType.Wireless80211))
               {
                   //过滤掉loopback连接
                   if (adapter.Description == "Microsoft Loopback Adapter")
                   {
                       continue;
                   }
                   //过滤掉蓝牙连接和vmware虚拟网卡连接
                   if (adapter.Name.ToUpper().Contains("BLUETOOTH") || adapter.Name.ToUpper().Contains("VMWARE"))
                   {
                       continue;
                   }
                   result = GetIpv4StringFromNetworkAdapter(result, adapter);
               }
           }
           return result;

       }

       private static string GetIpv4StringFromNetworkAdapter(string result, NetworkInterface adapter)
       {
           if (adapter == null) return null;
           foreach (UnicastIPAddressInformation ip in adapter.GetIPProperties().UnicastAddresses)
           {
               if (ip.Address.AddressFamily == AddressFamily.InterNetwork)
               {
                   result = ip.Address.ToString();
               }
           }
           return result;
       }

       public static string HttpRequest(string reuqestURL)
       {
           try
           {
               using (WebClient wc = new WebClient())
               {
                   wc.Encoding = Encoding.UTF8;
                   return wc.DownloadString(reuqestURL);
               }
           }
           catch (Exception ex)
           {
               return "";//表示执行失败
           }
       }

       public static bool BroadcastUdpData(int targetPort, byte[] udpData)
       {
           try
           {
               UdpClient udpClient = new UdpClient();
               IPEndPoint iep = new IPEndPoint(IPAddress.Broadcast, targetPort);
               udpClient.EnableBroadcast = true;
               udpClient.Send(udpData, udpData.Length, iep);
               Thread.Sleep(100);
               udpClient.Close();
               udpClient = null;
               iep = null;
               return true;
           }
           catch
           {
               return false;
           }
       }

       /// <summary>
       /// 发送UDP数据
       /// </summary>
       /// <param name="targetIP"></param>
       /// <param name="targetPort"></param>
       /// <param name="udpData"></param>
       /// <returns></returns>
       public static bool SendUdpData(string targetIP, int targetPort, byte[] udpData)
       {
           UdpClient udpClient = new UdpClient();
           try
           {
               udpClient.Connect(new IPEndPoint(IPAddress.Parse(targetIP), targetPort));
               udpClient.Send(udpData, udpData.Length);
               udpClient.Close();
               return true;
           }
           catch
           {
               Logger.Info("发送UDP数据失败!");
               return false;
           }
       }


      /// <summary>
      /// 发送Tcp数据
      /// </summary>
      /// <param name="targetIP"></param>
      /// <param name="targetPort"></param>
      /// <param name="tcpData"></param>
      /// <returns></returns>
       public static bool SendTcpData(string targetIP, int targetPort, byte[] tcpData)
       {
           TcpClient tcpClient = new TcpClient();
           try
           {
               tcpClient = new TcpClientWithTimeout(targetIP, targetPort, 2000).Connect();
               tcpClient.SendTimeout = 10 * 1000;//超时时间10秒
               tcpClient.ReceiveTimeout = 10 * 1000;//超时时间10秒
               NetworkStream stream = tcpClient.GetStream();
               stream.Write(tcpData, 0, tcpData.Length);
               Thread.Sleep(10);
               stream.Close();
               stream.Dispose();
               tcpClient.Close();
               GC.Collect();
           }
           catch
           {
               Logger.Info("发送TCP数据失败！");
               if (tcpClient != null)
               {
                   tcpClient.GetStream().Dispose();
                   tcpClient.Close();
               }
               return false;
           }

           return true;
       }

       public static bool CheckSum(byte[] bytes)
       {
           if (bytes == null || bytes.Length < 4)
           {
               return false;
           }

           byte sum = bytes[0];
           Int16 len = BitConverter.ToInt16(bytes, 1);

           if (len != bytes.Length)
           {
               return false;
           }

           byte[] buffer = new byte[bytes.Length - 3];
           for (int i = 0; i < buffer.Length; i++)
           {
               buffer[i] = bytes[i + 3];
           }

           if (sum != (byte)(buffer.Sum(x => x) % 256))
           {
               return false;
           }

           return true;
       }

       public static byte[] GetEncryBytes(string str)
       {
           byte[] urlBuffer = Encoding.UTF8.GetBytes(str);
           byte[] sum = new byte[] { (byte)(urlBuffer.Sum(x => x) % 256) };
           Int16 len = (Int16)(urlBuffer.Length + 1 + 2);
           byte[] lenBuffer = BitConverter.GetBytes(len);
           return sum.Concat(lenBuffer).Concat(urlBuffer).ToArray();
       }
    }

   public class TcpClientWithTimeout
   {
       protected string _hostname;
       protected int _port;
       protected int _timeout_milliseconds;
       protected TcpClient connection;
       protected bool connected;

       public TcpClientWithTimeout(string hostname, int port, int timeout_milliseconds)
       {
           _hostname = hostname;
           _port = port;
           _timeout_milliseconds = timeout_milliseconds;
       }

       public TcpClient Connect()
       {
           // kick off the thread that tries to connect
           connected = false;
           Thread thread = new Thread(new ThreadStart(BeginConnect));
           thread.IsBackground = true; // So that a failed connection attempt
           // wont prevent the process from terminating while it does the long timeout
           thread.Start();

           // wait for either the timeout or the thread to finish
           thread.Join(_timeout_milliseconds);

           if (connected == true)
           {
               // it succeeded, so return the connection
               thread.Abort();
           }
           else
           {
               connection = null;
           }
           return connection;
       }

       protected void BeginConnect()
       {
           try
           {
               connection = new TcpClient(_hostname, _port);
               // record that it succeeded, for the main thread to return to the caller
               connected = true;
           }
           catch
           {
               connected = false;
           }
       }

    }
}
