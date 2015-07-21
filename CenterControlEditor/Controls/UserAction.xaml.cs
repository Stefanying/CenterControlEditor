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
    /// UserAction.xaml 的交互逻辑
    /// </summary>
    public partial class UserAction : UserControl
    {
        public event EventHandler OnSelectThis;
        public event EventHandler OnEditThis;

        public UserAction()
        {
            InitializeComponent();
        }


        Business.UserAction _action;
        public Business.UserAction MyAction
        {
            get { return _action; }
            set { _action = value; ActionName.Content = _action.Name;CodeName.Content = _action.ReceiveCommand; }
        }

       


        private void InitUI(object sender, RoutedEventArgs e)
        {
            ActionContainer.Width = this.ActualWidth;
            ActionContainer.Height = this.ActualHeight;
            
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
                ActionName.Foreground = Brushes.Red;
                CodeName.Foreground = Brushes.Red;

            }
            else
            { 
                ActionName.Foreground = Brushes.Black;
                CodeName.Foreground = Brushes.Black;
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
