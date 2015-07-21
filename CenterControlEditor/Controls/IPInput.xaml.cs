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

namespace CenterControlEditor.Controls
{
    /// <summary>
    /// IPInput.xaml 的交互逻辑
    /// </summary>
    public partial class IPInput : UserControl
    {
        public IPInput()
        {
            InitializeComponent();
            //处理粘贴
            DataObject.AddPastingHandler(this, new DataObjectPastingEventHandler(IPTextBox_Paste));
            // 设置textBox次序
            ipTextBox1.setNeighbour(null, ipTextBox2);
            ipTextBox2.setNeighbour(ipTextBox1, ipTextBox3);
            ipTextBox3.setNeighbour(ipTextBox2, ipTextBox4);
            ipTextBox4.setNeighbour(ipTextBox3, null);
        }


        private void IPTextBox_Paste(object sender, DataObjectPastingEventArgs e)
        {

            if (e.DataObject.GetDataPresent(typeof(string)))
            {
                string value = e.DataObject.GetData(typeof(string)).ToString();
                setIP(value);
            }

            e.CancelCommand();
        }


        //设置ip
        private bool setIP(string strIP)
        {
            string[] ips = strIP.Split('.');
            if (ips.Length == 4)
            {
                int res;
                for (int i = 0; i < ips.Length; ++i)
                    if (!Int32.TryParse(ips[i], out res) || res > 255 || res < 0)
                    {
                        return false;
                    }
                ipTextBox1.Text = ips[0];
                ipTextBox2.Text = ips[1];
                ipTextBox3.Text = ips[2];
                ipTextBox4.Text = ips[3];
                return true;
            }
            return false;
        }

    }
}
