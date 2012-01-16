using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace ScrapWars.Object
{
    class Factory : TileObject
    {
        static Texture2D factoryTexture;

        public Factory(Vector2 center) : base( center, new Vector2( 5,5 ), factoryTexture )
        {
        }

        static public Texture2D FactoryTexture
        {
            set
            {
                factoryTexture = value;
            }
        }
    }
}
