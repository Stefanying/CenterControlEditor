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

namespace CenterControlEditor.Controls
{
    /// <summary>
    /// AreaSetting.xaml 的交互逻辑
    /// </summary>
    public partial class AreaSetting : Window
    {
        public AreaSetting()
        {
            InitializeComponent();
        }

        private void SetOk(object sender, MouseButtonEventArgs e)
        {
            _area_name = AreaName.Text;
            DialogResult = true;
        }

        private void SetCancel(object sender, MouseButtonEventArgs e)
        {
            DialogResult = false;
        }

        string _area_name;

        /// <summary>
        /// Area _name 属性
        /// </summary>
        public string Area_name
        {
            get { return _area_name; }
            set { _area_name = value; AreaName.Text = _area_name; }
        }

        private void MoveThis(object sender, MouseEventArgs e)
        {
            if (e.LeftButton == MouseButtonState.Pressed)
                this.DragMove();
        }
    }
}
