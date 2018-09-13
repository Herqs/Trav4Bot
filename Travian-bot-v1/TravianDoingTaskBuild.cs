using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Travian_bot_v1
{
    public class TravianDoingTaskBuild
    {
        public TimeSpan TimeLeft;
        public int Level, VillageId;
        public bool IsResource;
        public string Name, url;

        public TravianDoingTaskBuild(TimeSpan TimeLeft, int Level, int VillageId, string Name, bool IsResource, string url)
        {
            this.TimeLeft = TimeLeft;
            this.Level = Level;
            this.VillageId = VillageId;
            this.Name = Name;
            this.IsResource = IsResource;
            this.url = url;
        }
    }
}
