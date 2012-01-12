using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ScrapWars.Items
{
    class RightArm : Part
    {
        float projectileSize;
        int damage;
        int bulletTexureNum;

        public RightArm( String partName, PartClass partRank, float projectileSize, int damage, int bulletTexureNum ) 
            : base ( partName, partRank )
        {
            this.projectileSize = projectileSize;
            this.damage = damage;
            this.bulletTexureNum = bulletTexureNum;
        }
    }
}
