using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace ScrapWars.Object
{
    abstract class TileObject
    {
        protected Vector2 center;
        Vector2 sizeInTiles;
        Texture2D texture;        

        public TileObject( Vector2 centerTile, Vector2 size, Texture2D objectTexture )
        {
            center = centerTile;
            sizeInTiles = size;
            texture = objectTexture;
        }

        public Vector2 Center
        {
            get { return center; }
        }
        public Vector2 SizeInTiles
        {
            get { return sizeInTiles; }
        } 
        public Texture2D Texture
        {
            get { return texture; }
        }
    }
}
