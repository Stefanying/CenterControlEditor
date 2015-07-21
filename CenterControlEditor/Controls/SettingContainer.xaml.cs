using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Net;
using System.Net.Sockets;

namespace CenterControlEditor.Controls
{
    /// <summary>
    /// SettingContainer.xaml 的交互逻辑
    /// </summary>
    public partial class SettingContainer : UserControl
    {

        TcpClient _clinet;
        string _hostname;
        int _port = 10003;
        int _blockLength = 1024;
        
        public SettingContainer()
        {
            InitializeComponent();
            getIP();
        }
        /// <summary>
        /// 获取服务器IP
        /// </summary>
        private void getIP()
        {
           string ip=Utility.Data.GetInstance().GetIP();
           tbIP.Text = ip;
        }

        private void btnGetLockDogState_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                Start();
                Connect();
                if (_clinet.Connected)
                {
                    NetworkStream ns = _clinet.GetStream();
                    string command = "GetLockState";

                    Byte[] sendBytes = Encoding.Default.GetBytes(command);
                    ns.Write(sendBytes, 0, sendBytes.Length);

                    byte[] receiveBuffer = new byte[_blockLength];
                    int readLength = ns.Read(receiveBuffer, 0, _blockLength);

                    lbLockDogState.Text = System.Text.Encoding.UTF8.GetString(receiveBuffer, 0, readLength);
                    ns.Close();
                }
            }
            catch (Exception ex)
            {
                Console.Write("加密错误！");
            }
        }

        private void Connect()
        {
            try
            {
                _hostname = tbIP.Text;
                _clinet.Connect(IPAddress.Parse(_hostname), _port);
                Utility.Data.GetInstance().SaveIP(_hostname);
            }
            catch (Exception ex)
            {
                Console.Write("连接失败");
            }
        }

        private void Start()
        {
            _clinet = new TcpClient();
            _clinet.ReceiveTimeout = 1000 * 10;
        }
    }
}
