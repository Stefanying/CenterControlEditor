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
using System.Windows.Shapes;
using System.Text.RegularExpressions;

namespace CenterControlEditor.Controls
{
    /// <summary>
    /// IPSetting.xaml 的交互逻辑
    /// </summary>
    public partial class IPSetting : Window
    {
        //IP
        string _ip;
        public string Ip
        {
            get { return _ip; }
            set { _ip = value; tbIP.Text = value; }
        }

        //端口
        int _port;
        public int Port
        {
            get { return _port; }
            set { _port = value; tbPort.Text = value.ToString(); }
        }


        public IPSetting()
        {
            Console.Write(_ip);
            Console.Write(_port);
            InitializeComponent();
        }

        private bool IsIPString(string ipadress)
        {
            string pattrn = @"^((2[0-4]\d|25[0-5]|[01]?\d\d?)\.){3}(2[0-4]\d|25[0-5]|[01]?\d\d?)$";
            Regex rex = new Regex(pattrn);
            return rex.IsMatch(ipadress);
        }

        private void btnOk_Click(object sender, MouseButtonEventArgs e)
        {
            //判断IP格式是否正确
            try
            {
                _ip = tbIP.Text;
                if (IsIPString(_ip))
                {
                    _port = int.Parse(tbPort.Text);
                    DialogResult = true;
                }
            }
            catch
            {
 
            }
        }

        private void Cancel_MouseDown(object sender, MouseButtonEventArgs e)
        {
            DialogResult = false;
        }

        private void SetCancel(object sender, MouseButtonEventArgs e)
        {
            DialogResult = false;
        }

        /// <summary>
        /// 移动窗口
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void MoveThis(object sender, MouseEventArgs e)
        {
            if (e.LeftButton == MouseButtonState.Pressed)
            {
                this.DragMove();
            }
        }

        


    }
}
