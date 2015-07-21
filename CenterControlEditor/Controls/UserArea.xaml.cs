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
    /// UserArea.xaml 的交互逻辑
    /// </summary>
    public partial class UserArea : UserControl
    {
        public event EventHandler OnSelectThis;//事件委托
        public event EventHandler OnEditThis;
       // public event EventHandler DeleteThis;

        public UserArea()
        {
            InitializeComponent();
        }

        private void InitUI(object sender, RoutedEventArgs e)
        {
            Container.Width = this.ActualWidth;
            Container.Height = this.ActualHeight;
        }

        Business.Area _myArea;//Business.Area 类型的属性

        public Business.Area MyArea
        {
            get { return _myArea; }
            set { _myArea = value;
                AreaName.Content = _myArea.Name; }
        }

        int _index = -1;

        public int Index
        {
            get { return _index; }
            set { _index = value;
                if ((_index % 2) == 0)
                {
                    Background = new SolidColorBrush(Color.FromArgb(255, 255, 255, 255));
                }
                else
                {
                    Background = new SolidColorBrush(Color.FromArgb(255, 246, 248, 250));
                }   
            }
        }

        bool _isSelected;
        public bool IsSelected
        {
            get { return _isSelected; }
            set { _isSelected = value;
                if (_isSelected)
                {
                    AreaName.Foreground = Brushes.Red;
                    
                  }   
                  else
                {
                   AreaName.Foreground = Brushes.Black;
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
