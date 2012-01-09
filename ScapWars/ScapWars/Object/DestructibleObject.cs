using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace ScrapWars.Object
{
    abstract class DestructibleObject : TileObject
    {
        int hp;

        public DestructibleObject( Point centerTile, Point size, Texture2D objectTexture, int health ) 
            : base( centerTile, size, objectTexture )
        {
            hp = health;
        }

        public bool dealDamage( int damage )
        {
            hp -= damage;

            return hp < 1;
        }
        
        public int Hp
        {
            get { return hp; }
        }
    }
}
