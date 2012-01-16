using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;

namespace ScrapWars.Items
{
    class RightArm : Part
    {
        float projectileSize;
        int damage;
        Texture2D bulletTexure;

        int velocity;
        float timeToFire;        
        
        public RightArm( String partName, PartClass partRank, float projectileSize, int velocity,
                         float timeToFire, int damage, Texture2D bulletTexure ) 
            : base ( partName, partRank )
        {
            this.projectileSize = projectileSize;
            this.damage = damage;
            this.bulletTexure = bulletTexure;

            this.velocity = velocity;
            this.timeToFire = timeToFire;
        }

        public float ProjectileSize
        {
            get { return projectileSize; }
        }        

        public int Damage
        {
            get { return damage; }
        }        

        public Texture2D BulletTexure
        {
            get { return bulletTexure; }
        }

        public int Velocity { get { return velocity; } }
        public float TimeToFire { get { return timeToFire; } }
    }
}
