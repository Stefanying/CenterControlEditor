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
    /// OrderAreaContainer.xaml 的交互逻辑
    /// </summary>
    public partial class OrderAreaContainer : UserControl
    {
        public OrderAreaContainer()
        {
            InitializeComponent();
        }

        private List<Business.UserOrder> _orderList;
        public List<Business.UserOrder> OrderList
        {
            get { return _orderList; }
            set { _orderList = value; }
        }

        private Business.UserOrder _currentOrder;
        public Business.UserOrder CurrentOrder
        {
            get { return _currentOrder; }
            set { _currentOrder = value; }
        }

        private void AddOrderEvent(object sender, RoutedEventArgs e)
        {
            if (_orderList == null)
            {
                _orderList = new List<Business.UserOrder>();
            }
            OrderSetting setting = new OrderSetting();
            if (setting.ShowDialog() == true)
            {
                int hour = setting.Order_Hour;
                int minu = setting.Order_Minu;
                Business.UserOrder temp = new Business.UserOrder(hour, minu);
                _orderList.Add(temp);
                Refresh();
            }

        }

        private void Refresh()
        {
            OrderAction.Children.Clear();
            if (_orderList != null && _orderList.Count != 0)
            {
                for (int i = 0; i < _orderList.Count; i++)
                {
                    UserOrder order = new UserOrder();
                    order.MyOrder = _orderList[i];
                    order.Index = i;
                    order.OnSelectedThis += order_OnSelectThis;
                    order.OnEditThis += order_OnEditThis;
                    OrderAction.Children.Add(order);
                }
                InvalidateVisual();
            }
        }

        private void order_OnEditThis(object sender, EventArgs e)
        {
            Business.UserOrder orderToEdit = (sender as UserOrder).MyOrder;
            OrderSetting setting = new OrderSetting();
            setting.Order_Hour = (sender as UserOrder).MyOrder.Hour;
            setting.Order_Minu = (sender as UserOrder).MyOrder.Minute;

            if (setting.ShowDialog() == true)
            {
                orderToEdit.Hour = setting.Order_Hour;
                orderToEdit.Minute = setting.Order_Minu;
                Refresh();
            }
        }

        private void order_OnSelectThis(object sender, EventArgs e)
        {
            for (int i = 0; i < OrderAction.Children.Count; i++)
            {
                (OrderAction.Children[i] as UserOrder).IsSelected = false;
            }

            (sender as UserOrder).IsSelected = true;
        }


        private void MoveUp(object sender, RoutedEventArgs e)
        {
            Business.UserOrder orderToSwap = null;
            for (int i = 0; i < OrderAction.Children.Count; i++)
            {
                if ((OrderAction.Children[i] as UserOrder).IsSelected)
                {
                    orderToSwap = (OrderAction.Children[i] as UserOrder).MyOrder;
                    break;
                }
            }

            if (orderToSwap != null)
            {
                for (int i = 0; i < _orderList.Count(); i++)
                {
                    if (_orderList[i] == orderToSwap && i != 0)
                    {
                        Business.UserOrder orderTemp = new Business.UserOrder(_orderList[i - 1].Hour, _orderList[i - 1].Minute);
                        _orderList[i - 1] = orderToSwap;
                        _orderList[i] = orderTemp;

                        Refresh();
                        break;
                    }
                }
            }
        }

        private void MoveDown(Object sender, RoutedEventArgs e)
        {
            Business.UserOrder orderToSwap = null;
            for (int i = 0; i < OrderAction.Children.Count; i++)
            {
                if ((OrderAction.Children[i] as UserOrder).IsSelected)
                {
                    orderToSwap = (OrderAction.Children[i] as UserOrder).MyOrder;
                    break;
                }
            }

            if (orderToSwap != null)
            {
                for (int i = 0; i < _orderList.Count(); i++)
                {
                    if (_orderList[i] == orderToSwap && i != 0)
                    {
                        Business.UserOrder orderTemp = new Business.UserOrder(_orderList[i + 1].Hour, _orderList[i + 1].Minute);
                        _orderList[i + 1] = orderToSwap;
                        _orderList[i] = orderTemp;

                        Refresh();
                        break;
                    }
                }
            }
        }

        private void DeleteOrderEvent(object sender, RoutedEventArgs e)
        {
            Business.UserOrder orderToDelete = null;
            for (int i = 0; i < OrderAction.Children.Count; i++)
            {
                if ((OrderAction.Children[i] as UserOrder).IsSelected)
                {
                    orderToDelete = (OrderAction.Children[i] as UserOrder).MyOrder;
                    break;
                }
            }

            if (orderToDelete != null)
            {
                for (int i = 0; i < _orderList.Count(); i++)
                {
                    if (_orderList[i] == orderToDelete)
                    {
                        _orderList.RemoveAt(i);
                        Refresh();
                        break;
                    }
                }
            }
           
 
        }

    }
}
