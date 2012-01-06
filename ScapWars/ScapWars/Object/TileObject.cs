using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace ScapWars.Object
{
    abstract class TileObject
    {
        Point center;
        Point sizeInTiles;
        Texture2D texture;        

        public TileObject( Point centerTile, Point size, Texture2D objectTexture )
        {
            center = centerTile;
            sizeInTiles = size;
            texture = objectTexture;
        }

        public Point Center
        {
            get { return center; }
        }
        public Point SizeInTiles
        {
            get { return sizeInTiles; }
        } 
        public Texture2D Texture
        {
            get { return texture; }
        }
    }
}
