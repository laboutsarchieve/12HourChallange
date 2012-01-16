using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ScrapWars.Map;

namespace ScrapWars.Items
{
    class Legs : Part
    {
        float speedTilePerSecond;

        MovementType movement;

        public Legs( String partName, PartClass partRank, float speedTilePerSecond, MovementType movement ) 
            : base ( partName, partRank )
        {
            this.speedTilePerSecond = speedTilePerSecond;
            this.movement = movement;
        }
        public MoveQuality MoveQualityOn( Tile tileType )
        {
            switch( tileType )
            {
                case Tile.Sand:
                    return movement.Sand;
                case Tile.Grass:
                    return movement.Grass;
                case Tile.Dirt:
                    return movement.Dirt;
                case Tile.Volcano:
                    return movement.Dirt;
                case Tile.Water:
                    return movement.Water;
                case Tile.Lava:
                    return movement.Lava;
                default:
                    return MoveQuality.none;
            }
        }
        public MovementType Movement
        {
            get { return movement; }
        }
        public float SpeedTilesPerSecond
        {
            get { return speedTilePerSecond; }
        }
    }
}
