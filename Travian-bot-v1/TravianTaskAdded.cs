using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Travian_bot_v1
{
    public class TravianTaskAdded
    {
        public string Status, Time;
        public int level;
        public bool IsResource;
        public TravianTaskAdded(string Stat,string Time, int Level, bool IsResource)
        {
            this.Status = Stat;
            this.Time = Time;
            this.level = Level;
            this.IsResource = IsResource;
        }
    }
}
