using PZ3_NetworkService.Ostalo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PZ3_NetworkService.Model
{
    public class Type : ValidationBase
    {
        

        string name;
        string imgSrc;

        public string Name
        {
            get { return name; }
            set
            {

                //U zavisnosti koji put izabere
                if (name != value)
                {
                    name = value;
                    switch (name)
                    {
                        case ("IA"):
                            imgSrc = @"..\Slike\IA.png";
                            break;
                        case ("IB"):
                            imgSrc = @"..\Slike\IB.png";
                            break;

                    }
                    OnPropertyChanged("Name"); 
                }
            }
        }
        public string ImgSrc
        {
            get { return imgSrc; }
            set
            {
                if (imgSrc != value)
                {
                    imgSrc = value;
                    OnPropertyChanged("ImgSrc");
                }
            }
        }

        public Type(string name)
        {
            this.name = name;

            
            switch (name)
            {
                case ("IA"):
                    imgSrc = @"..\Slike\IA.png";
                    break;
                case ("IB"):
                    imgSrc = @"..\Slike\IB.png";
                    break;

            }
        }
        public Type() { }

        protected override void ValidateSelf()
        {
            
            if (string.IsNullOrWhiteSpace(this.Name)) // da li je prazan combobox?
            {
                this.ValidationErrors["Type"] = "Name type cannot be empty.";
            }
        }
    }
}
