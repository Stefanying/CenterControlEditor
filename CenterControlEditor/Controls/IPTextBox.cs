using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Controls;
using System.Windows.Input;

namespace CenterControlEditor.Controls
{
    class IPTextBox:TextBox
    {
        IPTextBox leftBox = null;
        IPTextBox rightBox = null;

        public void setNeighbour(IPTextBox left, IPTextBox right)
        {
            leftBox = left;
            rightBox = right;
        }

        //按下键
        protected override void OnPreviewKeyDown(KeyEventArgs e)
        {
            base.OnPreviewKeyDown(e);

            //删除键
            if (e.Key == Key.Back)
            {
                if ((CaretIndex == 0) && (leftBox != null) && SelectionLength == 0)
                {
                    leftBox.Focus();
                    leftBox.CaretIndex = leftBox.Text.Length;
                    e.Handled = true;
                }
            }

            //光标左移
            if (e.Key == Key.Left)
            {
                if ((CaretIndex == 0) && (leftBox != null))
                {
                    leftBox.Focus();
                    leftBox.CaretIndex = leftBox.Text.Length;
                    e.Handled = true;
                }
            }

            //光标右移
            if (e.Key == Key.Right)
            {
                if ((CaretIndex == Text.Length) && (rightBox != null))
                {
                    rightBox.Focus();
                    rightBox.CaretIndex =0;
                    e.Handled = true;
                }
            }

        }


        //文本输入时
        protected override void OnPreviewTextInput(TextCompositionEventArgs e)
        {
            base.OnPreviewTextInput(e);

            char ch = char.Parse(e.Text);

            if (ch == '.')
            {
                if ((CaretIndex == Text.Length) && (rightBox != null))
                {
                    rightBox.Focus();
                    rightBox.SelectAll();
                    e.Handled = true;
                    return;
                }
            }

            if (ch < '0' || ch > '9')
            {
                e.Handled = true;
                return;
            }

            if ((Text.Length >= 3) && SelectionLength == 0)
            {
                e.Handled = true;
                return;
            }
 
        }

        protected override void OnTextInput(TextCompositionEventArgs e)
        {
            base.OnTextInput(e);
            int ip = Int16.Parse(Text);

            if(ip>255)
                Text="255";

            if (Text.Length == 3)
            {
                if ((CaretIndex == Text.Length) && (rightBox != null))
                {
                    rightBox.Focus();
                    rightBox.SelectAll();
                }
            }
           
        }




    }

   



}
