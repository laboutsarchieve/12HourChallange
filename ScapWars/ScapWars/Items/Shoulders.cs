using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ScrapWars.Items
{
    class Shoulders : Part
    {
        float missleSize;
        int damage;
        int missleTexureNum;
                
        public Shoulders( String partName, PartClass partRank, float missleSize, int damage, int missleTexureNum ) 
            : base ( partName, partRank )
        {
            this.missleSize = missleSize;
            this.damage = damage;
            this.missleTexureNum = missleTexureNum;
        }

        public float MissleSize
        {
            get { return missleSize; }
            set { missleSize = value; }
        }        
        public int Damage
        {
            get { return damage; }
            set { damage = value; }
        }       
        public int MissleTexureNum
        {
            get { return missleTexureNum; }
            set { missleTexureNum = value; }
        }
    }
}
