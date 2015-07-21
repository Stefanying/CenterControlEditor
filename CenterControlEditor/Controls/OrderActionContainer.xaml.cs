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
    /// OrderActionContainer.xaml 的交互逻辑
    /// </summary>
    public partial class OrderActionContainer : UserControl
    {
        public OrderActionContainer()
        {
            InitializeComponent();
        }

        private List<Business.UserOperation> _oprationList;
        public List<Business.UserOperation> Opration
        {
            get { return _oprationList; }
            set { _oprationList = value; }
        }

        private Business.UserOperation _CurrentOperation;
        public Business.UserOperation CurrentOperation
        {
            get { return _CurrentOperation; }
            set { _CurrentOperation = value; }
        }


        private void AddOprationAction(object sender, RoutedEventArgs e)
        {
            if (_oprationList == null)
                _oprationList = new List<Business.UserOperation>();
            OprationSetting setting = new OprationSetting();
            if (setting.ShowDialog() == true)
            {
                string name = setting.OprationName;
                Business.OprationType opType = Business.OprationType.Com;
                //  Console.Write(opType);
                if (setting.OprationType.ToLower() == "tcp") opType = Business.OprationType.TCP;
                else if (setting.OprationType.ToLower() == "udp") opType = Business.OprationType.UDP;
                else if (setting.OprationType.ToLower() == "串口") opType = Business.OprationType.Com;

                Business.DataType dType = Business.DataType.Character;
                if (setting.DataType.ToLower() == "十六进制") dType = Business.DataType.Hex;
                else if (setting.DataType.ToLower() == "字符串") dType = Business.DataType.Character;

                string daTa = setting.Data;
                int time = setting.Time;
                Object a = setting.Setting;
                Business.UserOperation temp = new Business.UserOperation(name, opType, dType, a, daTa, time);
                _oprationList.Add(temp);
                Refresh();

            }

        }

        public void Refresh()
        {
            OpratiorContainer.Children.Clear();
            if (_oprationList != null && _oprationList.Count != 0)
            {
                for (int i = 0; i < _oprationList.Count; i++)
                {
                    UserOpration opration = new UserOpration();
                    opration.MyOperation = _oprationList[i];
                    opration.Index = i;
                    opration.OnSelectThis += opration_OnseleceThis;
                    opration.OnEditThis += opration_OnEditThis;
                    OpratiorContainer.Children.Add(opration);
                }
                InvalidateVisual();
            }
        }

        private void opration_OnEditThis(object sender, EventArgs e)
        {
            Business.UserOperation oprationToEdit = (sender as UserOpration).MyOperation;
            OprationSetting setting = new OprationSetting();
            setting.OprationName = (sender as UserOpration).MyOperation.Name;
            Console.Write(setting.OprationName);
            setting.OprationType = GetOperationTypeString((sender as UserOpration).MyOperation.OpreationType);
            Console.Write(setting.OprationType);
            setting.DataType = GetDataTypeString((sender as UserOpration).MyOperation.DataType);
            setting.Data = (sender as UserOpration).MyOperation.Data;
            setting.Time = (sender as UserOpration).MyOperation.Time;

            if (setting.ShowDialog() == true)
            {
                oprationToEdit.Name = setting.OprationName;
                //  oprationToEdit.OpreationType=(Business.OprationType)Enum.Parse(typeof(Business.OprationType), setting.OprationType);
                // oprationToEdit.DataType =(Business.DataType)Enum.Parse(typeof(Business.DataType),setting.DataType);
                Business.OprationType opType = Business.OprationType.Com;
                if (setting.OprationType.ToLower() == "tcp") opType = Business.OprationType.TCP;
                else if (setting.OprationType.ToLower() == "udp") opType = Business.OprationType.UDP;
                else if (setting.OprationType.ToLower() == "串口") opType = Business.OprationType.Com;

                Business.DataType dType = Business.DataType.Character;
                if (setting.DataType.ToLower() == "十六进制") dType = Business.DataType.Hex;
                else if (setting.DataType.ToLower() == "字符串") dType = Business.DataType.Character;
                oprationToEdit.Data = setting.Data;
                oprationToEdit.Time = setting.Time;
                Refresh();
            }
        }

        string GetOperationTypeString(Business.OprationType type)
        {
            string ret = "串口";
            switch (type)
            {
                case Business.OprationType.TCP:
                    ret = "TCP";
                    break;
                case Business.OprationType.UDP:
                    ret = "UDP";
                    break;
            }
            return ret;
        }

        string GetDataTypeString(Business.DataType type)
        {
            string ret = "十六进制";
            switch (type)
            {
                case Business.DataType.Character:
                    ret = "字符串";
                    break;
                case Business.DataType.Hex:
                    ret = "十六进制";
                    break;
            }
            return ret;
        }

        private void opration_OnseleceThis(object sender, EventArgs e)
        {
            for (int i = 0; i < OpratiorContainer.Children.Count; i++)
            {
                (OpratiorContainer.Children[i] as UserOpration).IsSelected = false;
            }

            (sender as UserOpration).IsSelected = true;
        }

        private void MoveUp(object sender, RoutedEventArgs e)
        {
            Business.UserOperation operationToSwap = null;
            for (int i = 0; i < OpratiorContainer.Children.Count; i++)
            {
                if ((OpratiorContainer.Children[i] as UserOpration).IsSelected)
                {
                    operationToSwap = (OpratiorContainer.Children[i] as UserOpration).MyOperation;
                    break;
                }

            }
            if (operationToSwap != null)
            {
                for (int i = 0; i < _oprationList.Count(); i++)
                {
                    if (_oprationList[i] == operationToSwap && i != 0)
                    {
                        Business.UserOperation operationtemp = new Business.UserOperation(_oprationList[i - 1].Name, _oprationList[i - 1].OpreationType, _oprationList[i - 1].DataType, _oprationList[i - 1].Setting, _oprationList[i - 1].Data, _oprationList[i - 1].Time);
                        _oprationList[i - 1] = operationToSwap;
                        _oprationList[i] = operationtemp;

                        Refresh();
                        break;
                    }
                }
            }
        }

        private void MoveDown(object sender, RoutedEventArgs e)
        {
            Business.UserOperation operationToSwap = null;
            for (int i = 0; i < OpratiorContainer.Children.Count; i++)
            {
                if ((OpratiorContainer.Children[i] as UserOpration).IsSelected)
                {
                    operationToSwap = (OpratiorContainer.Children[i] as UserOpration).MyOperation;
                    break;
                }
            }

            if (operationToSwap != null)
            {
                for (int i = 0; i < _oprationList.Count(); i++)
                {
                    if (_oprationList[i] == operationToSwap && i != _oprationList.Count - 1)
                    {
                        Business.UserOperation operationtemp = new Business.UserOperation(_oprationList[i + 1].Name, _oprationList[i + 1].OpreationType, _oprationList[i + 1].DataType, _oprationList[i + 1].Setting, _oprationList[i + 1].Data, _oprationList[i + 1].Time);
                        _oprationList[i + 1] = operationToSwap;
                        _oprationList[i] = operationtemp;

                        Refresh();
                        break;
                    }
                }
            }
        }

        private void DeleteArea(object sender, RoutedEventArgs e)
        {
            Business.UserOperation operationToDelete = null;
            for (int i = 0; i < _oprationList.Count; i++)
            {
                if ((OpratiorContainer.Children[i] as UserOpration).IsSelected)
                {
                    operationToDelete = (OpratiorContainer.Children[i] as UserOpration).MyOperation;
                    break;
                }
            }
            if (operationToDelete != null)
            {
                for (int i = 0; i < _oprationList.Count; i++)
                {
                    if (_oprationList[i] == operationToDelete)
                    {
                        _oprationList.RemoveAt(i);
                        Refresh();
                        break;
                    }
                }
            }
        }
    }
}
