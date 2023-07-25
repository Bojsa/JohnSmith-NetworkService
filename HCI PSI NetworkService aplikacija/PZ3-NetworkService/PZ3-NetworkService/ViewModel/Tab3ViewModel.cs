using PZ3_NetworkService.Model;
using PZ3_NetworkService.Ostalo;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace PZ3_NetworkService.ViewModel
{
    public class Tab3ViewModel : BindableBase
    {

        private int idContent = 1; 
        string SuperscriptDigits = "\u2070\u00b9\u00b2\u00b3\u2074\u2075\u2076\u2077\u2078\u2079"; 
        private ObservableCollection<Line> graphLines; //Lista Linije grafika
        private ObservableCollection<Text> graphTexts; // vreme
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
        public ObservableCollection<Line> GraphLines
        {
            get
            {
                return graphLines;
            }
            set
            {
                graphLines = value;
                OnPropertyChanged("GraphLines");
            }
        }
       


        public ObservableCollection<Text> GraphTexts
        {
            get
            {
                return graphTexts;
            }
            set
            {
                graphTexts = value;
                OnPropertyChanged("GraphTexts");
            }
        }

        public int IdContent
        {
            get { return idContent; }
            set
            {
                idContent = value;
                OnPropertyChanged("IdContent");
            }
        }


        public MyICommand PlotCommand { get; set; }
        public static MyICommand<object> ToolTipVisible { get; set; }
        
        public Tab3ViewModel(UserControl u)
        {
            UserControl = u;
            
            PlotCommand = new MyICommand(Plot);
            ToolTipVisible = new MyICommand<object>(OnToolTipVisible);
        }

        public void Plot()
        {
            //Open Log.txt
            DateTime dateTime = DateTime.Now - new TimeSpan(24, 0, 0);

            GraphLines = new ObservableCollection<Line>();
            GraphTexts = new ObservableCollection<Text>();
            string[] linesLog = File.ReadAllLines("Log.txt");
            List<double> values = new List<double>();
            List<DateTime> dates = new List<DateTime>();
            List<int> ids = new List<int>();
            bool isInLinst = false;
            int id = -1;
            //id exist?
            for (int i = 0; i < StaticRoadList.StaticRoads.Count(); i++)
            {
                if (StaticRoadList.StaticRoads[i].UserId.Equals(idContent))
                {
                    isInLinst = true;
                    id = i;
                }
            }


            //Filtriramo iz Log.txt po ID i vremenu unazad 24h BUKV CPY PAST
            for (int i = 0; i < linesLog.Count(); i++)
            {
                DateTime date = DateTime.Parse($"{linesLog[i].Split(':', '|')[5]}:{linesLog[i].Split(':', '|')[6]}:{linesLog[i].Split(':', '|')[7]}".Trim());
                if (int.Parse(linesLog[i].Split(':', '|')[1]).Equals(id) && date >= dateTime && isInLinst)
                {
                    values.Add(double.Parse(linesLog[i].Split(':', '|')[3]));
                    dates.Add(date);
                    ids.Add(int.Parse(linesLog[i].Split(':', '|')[1]));
                }
            }

            try
            {

                double distance = 1055 / (values.Count * 2);

                
                for (int i = 0; i < values.Count - 1; i++)
                {
                    string color = null;
                    
                    if (StaticRoadList.StaticRoads[ids[i]].RoadType.Name.Equals("IA"))
                    {
                        if (values[i] >= 0 && values[i] <= 15000)
                            color = "black";
                        else
                            color = "red";
                    }
                    else
                    {
                        if (values[i] >= 0 && values[i] <= 7000)
                            color = "black";
                        else
                            color = "red";
                    }

                    
                    GraphLines.Add(new Line()
                    {
                        X1 = distance * i * 2 + 5,
                        Y1 = 324 - values[i] / 61.728,
                        X2 = distance * (i + 1) * 2 + 5,
                        Y2 = 324 - values[i + 1] / 61.728,
                        Color = color
                    });
                    //Ispisujem vreme
                    GraphTexts.Add(new Text()
                    {
                        X = distance * i * 2 + 32,
                        Y = 0,
                        Width = distance + 8,
                        Height = 20,
                        Content = dates[i].Hour.ToString() + new string(dates[i].Minute.ToString().Select(x => SuperscriptDigits[x - '0']).ToArray()) //ovo je internet dao nama
                    });


                }
                //poslednji?
                GraphTexts.Add(new Text()
                {
                    X = distance * (dates.Count() - 1) * 2 + 45,
                    Y = 0,
                    Width = distance,
                    Height = 20,
                    Content = dates[(dates.Count() - 1)].Hour.ToString() + new string(dates[(dates.Count() - 1)].Minute.ToString().Select(x => SuperscriptDigits[x - '0']).ToArray())
                });
                //Y osa
                for (int i = 0; i <= 10; i++)
                {
                    GraphTexts.Add(new Text()
                    {
                        X = 0,
                        Y = (int)(i * 31) + 25,
                        Width = distance + 25,
                        Height = 20,
                        Content = (i * 2000).ToString()
                    });
                }
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
