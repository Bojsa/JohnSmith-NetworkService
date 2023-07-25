using PZ3_NetworkService.Model;
using PZ3_NetworkService.Ostalo;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace PZ3_NetworkService.ViewModel
{
    public class Tab2ViewModel : BindableBase
    {
        BindingList<Road> roads = new BindingList<Road>(StaticRoadList.StaticRoads); //Lb DataGrid
        Road selectedRoad = new Road();                                             //objekat datagrid-a
        Road currentRoad = new Road();                                              // koji se trenutno unosi 
        List<String> types = new List<String>() { "IA", "IB" }; 
        bool isTypeChecked; 
        string searchContent; 

        private UserControl userControl;
        public UserControl UserControl
        {
            get
            {
                return userControl;
            }
            set
            {
                userControl = value;
                OnPropertyChanged("UserControl");
            }
        }
        public static MyICommand<object> ToolTipVisible { get; set; }

        public BindingList<Road> Roads
        {
            get { return roads; }
            set
            {
                if (this.roads != value)
                {
                    this.roads = value;
                    OnPropertyChanged("Roads");
                }
            }
        }

        public Road CurrentRoad
        {
            get { return currentRoad; }
            set
            {
                if (this.currentRoad != value)
                {
                    this.currentRoad = value;
                    OnPropertyChanged("CurrentRoad");
                }
            }
        }

        public Road SelectedRoad
        {
            get { return selectedRoad; }
            set
            {
                if (this.selectedRoad != value)
                {
                    this.selectedRoad = value;
                    OnPropertyChanged("selectedRoad");
                }
            }
        }

        public List<String> Types
        {
            get { return types; }
            set
            {
                if (this.types != value)
                {
                    this.types = value;
                    OnPropertyChanged("Types");
                }
            }
        }

        public bool IsTypeChecked
        {
            get { return isTypeChecked; }
            set
            {
                if (this.isTypeChecked != value)
                {
                    this.isTypeChecked = value;
                    OnPropertyChanged("IsTypeChecked");
                }
            }
        }
        public string SearchContent
        {
            get { return searchContent; }
            set
            {
                if (this.searchContent != value)
                {
                    this.searchContent = value;
                    OnPropertyChanged("SearchContent");
                }
            }
        }
        public MyICommand AddCommand { get; set; } 

        public MyICommand SearchCommand { get; set; } 
        public MyICommand RemoveCommand { get; set; } 
        public static MyICommand<string> ValueChangedCommand { get; set; } 

        public Tab2ViewModel(UserControl u)
        {
            UserControl = u;
            ToolTipVisible = new MyICommand<object>(OnToolTipVisible);
            
            AddCommand = new MyICommand(OnAdd);
            SearchCommand = new MyICommand(OnSearch);
            RemoveCommand = new MyICommand(OnRemove);
            ValueChangedCommand = new MyICommand<string>(OnValueChange);
        }

        public void OnAdd()
        {
           
            CurrentRoad.Validate();
            if (CurrentRoad.IsValid)
            {
                Road Road = new Road(currentRoad.UserId, currentRoad.RoadNum, currentRoad.RoadType.Name);
                StaticRoadList.StaticRoads.Add(Road);
                Roads = new BindingList<Road>(StaticRoadList.StaticRoads);
                SearchContent = "";
                CurrentRoad = new Road();
                Tab1ViewModel.ValueChangedCommand.Execute($"A_{StaticRoadList.StaticRoads.Count() - 1}"); 
            }


        }
        public void OnSearch()
        {
            
            if (!String.IsNullOrWhiteSpace(SearchContent))
            {
                Roads = new BindingList<Road>();
                //Name
                if (!IsTypeChecked)
                {
                    foreach (Road item in StaticRoadList.StaticRoads)
                    {
                        if (item.RoadNum.Contains(searchContent))
                            Roads.Add(item);
                    }
                }
                //Type
                else
                {
                    foreach (Road item in StaticRoadList.StaticRoads)
                    {
                        if (item.RoadType.Name.Contains(searchContent))
                            Roads.Add(item);
                    }
                }
            }
            else
            {
                
                Roads = new BindingList<Road>(StaticRoadList.StaticRoads);
            }

            SearchContent = "";
            CurrentRoad = new Road();
        }

        public void OnRemove()
        {
            //selected?
            int idx = -1;
            int userID = selectedRoad.UserId;

            for (int i = 0; i < StaticRoadList.StaticRoads.Count(); i++)
            {
                if (StaticRoadList.StaticRoads[i].UserId.Equals(selectedRoad.UserId))
                    idx = i;
            }

            StaticRoadList.StaticRoads.RemoveAt(idx);
            Roads = new BindingList<Road>(StaticRoadList.StaticRoads);



            Tab1ViewModel.ValueChangedCommand.Execute($"Remove_{userID}"); 

            SearchContent = "";
            CurrentRoad = new Road();
        }

        public void OnValueChange(string value)
        {
            //1_123
            //promena vrednosti na 123
            String[] parts = value.Split('_');
            try
            {
                Roads[int.Parse(parts[0])].Value = int.Parse(parts[1]);
            }
            catch { }
        }

        private bool _isToolTipVisible = true;
        public void OnToolTipVisible(object o)
        {
            Style style = new Style(typeof(ToolTip));
            style.Setters.Add(new Setter(UIElement.VisibilityProperty, Visibility.Collapsed));
            style.Seal();


            if (_isToolTipVisible)
            {
                UserControl.Resources.Add(typeof(ToolTip), style); //hide
                _isToolTipVisible = false;
            }
            else
            {
                UserControl.Resources.Remove(typeof(ToolTip)); //show
                _isToolTipVisible = true;
            }

        }

    }
}
