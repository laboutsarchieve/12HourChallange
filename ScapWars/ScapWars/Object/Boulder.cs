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
        const int TREE_MAX_HP = 100;

        static Texture2D treeTexture;

        public Boulder( Point centerTile  ) 
            : base( centerTile, new Point( 1, 1 ), treeTexture, TREE_MAX_HP )
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
