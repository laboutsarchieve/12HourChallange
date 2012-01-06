using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

namespace ScapWars.Map
{
    enum Tile
    {
        Dirt,
        Grass,
        Sand,
        Water,
        Lava,
        Air
    }

    class Map
    {
        public Tile[,] tileMap; 

        public Map( Point size )
        {
            tileMap = new Tile[ size.X, size.Y ];
            Size = size;            
        }

        public Tile GetTile( Point loc )
        {
            return tileMap[loc.X,loc.Y];
        }

        public void SetTile( Point loc, Tile tile )
        {
            tileMap[loc.X,loc.Y] = tile;
        }

        public Point Size{ get; private set; }
    }
}
