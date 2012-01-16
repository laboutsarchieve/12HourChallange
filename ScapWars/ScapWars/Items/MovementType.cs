using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ScrapWars.Items
{
    enum MoveQuality
    {
        none = 0,
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

        static public MovementType BasicGround( )
        {
            MovementType groundMove = new MovementType( );

            groundMove.Grass = MoveQuality.normal;
            groundMove.Dirt = MoveQuality.normal;
            groundMove.Sand = MoveQuality.slow;
            groundMove.Water = MoveQuality.none;
            groundMove.Lava = MoveQuality.none;

            return groundMove;
        }
    }
}
