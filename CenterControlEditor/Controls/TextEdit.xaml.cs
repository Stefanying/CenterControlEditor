using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Forms;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace CenterControlEditor.Controls
{
    /// <summary>
    /// TextEdit.xaml 的交互逻辑
    /// </summary>
    /// 
    public enum EditorMode
    {
        Hex =0,
        Character=1
    }
    public partial class TextEdit : System.Windows.Controls.UserControl
    {
        EditorMode _mode = EditorMode.Character;
        public EditorMode Mode
        {
            get { return _mode; }
            set { _mode = value; }
        }

        string _textValue = "";
        public string TextValue
        {
            get { _textValue = editor.Text; return _textValue; }
            set
            {
                _textValue = value;
                editor.Text = value;
            }
        }
        public TextEdit()
        {
            InitializeComponent();
        }

        private void editor_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (_mode == EditorMode.Hex)
            {
                if (e.KeyChar != '\b') e.Handled = "0123456789ABCDEF".IndexOf(char.ToUpper(e.KeyChar)) < 0;
            }
        }   
    }
}
