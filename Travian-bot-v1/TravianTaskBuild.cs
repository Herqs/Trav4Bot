using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Travian_bot_v1
{
    public class TravianTaskBuild : Building
    {
        public string villageUrl, Status;
        public int DesiredLvl, villageId;

        public TravianTaskBuild(string name, string href, int level, string villageUrl, int VillageId, int desiredLvl, string status, bool IsResource) :base(name, href, level, IsResource) 
        {
            this.villageUrl = villageUrl;
            this.villageId = VillageId;
            this.DesiredLvl = desiredLvl;
            this.Status = status;
        }
    }
}
