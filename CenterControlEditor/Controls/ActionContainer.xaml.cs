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
using CenterControlEditor.Business;

namespace CenterControlEditor.Controls
{
    /// <summary>
    /// ActionContainer.xaml 的交互逻辑
    /// </summary>
    public partial class ActionContainer : UserControl
    {
        public event EventHandler OnCurrentActionChange;

        public ActionContainer()
        {
            InitializeComponent();
        }

       
      private  List<Business.UserAction> _actionList;
      public List<Business.UserAction> ActionList
      {
          get { return _actionList; }
          set { _actionList = value; }
      }

      private Business.UserAction _currentAction;
      public Business.UserAction CurrentAction
      {
          get { return _currentAction; }
          set { _currentAction = value; }
      }


        private void AddAreaAction(object sender, RoutedEventArgs e)
        {
            if (_actionList == null)
                _actionList = new List<Business.UserAction>();
            ActionSetting setting = new ActionSetting();

            if (setting.ShowDialog() == true)
            {
                string name = setting.Action_name;
                string recveCommand = setting.ActionCode;
               Business.UserAction temp = new Business.UserAction(name,recveCommand);
                _actionList.Add(temp);
                Refresh();
            }

        }

        public void Refresh()
        {
            ActionContains.Children.Clear();
            if(_actionList != null && _actionList.Count !=0)
            {
                for (int i=0; i < _actionList.Count; i++)
                {
                    UserAction action = new UserAction();
                    action.MyAction = _actionList[i];
                    action.Index = i;
                    action.OnSelectThis += action_OnSelectThis;
                    action.OnEditThis   +=action_OnEditThis;
                    ActionContains.Children.Add(action);
                }
                InvalidateVisual();

                if (OnCurrentActionChange != null)
                {
                    OnCurrentActionChange(this,null);
                }
            }
        }

        private void action_OnEditThis(object sender, EventArgs e)
        {
           Business.UserAction actionToEdit=(sender  as  UserAction).MyAction;
           ActionSetting setting = new ActionSetting();
           setting.Action_name = (sender as UserAction).MyAction.Name;
           setting.ActionCode=(sender as UserAction ).MyAction.ReceiveCommand;

           if (setting.ShowDialog() == true)
           {
               actionToEdit.Name = setting.Action_name;
               actionToEdit.ReceiveCommand = setting.ActionCode;
               Refresh();
           }

        }

        private void action_OnSelectThis(object sender, EventArgs e)
        {
            for (int i = 0; i < ActionContains.Children.Count; i++)
            {
                (ActionContains.Children[i] as UserAction).IsSelected = false;
            }

             (sender as UserAction).IsSelected = true;

        }


        private void MoveUp(object sender, RoutedEventArgs e)
        {
            Business.UserAction actionToSwap = null;
            for (int i = 0; i < ActionContains.Children.Count; i++)
            {
                if ((ActionContains.Children[i] as UserAction).IsSelected)
                {
                    actionToSwap = (ActionContains.Children[i] as UserAction).MyAction;
                    break;
                }
            }

            if (actionToSwap != null)
            {
                for (int i = 0; i < _actionList.Count(); i++)
                {
                    if (_actionList[i] == actionToSwap && i != 0)
                    {
                        Business.UserAction actiontemp = new Business.UserAction(_actionList[i - 1].Name,_actionList[i-1].ReceiveCommand);
                        _actionList[i - 1] = actionToSwap;
                        _actionList[i] = actiontemp;

                        Refresh();
                        break;
                    }
                }
            }

 
        }

        private void MoveDown(object sender, RoutedEventArgs e)
        {
            Business.UserAction actionToSwap = null;
            for (int i = 0; i < ActionContains.Children.Count; i++)
            {
                if ((ActionContains.Children[i] as UserAction).IsSelected)
                {
                    actionToSwap = (ActionContains.Children[i] as UserAction).MyAction;
                    break;
                }
            }

            if (actionToSwap != null)
            {
                for (int i = 0; i < _actionList.Count(); i++)
                {
                    if (_actionList[i] == actionToSwap && i != _actionList.Count - 1)
                    {
                        Business.UserAction actiontemp = new Business.UserAction(_actionList[i + 1].Name,_actionList[i+1].ReceiveCommand);
                        _actionList[i + 1] = actionToSwap;
                        _actionList[i] = actiontemp;

                        Refresh();
                        break;
                    }
                }
            }
 
        }


        private void DeleteAction(object sender, RoutedEventArgs e)
        {
            Delete();
 
        }

        public void Delete()
        {
            Business.UserAction actionToDelete = null;
            for (int i = 0; i < ActionContains.Children.Count; i++)
            {
                if ((ActionContains.Children[i] as UserAction).IsSelected)
                {
                    actionToDelete = (ActionContains.Children[i] as UserAction).MyAction;
                    break;
                }
            }

            if (actionToDelete != null)
            {
                for (int i = 0; i < _actionList.Count(); i++)
                {
                    if (_actionList[i] == actionToDelete)
                    {
                        _actionList.RemoveAt(i);
                        Refresh();
                        break;
                    }
                }
            }

 
        }



    }
}
