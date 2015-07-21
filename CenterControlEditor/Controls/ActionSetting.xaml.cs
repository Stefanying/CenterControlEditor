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

namespace CenterControlEditor.Controls
{
    /// <summary>
    /// ActionSetting.xaml 的交互逻辑
    /// </summary>
    public partial class ActionSetting : Window
    {
        public ActionSetting()
        {
            InitializeComponent();
        }

        private void SetOk(object sender, MouseButtonEventArgs e)
        {
            _action_name=tbName.Text;
            _actionCode = tbCustomcode.Text;
            DialogResult = true;
        }

        private void SetCancel(object sender, MouseButtonEventArgs e)
        {
            DialogResult = false;
        }

        string _action_name;
        public  string   Action_name
        {
            get { return _action_name; }
            set { _action_name = value; tbName.Text = _action_name; }
        }

        string _actionCode;

        public string ActionCode
        {
            get { return _actionCode; }
            set { _actionCode = value; tbCustomcode.Text = _actionCode; }
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
