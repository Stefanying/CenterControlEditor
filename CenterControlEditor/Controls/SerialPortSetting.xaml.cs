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
using CenterControlEditor.Business;
using System.Text.RegularExpressions;

namespace CenterControlEditor.Controls
{
    /// <summary>
    /// SerialPortSetting.xaml 的交互逻辑
    /// </summary>
    public partial class SerialPortSetting : Window
    {
        string _comNumber;
        public string ComNumber
        {
            get { return _comNumber; }
            set { _comNumber = value; cbComNumber.Text=value; }
        }

        int _baudRate;
        public int BaudRate
        {
            get { return _baudRate; }
            set { _baudRate = value; cbBaudrate.Text=value.ToString(); }
        }

        int _dataBit;
        public int DataBit
        {
            get { return _dataBit; }
            set { _dataBit = value; cbDatabit.Text = value.ToString(); }
        }

        int _stopBit;
        public int StopBit
        {
            get { return _stopBit; }
            set { _stopBit = value; cbStopbit.Text = value.ToString(); }
        }

        Parity _parity;
        public Parity Parity
        {
            get { return _parity; }
            set { _parity = value; cbParity.SelectedIndex = (int)value; }
        }

        public SerialPortSetting()
        {
            InitializeComponent();
            cbComNumber.Text = "COM1";
            cbBaudrate.Text = "9600";
            cbDatabit.Text = "8";
            cbStopbit.Text = "1";
            cbParity.SelectedIndex = 2;
        }

        private bool IsCom(string comNumber)
        {
            Regex rex = new Regex(@"com\d+$");
            return rex.IsMatch(comNumber);
        }

        private void btnOK_Click(object sender, MouseButtonEventArgs e)
        {
            _comNumber = cbComNumber.Text;
            _baudRate = int.Parse(cbBaudrate.Text);
            _dataBit = int.Parse(cbDatabit.Text);
            _stopBit = int.Parse(cbStopbit.Text);
            _parity = (Parity)cbParity.SelectedIndex;
            DialogResult = true;
        }

        private void btnCancel_Click(object sender, MouseButtonEventArgs e)
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
