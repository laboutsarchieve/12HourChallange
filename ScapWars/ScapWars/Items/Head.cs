using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ScrapWars.Items
{
    enum EnemyScanAbility
    {
        None = 0,
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

        public EnemyScanAbility EnemyScanAbility
        {
            get { return enemyScanAbility; }
            set { enemyScanAbility = value; }
        }
        

        public float MissileInterceptionRate
        {
            get { return missileInterceptionRate; }
            set { missileInterceptionRate = value; }
        }
    }
}
