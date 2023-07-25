using PZ3_NetworkService.Model;
using PZ3_NetworkService.Ostalo;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace PZ3_NetworkService.ViewModel
{
    public class Tab1ViewModel : BindableBase
    {
        #region Fields
        string directorium = @"C:\Users\Bojsa\Desktop\Nebojsa Vasic\HCI PSI NetworkService aplikacija\PZ3-NetworkService\PZ3-NetworkService\Slike"; 

        static List<Road> l = new List<Road>(StaticRoadList.StaticRoads); //razbija reference baze i lista za bindovanje
        private BindingList<Road> roads = new BindingList<Road>(l);       //bind on listview
        private bool dragging = false; 
        private string draggedItem; 
        private List<Canvas> canvases = new List<Canvas>();
        private Canvas currentCanvas = null; 
        #endregion

        #region Properties 
        public UserControl UserControl { get; set; }
        public Road SelectedRoad { get; set; }

        public BindingList<Road> Roads
        {
            get
            {
                return roads;
            }
            set
            {
                roads = value;
                OnPropertyChanged("Roads");
            }
        }
        #endregion

        #region Commands
        public MyICommand SelectionChangedCommand { get; set; } 
        public MyICommand<Canvas> FreeCommand { get; set; }

        public MyICommand MouseLeftButtonUpCommand { get; set; } 

        public MyICommand<Canvas> DragOverCommandSender { get; set; } 
        public MyICommand<DragEventArgs> DragOverCommandEvent { get; set; } 

        public MyICommand<Canvas> DropCommandSender { get; set; } 
        public MyICommand<DragEventArgs> DropCommandEvent { get; set; }
        public static MyICommand<String> ValueChangedCommand { get; set; } 
        #endregion
        
        public static MyICommand<object> ToolTipVisible { get; set; }

        public Tab1ViewModel(UserControl u)
        {
            UserControl = u;
            ToolTipVisible = new MyICommand<object>(OnToolTipVisible);

            SelectionChangedCommand = new MyICommand(OnSelectionChanged);
            MouseLeftButtonUpCommand = new MyICommand(OnMouseLeftButtonUp);
            DragOverCommandSender = new MyICommand<Canvas>(OnDragOverSender);
            DropCommandSender = new MyICommand<Canvas>(OnDropSender);
            FreeCommand = new MyICommand<Canvas>(OnFree);
            ValueChangedCommand = new MyICommand<string>(OnChangeValue);
            DragOverCommandEvent = new MyICommand<DragEventArgs>(OnDragOverEvent);
            DropCommandEvent = new MyICommand<DragEventArgs>(OnDropEvent);
        }

        #region Command Methods
        public void OnChangeValue(String value)
        {
            if (value.Split('_')[0].Equals("Remove")) //Update Liste
            {
                l = new List<Road>(StaticRoadList.StaticRoads);

                List<Road> curr = new List<Road>(Roads);
                Roads = new BindingList<Road>();

                foreach (Road item in l)
                {
                    if (curr.Contains(item))
                        Roads.Add(item);
                }
                
                foreach (Canvas item in canvases)
                {
                    if (((TextBlock)(item).Children[0]).Text.Equals(value.Split('_')[1]))
                    {
                        OnFree(item);
                        break;
                    }

                }
            }
            else if (value.Split('_')[0].Equals("A")) //Update list
            {
                l = new List<Road>(StaticRoadList.StaticRoads);

                List<Road> curr = new List<Road>(Roads);
                Roads = new BindingList<Road>();
                foreach (Road item in l)
                {
                    if (curr.Contains(item))
                        Roads.Add(item);
                }

                Roads.Add(StaticRoadList.StaticRoads[int.Parse(value.Split('_')[1])]);
            }
            else 
            {

                //1_272
                String[] parts = value.Split('_');
                String img;
                String userId = StaticRoadList.StaticRoads[int.Parse(parts[0])].UserId.ToString();
                String type = StaticRoadList.StaticRoads[int.Parse(parts[0])].RoadType.Name;
                for (int i = 0; i < canvases.Count; i++)
                {
                    UserControl.Dispatcher.Invoke((Action)(() => //change picture
                    {
                        if (((TextBlock)((Canvas)canvases[i]).Children[0]).Text.Equals(userId))
                        {
                            
                            if (type.Equals("IA"))
                            {
                                if (int.Parse(parts[1]) >= 0 && int.Parse(parts[1]) <= 15000)
                                {
                                    img = directorium+ @"\IA.png";
                                }
                                else
                                {
                                    img = directorium+ @"\warn.png";

                                }

                            }
                            else
                            {
                                if (int.Parse(parts[1]) >= 0 && int.Parse(parts[1]) <= 7000)
                                {
                                    img = directorium + @"\IB.png";
                                }
                                else
                                {
                                    img = directorium+ @"\warn.png";

                                }
                            }
                            //set picture in canvas
                            BitmapImage logo = new BitmapImage();
                            logo.BeginInit();
                            logo.UriSource = new Uri(img);
                            logo.EndInit();
                            canvases[i].Background = new ImageBrush(logo);
                        }
                    }));
                }
            }
        }

        public void OnSelectionChanged()
        {
            //Zapoceto prevlacenje 
            if (!dragging)
            {
                dragging = true;
                draggedItem = SelectedRoad.RoadType.ImgSrc;
                DragDrop.DoDragDrop(UserControl, draggedItem, DragDropEffects.Copy | DragDropEffects.Move);
            }
        }

        public void OnMouseLeftButtonUp()
        {
            //Zavrsetak prevlacenja
            draggedItem = null;
            SelectedRoad = null;
            dragging = false;
        }

        public void OnDropEvent(DragEventArgs e)
        {
            //Pri pustanju objekta na kanvas
            e.Handled = true;
        }

        public void OnDragOverEvent(DragEventArgs e)
        {
            //Pri prevlacenja objekta iznad kanvasa
            if (currentCanvas.Resources["taken"] != null)
            {
                ((DragEventArgs)(e)).Effects = DragDropEffects.None;
            }
            else
            {
                ((DragEventArgs)(e)).Effects = DragDropEffects.Copy;
            }
            ((DragEventArgs)(e)).Handled = true;
        }

        public void OnDragOverSender(Canvas e)
        {
            //Postavljanje trenutnog kanvasa na gde se trenutno mis nalazi
            currentCanvas = e;
        }

        public void OnDropSender(Canvas e)
        {
            //Pri pustanju objekta na kanvas
            if (draggedItem != null)
            {
                if (((Canvas)e).Resources["taken"] == null)
                {
                    BitmapImage logo = new BitmapImage();
                    logo.BeginInit();
                    logo.UriSource = new Uri(directorium+@"\" +draggedItem);
                    logo.EndInit();
                    ((Canvas)e).Background = new ImageBrush(logo);
                    ((TextBlock)((Canvas)e).Children[0]).Foreground = (SolidColorBrush)(new BrushConverter().ConvertFrom("#000000"));
                    ((TextBlock)((Canvas)e).Children[0]).Text = SelectedRoad.UserId.ToString();
                    ((TextBlock)((Canvas)e).Children[2]).Text = SelectedRoad.RoadNum;
                    ((Canvas)e).Resources.Add("taken", true);

                    canvases.Add((Canvas)e);
                    this.Roads.Remove(SelectedRoad);

                }
                SelectedRoad = null;
                dragging = false;
            }
        }

        public void OnFree(Canvas canvas)
        {
 
            canvases.Remove(canvas);
            int userId;
            try
            {
                userId = int.Parse(((TextBlock)(canvas).Children[0]).Text);
            }
            catch { return; }

            if (canvas.Resources["taken"] != null)
            {
                canvas.Background = Brushes.CadetBlue;
                ((TextBlock)canvas.Children[0]).Text = "Free";
                ((TextBlock)canvas.Children[2]).Text = "";
                ((TextBlock)canvas.Children[0]).Foreground = Brushes.Black;
                canvas.Resources.Remove("taken");
            }

            //Vracanje objekta u ListVIew 
            StaticRoadList.StaticRoads.ForEach(x =>
            {
                if (x.UserId.Equals(userId))
                    Roads.Add(x);
            });

            Roads = new BindingList<Road>(Roads.ToList().OrderBy(x => x.UserId).ToList());
        }
        #endregion
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
