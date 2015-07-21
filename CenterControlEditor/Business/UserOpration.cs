using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CenterControlEditor.Business
{
    public enum OprationType
    {
        TCP,
        UDP,
        Com
    }

    public enum DataType
    {
        Hex,
        Character
    }

    public enum Parity
    {
        Odd,
        Even,
        None
    }

    /// <summary>
    /// 串口设置 
    /// ComNumber、BaudRate、DataBits、StopBits、Parity
    /// 成员变量
    /// </summary>
    public class ComSetting
    {
        string _comNumber;
        public string ComNumber
        {
            get { return _comNumber; }
            set { _comNumber = value; }
        }

        int _baudRate;
        public int BaudRate
        {
            get { return _baudRate; }
            set { _baudRate = value; }
        }

        int _dataBits;
        public int DataBits
        {
            get { return _dataBits; }
            set { _dataBits = value; }
        }
        int _stopBits;

        public int StopBits
        {
            get { return _stopBits; }
            set { _stopBits = value; }
        }

        Parity _parity;
        public Parity Parity
        {
            get { return _parity; }
            set { _parity = value; }
        }
    }

    /// <summary>
    /// 网络设置
    /// 
    /// ip、port 成员变量
    /// </summary>
    public class NetworkSetting
    {
        string _ip;

        public string Ip
        {
            get { return _ip; }
            set { _ip = value; }
        }
        int _port;

        public int Port
        {
            get { return _port; }
            set { _port = value; }
        }
    }

    public class UserOperation
    {
        string _name;

        public string Name
        {
            get { return _name; }
            set { _name = value; }
        }

        OprationType _opreationType;

       
        internal OprationType OpreationType
        {
            get { return _opreationType; }
            set { _opreationType = value; }
        }

        DataType _dataType;
        internal DataType DataType
        {
            get { return _dataType; }
            set { _dataType = value; }
        }

        object _setting;
        public object Setting
        {
            get { return _setting; }
            set { _setting = value; }
        }

        int _time;//与上一条命令间隔时间
        public int Time
        {
            get { return _time; }
            set { _time = value; }
        }

        string _data;
        public string Data
        {
            get { return _data; }
            set { _data = value; }
        }

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="name"></param>
        /// <param name="oType"></param>
        /// <param name="dType"></param>
        /// <param name="setting"></param>
        /// <param name="data"></param>
        /// <param name="time"></param>
        public UserOperation(string name, OprationType oType, DataType dType, object setting, string data, int time)
        {
            _name = name;
            _opreationType = oType;
            _dataType = dType;
            _setting = setting;
            _data = data;
            _time = time;
        }
    }
}
