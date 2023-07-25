using PZ3_NetworkService.Ostalo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PZ3_NetworkService.Model
{
    
    public class Road : ValidationBase //zbog validacije polja pri unosu novog puta
    {
        
        int userId; 
        string roadNum; 
        Type roadType; 
        double value;

        public int UserId
        {
            get { return userId; }
            set
            {
                if (userId != value)
                {
                    userId = value;
                    OnPropertyChanged("UserId");    //azuriranje
                }
            }
        }
        public string RoadNum
        {
            get { return roadNum; }
            set
            {
                if (roadNum != value)
                {
                    roadNum = value;
                    OnPropertyChanged("RoadNum");   
                }
            }
        }

        public Type RoadType
        {
            get { return roadType; }
            set
            {
                if (roadType != value)
                {
                    roadType = value;
                    OnPropertyChanged("RoadType");  
                }
            }
        }

        public double Value
        {
            get { return value; }
            set
            {
                if (this.value != value)
                {
                    this.value = value;
                    OnPropertyChanged("Value");  
                }
            }
        }


        public Road(int id, string roadNum, string type)
        {

            //klijentski ID korisnik sam bira 
            this.UserId = id;
            this.RoadNum = roadNum;
            
            this.RoadType = new Type(type);
        }

        public Road() { this.RoadType = new Type(); }


        protected override void ValidateSelf()
        {
           
            bool valid = true;
            for (int i = 0; i < StaticRoadList.StaticRoads.Count(); i++) //ID postoji?
            {
                if (StaticRoadList.StaticRoads[i].userId.Equals(UserId))
                {
                    valid = false;
                    break;
                }
            }

            if (UserId <= 0 || string.IsNullOrWhiteSpace(this.UserId.ToString())) 
            {
                this.ValidationErrors["UserId"] = "UserId cannot be empty.";
            }
            else if (!valid)
            {
                this.ValidationErrors["UserId"] = "UserId alredy exists.";
            }
            if (string.IsNullOrWhiteSpace(this.RoadNum))
            {
                this.ValidationErrors["RoadNum"] = "RoadNum cannot be empty."; 
            }
            RoadType.Validate();
        }

    }
}
