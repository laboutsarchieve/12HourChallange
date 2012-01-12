using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ScrapWars.Items
{
    class LeftArm : Part
    {
        float swordLength;
        int swordDamage;

        public LeftArm( String partName, PartClass partRank, float swordLength, int swordDamage ) : base ( partName, partRank )
        {
            this.swordLength = swordLength;
            this.swordDamage = swordDamage;
        }
    }
}
