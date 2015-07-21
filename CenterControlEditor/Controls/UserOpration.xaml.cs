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
using CenterControlEditor.Controls;

namespace CenterControlEditor.Controls
{
    /// <summary>
    /// UserOpration.xaml 的交互逻辑
    /// </summary>
    public partial class UserOpration : UserControl
    {
        public event EventHandler OnSelectThis;
        public event EventHandler OnEditThis;

        public UserOpration()
        {
            InitializeComponent();
        }

        private void InitUI(object sender, RoutedEventArgs e)
        {
            Name.Width = this.ActualHeight;
            Name.Height = this.ActualWidth;
        }

        Business.UserOperation _operation;
        public Business.UserOperation MyOperation
        {
            get { return _operation; }
            set { _operation = value; OprationName.Content = _operation.Name; OprationType.Content =GetOperationTypeString(_operation.OpreationType); DateType.Content =GetDataTypeString( _operation.DataType); Data.Content = _operation.Data; Time.Content = _operation.Time; }
        }

        private object GetDataTypeString(Business.DataType type)
        {
            string ret = "十六进制";
            switch (type)
            {
                case Business.DataType.Character:
                    ret = "字符串";
                    break;
                case Business.DataType.Hex:
                    ret = "十六进制";
                    break;
            }
            return ret;
        }

        private object GetOperationTypeString(Business.OprationType oprationType)
        {
            string ret = "串口";
            switch (oprationType)
            {
                case Business.OprationType.TCP:
                    ret = "TCP";
                    break;
                case Business.OprationType.UDP:
                    ret = "UDP";
                    break;
            }
            return ret;
        }

        int _index = -1;
        public int Index
        {
            get { return _index; }
            set
            {
                _index = value;
                if ((_index % 2) == 0)
                {
                    Background = new SolidColorBrush(Color.FromArgb(255, 255, 255, 255));
                }
                else
                {
                    Background = new SolidColorBrush(Color.FromArgb(255,246,248,250));
                }
            }
        }

        bool _isSelected;
        public bool IsSelected
        {
            get { return _isSelected; }
            set
            {
                _isSelected = value;
                if (_isSelected)
                {
                    OprationName.Foreground = Brushes.Red;
                    OprationType.Foreground = Brushes.Red;
                    DateType.Foreground = Brushes.Red;
                    Data.Foreground = Brushes.Red;
                    Time.Foreground = Brushes.Red;
                }
                else
                {
                    OprationName.Foreground = Brushes.Black;
                    OprationType.Foreground = Brushes.Black;
                    DateType.Foreground = Brushes.Black;
                    Data.Foreground = Brushes.Black;
                    Time.Foreground = Brushes.Black;
 
                }
            }
        }


        private void SelectThis(object sender, MouseButtonEventArgs e)
        {
            if (OnSelectThis != null) OnSelectThis(this, null);
        }

        private void EditThis(object sender, MouseButtonEventArgs e)
        {
            if (OnEditThis != null) OnEditThis(this, null);
        }
    }
}
