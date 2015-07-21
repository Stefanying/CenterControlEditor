using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Media;
using CenterControlEditor.Controls;

namespace CenterControlEditor.Business
{
     public class helper
    {
         string _caption;
         public string Caption
         {
             get { return _caption; }
             set { _caption = value; }
         }

         string _info;
         public string Information
         {
             get { return _info; }
             set { _info = value; }
         }

         ImageSource _icon;
         public ImageSource Icon
         {
             get { return _icon; }
             set { _icon = value; }
         }

         public helper( )
         {
             
             //_icon=icon;
             EnSureBox enBox = new EnSureBox();
            // enBox.ShowDialog();
             //enBox.MyCaption = _caption;
             //enBox.MyInformation= _info;
            // enBox.Icon = _icon;
         }

         public int  showMessage(string caption, string Info)
         {
             _caption = caption;
             _info = Info;
             EnSureBox enBox = new EnSureBox();
             enBox.MyCaption = _caption;
             enBox.MyInformation = _info;
             enBox.ShowDialog();
             int number = enBox.PressNumber();
             Console.Write(number);
             return number;
         }

    }
}
