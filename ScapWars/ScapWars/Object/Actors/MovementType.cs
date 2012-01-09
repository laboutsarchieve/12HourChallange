using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ScrapWars.Object.Actors
{
    enum MoveQuality
    {
        none,
        slow,
        normal,
        fast
    }
    class MovementType
    {        
        public MoveQuality Grass;
        public MoveQuality Dirt;
        public MoveQuality Sand;
        public MoveQuality Water;
        public MoveQuality Lava;
    }
}
