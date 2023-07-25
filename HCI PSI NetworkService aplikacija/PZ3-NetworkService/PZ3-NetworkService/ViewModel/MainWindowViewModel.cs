using PZ3_NetworkService.Ostalo;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace PZ3_NetworkService.ViewModel
{
    public class MainWindowViewModel
    {
        public MyICommand ToogleToolTip { get; set; }
        public MainWindowViewModel()
        {
            createListener();
            ToogleToolTip = new MyICommand(OnToogleToolTip);
        }
        public void OnToogleToolTip()
        {
            Tab1ViewModel.ToolTipVisible.Execute(null);
            Tab2ViewModel.ToolTipVisible.Execute(null);
            Tab3ViewModel.ToolTipVisible.Execute(null);
        }
        private void createListener()
        {
            var tcp = new TcpListener(IPAddress.Any, 25565);
            tcp.Start();

            var listeningThread = new Thread(() =>
            {
                while (true)
                {
                    var tcpClient = tcp.AcceptTcpClient();
                    ThreadPool.QueueUserWorkItem(param =>
                    {
                        //Prijem poruke
                        NetworkStream stream = tcpClient.GetStream();
                        string incomming;
                        byte[] bytes = new byte[1024];
                        int i = stream.Read(bytes, 0, bytes.Length);
                        //Primljena poruka je sacuvana u incomming stringu
                        incomming = System.Text.Encoding.ASCII.GetString(bytes, 0, i);

                        //Ukoliko je primljena poruka pitanje koliko objekata ima u sistemu -> odgovor
                        if (incomming.Equals("Need object count"))
                        {
                            //Response
                            /* Umesto sto se ovde salje count.ToString(), potrebno je poslati 
                             * duzinu liste koja sadrzi sve objekte pod monitoringom, odnosno
                             * njihov ukupan broj (NE BROJATI OD NULE, VEC POSLATI UKUPAN BROJ)
                             * */
                            Byte[] data = System.Text.Encoding.ASCII.GetBytes(StaticRoadList.StaticRoads.Count.ToString());
                            stream.Write(data, 0, data.Length);
                        }
                        else
                        {
                            //U suprotnom, server je poslao promenu stanja nekog objekta u sistemu
                            Console.WriteLine(incomming); //Na primer: "Objekat_1:272"

                            //################ IMPLEMENTACIJA ####################
                            // Obraditi poruku kako bi se dobile informacije o izmeni
                            // Azuriranje potrebnih stvari u aplikaciji
                            try
                            {
                                string[] parts = incomming.Split('_', ':'); 
                                StaticRoadList.StaticRoads[int.Parse(parts[1])].Value = int.Parse(parts[2]); // dodamo vrednost u bazu
                                File.AppendAllText(@"Log.txt", $"Road: {int.Parse(parts[1])}\t|Value: {int.Parse(parts[2])}\t|Time: {DateTime.Now}" + Environment.NewLine); 
                                Tab1ViewModel.ValueChangedCommand.Execute($"{parts[1]}_{parts[2]}");
                                Tab2ViewModel.ValueChangedCommand.Execute($"{parts[1]}_{parts[2]}");
                            }
                            catch { }
                        }
                    }, null);
                }
            });

            listeningThread.IsBackground = true;
            listeningThread.Start();
        }
    }
}
