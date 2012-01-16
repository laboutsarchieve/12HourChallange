using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace ScrapWars.Object
{
    class Bullet : TileObject
    {
        const int TILE_SIZE = 24; // bad duplication

        int damage;        

        float size;
        int velocity;        

        Vector2 direction;

        bool playerBullet; 

        public Bullet( Vector2 position, float bulletSize, int damage, int velocity, Vector2 direction, Texture2D bulletTexture, bool playerBullet ) 
            : base( position, new Vector2(bulletSize,bulletSize), bulletTexture )
        {
            this.damage = damage;

            size = bulletSize;
            this.velocity = velocity;            

            this.direction = direction;   
         
            this.playerBullet = playerBullet;

            BulletColor = Color.Yellow;
        }

        public int Damage
        {
            get { return damage; }
        }
        public void Update( float deltaTime )
        {
            center += deltaTime * velocity * direction;            
        }
        public bool PlayerBullet
        {
            get { return playerBullet; }
            set { playerBullet = value; }
        }

        public Color BulletColor { get; set; }

        public int Velocity { get{ return velocity; } }
    }
}
