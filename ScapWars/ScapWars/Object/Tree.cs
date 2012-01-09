using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace ScrapWars.Object
{
    class Tree : DestructibleObject
    {
        const int TREE_MAX_HP = 100;

        static Texture2D treeTexture;

        public Tree( Point centerTile ) 
            : base( centerTile, new Point( 2, 2 ), treeTexture, TREE_MAX_HP )
        {
        }

        static public Texture2D TreeTexture
        {
            set
            {
                treeTexture = value;
            }
        }
    }
}
