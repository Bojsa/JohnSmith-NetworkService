using PZ3_NetworkService.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PZ3_NetworkService.Ostalo
{
    public static class StaticRoadList
    {
        //Baza svih puteva koje imamo u sistemu
        public static List<Road> StaticRoads = new List<Road>()
        {
            new Road(1,"Road One","IA"),
            new Road(2,"Road Two","IB"),
            new Road(3,"Road Three","IA"),
            new Road(4,"Road Four","IB"),
        };
    }
}
