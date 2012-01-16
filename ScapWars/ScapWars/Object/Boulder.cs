using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;

namespace ScrapWars.Object
{
    class Boulder : DestructibleObject
    {
        const int BOULDER_MAX_HP = 30;

        static Texture2D treeTexture;

        public Boulder( Vector2 centerTile  ) 
            : base( centerTile, new Vector2( 2, 2 ), treeTexture, BOULDER_MAX_HP )
        {
        }

        static public Texture2D BoulderTexture
        {
            set
            {
                treeTexture = value;
            }
        }
    }
}
