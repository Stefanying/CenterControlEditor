using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Forms;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace CenterControlEditor.Controls
{
    /// <summary>
    /// OprationSetting.xaml 的交互逻辑
    /// </summary>
    public partial class OprationSetting : Window
    {
        public OprationSetting()
        {
            InitializeComponent();
        }

        object _setting;
        public object Setting
        {
            get { return _setting; }
            set { _setting = value; }
        }
    
        //名称
        private string _oprationName;
        public string OprationName
        {
            get { return _oprationName; }
            set { _oprationName = value; tbName.Text = value; }
        }

        //通信方式
       private string _oprationType;
        public string OprationType
        {
            get { return _oprationType; }
            set { _oprationType = value; cbOprationType.Text = value; }
        }

        //数据类型
       private string _dataType;
        public string DataType
        {
            get { return _dataType; }
            set { _dataType = value; cbDataType.Text = value; }
        }

        //数据内容
       private string _data;
        public string Data
        {
            get { return _data; }
            set { _data = value; tbData.TextValue = value; }
        }


      private  int _time;
        public int Time
        {
            get { return _time; }
            set { _time = value; tbTime.Text = value.ToString(); }
        }

        private void btnSetting_Click(object sender, MouseButtonEventArgs e)
        {
            if (cbOprationType.Text == "TCP" || cbOprationType.Text == "UDP")
            {
                IPSetting settingForm = new IPSetting();
                if (Setting != null && (Setting as Business.NetworkSetting) != null)
                {
                    Business.NetworkSetting ns = (Business.NetworkSetting)Setting;
                    settingForm.Ip = ns.Ip;
                    settingForm.Port = ns.Port;
                }

                if (settingForm.ShowDialog() == true)
                {
                    Business.NetworkSetting ns = new Business.NetworkSetting();
                    ns.Ip = settingForm.Ip;
                    ns.Port = settingForm.Port;

                    Setting = ns;
                }
            }
            else if (cbOprationType.Text == "串口")
            {
                SerialPortSetting settingForm = new SerialPortSetting();
                if (Setting  != null && (Setting as Business.ComSetting) != null)
                {
                    Business.ComSetting cs = (Business.ComSetting)Setting;
                    settingForm.ComNumber = cs.ComNumber;
                    settingForm.BaudRate = cs.BaudRate;
                    settingForm.DataBit = cs.DataBits;
                    settingForm.StopBit = cs.StopBits;
                    settingForm.Parity = cs.Parity;
                }
                if (settingForm.ShowDialog() == true)
                {
                    Business.ComSetting cs = new Business.ComSetting();
                    cs.ComNumber = settingForm.ComNumber;
                    cs.BaudRate = settingForm.BaudRate;
                    cs.StopBits = settingForm.StopBit;
                    cs.DataBits = settingForm.DataBit;
                    cs.Parity = settingForm.Parity;

                    Setting = cs;
                }

            }
        }

        private void btnOk_Click(object sender, MouseButtonEventArgs e)
        {

            try
            {
                if (Setting == null)
                {
 
                }
                _oprationName = tbName.Text;
                _oprationType = cbOprationType.Text;
                _time = int.Parse(tbTime.Text);
                _dataType = cbDataType.Text;
                _data = tbData.TextValue;
                if (CheckTime(_time) && CheckData(_data))
                {
                    DialogResult = true;
 
                }
            }
            catch
            {
 
            }
        }

        private bool CheckData(string data)
        {
            bool ret = data.Length < 200;
            return ret;
        }

        private bool CheckTime(int time)
        {
            bool ret = time < 20000;
            return ret;
        }

        //数据格式变化
        private void cbDataType_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cbDataType.Text == "十六进制")
            {
                tbData.Mode = EditorMode.Hex;
                tbData.TextValue = "";
            }
            else
            {
                tbData.Mode = EditorMode.Character;
                tbData.TextValue = "";
            }
        }

        //时间只能输入整数
        private void tbTime_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar != '\b') e.Handled = "0123456789".IndexOf(char.ToUpper(e.KeyChar)) < 0;
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
        private void Grid_MouseMove_1(object sender, System.Windows.Input.MouseEventArgs e)
        {
            if (e.LeftButton == MouseButtonState.Pressed)
            {
                this.DragMove();
            }
        }

        
    }
}
