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
    /// UserOrder.xaml 的交互逻辑
    /// </summary>
    public partial class UserOrder : UserControl
    {

        public event EventHandler OnSelectedThis;
        public event EventHandler OnEditThis;

        public UserOrder()
        {
            InitializeComponent();
        }

        Business.UserOrder _order;
        public Business.UserOrder MyOrder
        {
            get { return _order; }
            set { _order = value; OrderHour.Content = _order.Hour; OrderMinu.Content = _order.Minute; }
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
                    OrderHour.Foreground = Brushes.Red;
                    OrderMinu.Foreground = Brushes.Red;
                }
                else
                {
                    OrderHour.Foreground = Brushes.Black;
                    OrderMinu.Foreground = Brushes.Black;
                }
            }
        }

        private void SelectThis(object sender, MouseButtonEventArgs e)
        {
            if (OnSelectedThis != null) OnSelectedThis(this,null);
        }

        private void EditThis(object sender, MouseButtonEventArgs e)
        {
            if (OnEditThis != null) OnEditThis(this,null);
        }
    }
}
