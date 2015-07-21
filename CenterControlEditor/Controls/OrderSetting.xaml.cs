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
    /// OrderSetting.xaml 的交互逻辑
    /// </summary>
    public partial class OrderSetting : Window
    {
        public OrderSetting()
        {
            InitializeComponent();
        }

      private  int _order_hour;
      public int Order_Hour
      {
          get { return _order_hour; }
          set { _order_hour = value; tbHour.Text = value.ToString(); }
      }

      private int _order_minu;
      public int Order_Minu
      {
          get { return _order_minu; }
          set { _order_minu = value; tbMin.Text =value.ToString(); }
      }

        /// <summary>
        /// 确定
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
      private void SetOk(object sender, MouseButtonEventArgs e)
      {
          _order_hour = int.Parse(tbHour.Text);
          _order_minu = int.Parse(tbMin.Text);
          DialogResult = true;
 
      }


/// <summary>
/// 取消
/// </summary>
/// <param name="sender"></param>
/// <param name="e"></param>
        private void SetCancel(object sender,MouseButtonEventArgs e)
        {
            DialogResult = false;
 
        }
    }
}
