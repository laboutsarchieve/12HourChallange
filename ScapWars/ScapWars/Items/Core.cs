using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ScrapWars.Items
{
    class Core : Part
    {
        int maxHp;        
        bool resistLava;        

        public Core( String partName, PartClass partRank, int maxHp, bool resistLava ) : base ( partName, partRank )
        {
            this.maxHp = maxHp;
            this.resistLava = resistLava;
        }    

        public int MaxHp
        {
            get { return maxHp; }            
        }

        public bool ResistLava
        {
            get { return resistLava; }            
        }
    }
}
