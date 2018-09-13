using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Travian_bot_v1
{
    public class Building
    {
        public string href, name;
        public int level;
        public bool IsResource; // 0 building 1 field

        public Building(string name, string href, int level, bool IsResource)
        {
            this.name = name;
            this.href = href;
            this.level = level;
            this.IsResource = IsResource;
        }
    }

}
