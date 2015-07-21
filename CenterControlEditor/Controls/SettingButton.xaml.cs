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
    /// SettingButton.xaml 的交互逻辑
    /// </summary>
    public partial class SettingButton : UserControl
    {
        public SettingButton()
        {
            InitializeComponent();
        }

        ImageSource _icon;
        public ImageSource Icon
        {
            get { return _icon; }
            set { _icon = value; ImageIcon.Source = _icon; }
        }
    }
}
