using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CenterControlEditor.Business
{
    public class Area
    {
        string _name;

        public string Name
        {
            get { return _name; }
            set { _name = value; }
        }

//声明List
        List<UserAction> _actions;

        public List<UserAction> Actions
        {
            get { return _actions; }
            set { _actions = value; }
        }


//构造函数
        public Area(string name)
        {
            _name = name;
            _actions = new List<UserAction>();
        }

        public void AddAction(UserAction op)
        {
            _actions.Add(op);
        }

        public void Remove(UserAction op)
        {
            _actions.Remove(op);
        }
    }
}
