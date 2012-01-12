using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ScrapWars.Items
{
    class Legs : Part
    {
        float speedTilePerSecond;
        MovementType movement;

        public Legs( String partName, PartClass partRank, float speed, MovementType moveType ) : base ( partName, partRank )
        {
        }

        
    }
}
