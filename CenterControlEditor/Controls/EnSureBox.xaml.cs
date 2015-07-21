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
    /// EnSureBox.xaml 的交互逻辑
    /// </summary>
    public partial class EnSureBox : Window
    {
        public EnSureBox()
        {
            InitializeComponent();
        }

         static   int _number=0;

        ImageSource _icon;
        public ImageSource  Icon
        {
            get { return _icon; }
            set { _icon = value; icon.Source = _icon; }
        }

        private string _caption;
        public string MyCaption
        {
            get { return _caption; }
            set { _caption=value; Caption.Content = _caption; }
        }

        private string _info;
        public string MyInformation
        {
            get { return _info;}
            set { _info = value; Information.Content = _info; }
        }

        private void SetCancel(object sender, MouseButtonEventArgs e)
        {
            DialogResult = false;
            
        }

     
        private void   btnOk_Click(object sender, MouseButtonEventArgs e)
        {
            _number = 1;        
              this.DialogResult = true;            
        }

        private  void   btnCancel_Click(object sender, MouseButtonEventArgs e)
        {
            _number = 2;
            this.DialogResult = false;
            
        }

        public int PressNumber()
        {
            switch (_number)
            {
                case 2 :
                    return 0;
                case 1 :
                    return 1;
                   
                default:
                    return 0;                   
            }
        }
    }
}
