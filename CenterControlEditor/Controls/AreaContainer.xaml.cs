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
    /// AreaContainer.xaml 的交互逻辑
    /// </summary>
    public partial class AreaContainer : UserControl
    {

       
        public event EventHandler OnCurrentAreaChanged;
        public AreaContainer()
        {
            InitializeComponent();
            Refresh();
        }

        private Area _currentArea;
        public Area CurrentArea
        {
            get { return _currentArea; }
            set { _currentArea = value; }
        }

       private List<Area> _areas;
       public List<Area> Areas
       {
           get { return _areas; }
           set { _areas = value; }
       }

        private object _lock = new object();

        private void AddArea(object sender, RoutedEventArgs e)
        {
            lock (_lock)
            {
                if (_areas == null)
                    _areas = new List<Business.Area>();
                AreaSetting setting = new AreaSetting();
                if (setting.ShowDialog() == true)
                {
                    string name = setting.Area_name;
                    Area temp = new Area(name);
                    _areas.Add(temp);//将Area类类型的变量添加到List表中
                    //更新
                    Refresh();
                }       
            }
        }

      public  void Refresh()
        {
            AreaContains.Children.Clear();
           // Console.Write("Begin!");
          
            if (_areas ==null)
            {
               // Console.Write("_areas is Null!");
 
            }
            if (_areas != null && _areas.Count != 0)
            {
                Console.Write("!");
                for (int i = 0; i < _areas.Count(); i++)
                {
                    UserArea area = new UserArea();
                    area.MyArea = _areas[i];
                    area.Index = i;
                    area.OnSelectThis += area_OnSelectThis;
                    area.OnEditThis += area_OnEditThis;
                    AreaContains.Children.Add(area);
                    Console.Write(_areas.Count());
                }
                InvalidateVisual();

                if (OnCurrentAreaChanged != null)
                {
                    OnCurrentAreaChanged(this,null);
                }
            }
        }

          void area_OnEditThis(object sender, EventArgs e)
        {

            Area areaToEdit = (sender as UserArea).MyArea;
         
            AreaSetting setting = new AreaSetting();
            setting.Area_name = (sender as UserArea).MyArea.Name;

            if (setting.ShowDialog() == true)
            {
                areaToEdit.Name = setting.Area_name;

                Refresh();
            }

        }

        void area_OnSelectThis(object sender, EventArgs e)
        {
            for (int i = 0; i < AreaContains.Children.Count; i++)
            {
                (AreaContains.Children[i] as UserArea).IsSelected = false;
            }

            (sender as UserArea).IsSelected = true;
        }

       

        private void MoveUp(object sender, RoutedEventArgs e)
        {
                 Area  areaToSwap = null;
            for (int i = 0; i < AreaContains.Children.Count; i++)
            {
                if ((AreaContains.Children[i] as UserArea).IsSelected)
                {
                    areaToSwap = (AreaContains.Children[i] as UserArea).MyArea;
                    break;
                }
            }

            if (areaToSwap != null)
            {
                for (int i = 0; i < _areas.Count(); i++)
                {
                    if (_areas[i] == areaToSwap && i != 0)
                    {
                         Area areatemp = new Area(_areas[i - 1].Name);
                        _areas[i - 1] = areaToSwap;
                        _areas[i] = areatemp;

                        Refresh();
                        break;
                    }
                }
            }
        }

        private void MoveDown(object sender, RoutedEventArgs e)
        {
            Area areaToSwap = null;
            for (int i = 0; i < AreaContains.Children.Count; i++)
            {
                if ((AreaContains.Children[i] as UserArea).IsSelected)
                {
                    areaToSwap = (AreaContains.Children[i] as UserArea).MyArea;
                    break;
                }
            }

            if (areaToSwap != null)
            {
                for (int i = 0; i < _areas.Count(); i++)
                {
                    if (_areas[i] == areaToSwap && i != _areas.Count - 1)
                    {
                        Area areatemp = new Area(_areas[i + 1].Name);
                        _areas[i+1] = areaToSwap;
                        _areas[i] = areatemp;

                        Refresh();
                        break;
                    }
                }
            }
        }

        public void Delete(Area area)
        {
            lock (_lock)
            {
                if (_areas != null)
                {
                    _areas.Remove(area);
                }
                Refresh();
            }
        }





        private void DeleteArea(object sender, RoutedEventArgs e)
        {
            Area areaToDelete = null;
            for (int i = 0; i < AreaContains.Children.Count; i++)
            {
                if ((AreaContains.Children[i] as UserArea).IsSelected)
                {
                    areaToDelete = (AreaContains.Children[i] as UserArea).MyArea;
                    Delete(areaToDelete);
                    break;
                }
            }

            /*
            if (areaToDelete != null)
            {
                for (int i = 0; i < _areas.Count(); i++)
                {
                    if (_areas[i] == areaToDelete)
                    {
                        _areas.RemoveAt(i);
                        Refresh();
                        break;
                    }
                }
               */
                
            
        }

       

        
    }
}
