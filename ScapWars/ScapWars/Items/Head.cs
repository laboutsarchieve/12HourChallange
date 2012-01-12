using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ScrapWars.Items
{
    enum EnemyScanAbility
    {
        None,
        Poor,
        Good,
        Excellent
    }
    class Head : Part
    {
        EnemyScanAbility enemyScanAbility;
        float missileInterceptionRate;
        
        public Head( String partName, PartClass partRank, EnemyScanAbility enemyScanAbility, float missileInterceptionRate ) 
            : base ( partName, partRank )
        {
            this.missileInterceptionRate = missileInterceptionRate;
            this.enemyScanAbility = enemyScanAbility;
        }
    }
}
