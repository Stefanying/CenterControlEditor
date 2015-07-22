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
            _areas = AreasEditor.Areas;
            _userAction = ActionEditor.ActionList;
            _userOperation = OprationEditor.Opration;
            #region NormalCommands
            for (int i = 0; i < _areas.Count;i++ )
            {
                Area currentArea = _areas[i];
                currentArea.Actions = _userAction;
                
                XmlNode area = config.CreateNode(XmlNodeType.Element,"Area",null);
                XmlNode areaname = config.CreateNode(XmlNodeType.Element,"AreaName",null);
                areaname.InnerText = currentArea.Name;
                area.AppendChild(areaname);
                for (int count_action = 0; count_action < currentArea.Actions.Count; count_action++)
                {
                    
                    UserAction temp = currentArea.Actions[count_action];
                    temp.Operations = _userOperation;
                    XmlNode action = config.CreateNode(XmlNodeType.Element,"Action",null);
                    XmlElement actionName = config.CreateElement("ActionName");
                    actionName.InnerText = temp.Name;
                    XmlElement receiveData = config.CreateElement("ActionReceiveData");
                    receiveData.InnerText = temp.ReceiveCommand;

                    action.AppendChild(actionName);
                    action.AppendChild(receiveData);
                    for (int count_opreation = 0; count_opreation < temp.Operations.Count; count_opreation++)
                    {
                        UserOperation operation = temp.Operations[count_opreation];
                        XmlNode operationNode = config.CreateNode(XmlNodeType.Element,"Operation",null);
                        XmlNode operationName = config.CreateNode(XmlNodeType.Element,"OperationName",null);
                        operationName.InnerText = operation.Name;
                        XmlNode operationType = config.CreateNode(XmlNodeType.Element, "OperationType", null);
                        operationType.InnerText = operation.OpreationType.ToString();//将选择的通信方式转换成字符串格式存储到xml文件中

                        XmlNode operationDataType = config.CreateNode(XmlNodeType.Element, "OperationDataType", null);
                        operationDataType.InnerText = operation.DataType.ToString();

                        XmlNode operationData = config.CreateNode(XmlNodeType.Element, "OperationData", null);

                        if (operation.DataType == DataType.Hex)
                        {
                            operationData.InnerText = operation.Data.Replace(" ", "").Trim();
                        }
                        else
                        {
                            operationData.InnerText = operation.Data.Trim();
                        }

                        XmlNode operationTime = config.CreateNode(XmlNodeType.Element, "OperationTime", null);
                        operationTime.InnerText = operation.Time.ToString();

                        XmlNode operationSetting = config.CreateNode(XmlNodeType.Element, "OperationSetting", null);
                        if (operation.Setting as ComSetting != null)
                        {
                            SaveComSetting(config, operation, operationSetting);
                        }
                        else if (operation.Setting as NetworkSetting != null)
                        {
                            SaveIPSetting(config, operation, operationSetting);
                        }

                        operationNode.AppendChild(operationName);
                        operationNode.AppendChild(operationType);
                        operationNode.AppendChild(operationDataType);
                        operationNode.AppendChild(operationData);
                        operationNode.AppendChild(operationTime);
                        operationNode.AppendChild(operationSetting);

                        action.AppendChild(operationNode);
                    }
                    area.AppendChild(action);
                }
                root.AppendChild(area);
            }
            #endregion

            #region TimeShaft
            #endregion
            config.Save(_configFile);

        }

        private static void SaveIPSetting(XmlDocument config, UserOperation operation, XmlNode operationSetting)
        {
            NetworkSetting ns = operation.Setting as NetworkSetting;
            XmlNode ip = config.CreateNode(XmlNodeType.Element, "IP", null);
            ip.InnerText = ns.Ip;

            XmlNode port = config.CreateNode(XmlNodeType.Element, "Port", null);
            port.InnerText = ns.Port.ToString();

            operationSetting.AppendChild(ip);
            operationSetting.AppendChild(port);
        }

        private static void SaveComSetting(XmlDocument config, UserOperation operation, XmlNode operationSetting)
        {
            ComSetting cs = operation.Setting as ComSetting;
            XmlNode comNumber = config.CreateNode(XmlNodeType.Element, "ComNumber", null);
            comNumber.InnerText = cs.ComNumber;

            XmlNode baudRate = config.CreateNode(XmlNodeType.Element, "BaudRate", null);
            baudRate.InnerText = cs.BaudRate.ToString();

            XmlNode dataBit = config.CreateNode(XmlNodeType.Element, "DataBit", null);
            dataBit.InnerText = cs.DataBits.ToString();

            XmlNode stopBit = config.CreateNode(XmlNodeType.Element, "StopBit", null);
            stopBit.InnerText = cs.StopBits.ToString();

            XmlNode parity = config.CreateNode(XmlNodeType.Element, "Parity", null);
            parity.InnerText = cs.Parity.ToString();

            operationSetting.AppendChild(comNumber);
            operationSetting.AppendChild(baudRate);
            operationSetting.AppendChild(dataBit);
            operationSetting.AppendChild(stopBit);
            operationSetting.AppendChild(parity);
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
                                cs.DataBits = int.Parse(operationSetting.SelectSingleNode("DataBit").InnerText);
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
