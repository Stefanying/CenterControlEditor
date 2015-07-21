using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
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
using System.Threading;
using System.Net;
using System.Net.Sockets;
using CenterControlEditor.Business;
using System.Xml;
using SFLib;

namespace CenterControlEditor
{
    /// <summary>
    /// Window1.xaml 的交互逻辑
    /// </summary>
    public partial class MainWindow : Window
    {
        TcpClient _client;
        string _hostname;
        int _port = 10003;
        int _blockLength = 1024;

        string _configFile = AppDomain.CurrentDomain.BaseDirectory + "config.xml";
        string _timelineConfig = AppDomain.CurrentDomain.BaseDirectory + "timeline.xml";

      //  Controls.UserArea _arealist = new Controls.UserArea();
        Controls.UserAction _actionlist = new Controls.UserAction();
        Controls.UserOpration _oprationlist = new Controls.UserOpration();
        Controls.UserOrder _timeList = new Controls.UserOrder();
        Controls.UserOpration _orderlist = new Controls.UserOpration();

        Controls.AreaContainer _arealist = new Controls.AreaContainer();


        List<Area> _areas = new List<Area>();
        List<UserAction> _userAction = new List<UserAction>();
        List<UserOperation> _userOperation = new List<UserOperation>();
        List<UserOrder> _orders =new List<UserOrder>();
        List<UserAction> _shaft_actions = new List<UserAction>();


        public MainWindow()
        {
            InitializeComponent();
            LoadConfig();
            AreasEditor.Areas = _areas;
            AreasEditor.Refresh();
            ActionEditor.ActionList = _userAction;
            ActionEditor.Refresh();
            OprationEditor.Opration = _userOperation;
            OprationEditor.Refresh();
        }

        private void MiniMize_MouseDown(object sender, MouseButtonEventArgs e)
        {
            this.WindowState = WindowState.Minimized;
        }

        private void MaxiMize_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (this.WindowState == WindowState.Maximized)
            {
                this.WindowState = WindowState.Normal;
            }
            else
            {
                this.WindowState = WindowState.Maximized;
            }
        }

        private void CloseWindow_MouseDown(object sender, MouseButtonEventArgs e)
        {
            this.Close();
        }

        private void MoveThis(object sender, MouseEventArgs e)
        {
            if (e.LeftButton == MouseButtonState.Pressed)
                this.DragMove();

        }

        private void TabControl1_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

            //Console.Write("TabControl had  choose!");
            switch (this.TabControl1.SelectedIndex)
            {
                case 0:
                    toolBar.Content = null;
                    title.Content = "配置 To configure";
                    break;
                case 1:
                    title.Content = "预约 Make an appointment";
                    toolBar.Content = new Controls.OrderToolBar();
                    toolBar.Width = 240;
                    
                    toolBar.Margin = new Thickness(480,0,0,0);
                    break;
                case 2:
                    title.Content = "设置 Set up";
                    toolBar.Content = null;
                    break;
                default:
                    break;
            }
        }

        private void btnSave_Click(object sender,MouseButtonEventArgs e)
        {
            SaveConfig();
 
        }

        private void SaveConfig()
        {
            XmlDocument config = new XmlDocument();

            XmlNode root = config.CreateNode(XmlNodeType.Element,"Root",null);
            config.AppendChild(root);
            _areas = new List<Business.Area>();
            Console.Write("_areas");
            Console.Write(_areas.Count());
            #region NormalCommands
            for (int i = 0; i < _areas.Count;i++ )
            {

            }
            #endregion
        }



        /// <summary>
        /// 加载配置文件
        /// </summary>
        private void LoadConfig()
        {
            try
            {
                XmlDocument config = new XmlDocument();
                config.Load(_configFile);

                XmlNode root = config.SelectSingleNode("Root");

                #region LoadNormalCommand
                XmlNodeList areas = root.SelectNodes("Area");
                _areas.Clear();
                foreach (XmlNode area in areas)
                {
                    string areaName = area.SelectSingleNode("AreaName").InnerText;
                    Area tempArea = new Area(areaName);
                    _areas.Add(tempArea);

                    XmlNodeList actions = area.SelectNodes("Action");
                    foreach (XmlNode action in actions)
                    {
                        string actionName = action.SelectSingleNode("ActionName").InnerText;
                        string actionReceiveData = action.SelectSingleNode("ActionReceiveData").InnerText;
                        UserAction useraction = new UserAction(actionName,actionReceiveData);
                        //tempArea.Actions.Add(useraction);
                        _userAction.Add(useraction);
                        XmlNodeList oprations = action.SelectNodes("Operation");
                        foreach (XmlNode operation in oprations)
                        {
                            string operationName = operation.SelectSingleNode("OperationName").InnerText;
                            string operationTypeString = operation.SelectSingleNode("OperationType").InnerText;
                            OprationType operationType = (OprationType)Enum.Parse(typeof(OprationType),operationTypeString,true);
                            XmlNode operationSetting = operation.SelectSingleNode("OperationSetting");
                            object setting = null;
                            if (operationType == OprationType.Com)
                            {
                                ComSetting cs = new ComSetting();
                                cs.ComNumber = operationSetting.SelectSingleNode("ComNumber").InnerText;
                                cs.BaudRate = int.Parse(operationSetting.SelectSingleNode("BaudRate").InnerText);
                                cs.DataBits = int.Parse(operationSetting.SelectSingleNode("DataBits").InnerText);
                                cs.StopBits = int.Parse(operationSetting.SelectSingleNode("StopBit").InnerText);
                                cs.Parity = (Parity)Enum.Parse(typeof(Parity),operationSetting.SelectSingleNode("Parity").InnerText);

                                setting = cs;
                            }
                            else if (operationType == OprationType.TCP || operationType == OprationType.UDP)
                            {
                                NetworkSetting ns = new NetworkSetting();
                                ns.Ip = operationSetting.SelectSingleNode("IP").InnerText;
                                ns.Port = int.Parse(operationSetting.SelectSingleNode("Port").InnerText);
                                setting = ns;
                            }

                            string dataTypeString = operation.SelectSingleNode("OperationDataType").InnerText;
                            DataType dataType = (DataType)Enum.Parse(typeof(DataType),dataTypeString,true);
                            string data = operation.SelectSingleNode("OperationData").InnerText;
                            int time = int.Parse(operation.SelectSingleNode("OperationTime").InnerText);
                            UserOperation userOperation = new UserOperation(operationName,operationType,dataType,setting,data,time);
                            _userOperation.Add(userOperation);
                        }
                    }

                }
                _arealist.Refresh();
                #endregion
            }
            catch(Exception ex)
            {
                MessageBox.Show("未找到配置命令!");
                
            }
 
        }
      
    }
}
