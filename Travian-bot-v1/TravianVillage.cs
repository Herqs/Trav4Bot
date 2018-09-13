using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Travian_bot_v1
{
    public class TravianVillage
    {
        public List<double> Resources;
        public List<long> Storage;
        public List<double> Production;
        public int Number;
        public string Name;
        public string Url;
        public List<Building> ResourceFields;
        public List<Building> Buildings;
        public Coordinates coordinates;

        public TravianVillage(string Name, string Url, Coordinates coord)
        {
            this.Name = Name;
            this.Url = Url;
            ResourceFields = new List<Building>();
            Buildings = new List<Building>();
            Resources = new List<double>();
            Storage = new List<long>();
            Production = new List<double>();
            this.coordinates = coord;
        }
    }
}
