using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ScrapWars.Items;

namespace ScrapWars.Object.Actors
{
    class Enemy : TileObject
    {
        static List<Texture2D> enemyTextures;        
        int Hp;
        
        Player thePlayer;
        Vector2 size;

        float timeTillFire;
        float timeBetweenFire;

        int avoidanceNum;
        int sight;

        float speed;
        Vector2 direction;

        Bullet bullet;     

        Random rng; // I don't like having an rng for each enemy, fix this

        public Enemy( Vector2 pos, Texture2D texture, Vector2 size, Player thePlayer ) : base (pos, size, texture )
        {
            rng = new Random( );
            avoidanceNum = rng.Next( 3, 5 );
            sight = rng.Next( avoidanceNum+10, avoidanceNum+15 );
            speed = rng.Next( 1, 5 );

            Hp = rng.Next( 2, 12 );

            int damage = rng.Next(1, 3);
            int bulletSpeed = rng.Next( 2, 6 );
            
            timeTillFire = 0;

            bullet = new Bullet( Vector2.Zero,
                                (1+(float)rng.NextDouble( ))/4,
                                damage,
                                bulletSpeed,
                                Vector2.Zero,
                                RightArmGenerator.bulletTextureList[0],
                                false);

            bullet.BulletColor = new Color( rng.Next(0,255), rng.Next(0,255), rng.Next(0,255) );

            timeBetweenFire = rng.Next( 100 + 25*bullet.Damage, 500 + 125*bullet.Damage );

            direction = Vector2.Zero;

            this.thePlayer = thePlayer;
        }

        public Bullet Update( float deltaTime )
        {
            center += deltaTime * speed * direction;
            PursuePlayer( );
            return ShootAtPlayer( deltaTime );
        }
        public bool takeDamage( int damage )
        {
            Hp -= damage;
            return Hp < 1;                
        }
        private Bullet ShootAtPlayer( float deltaTime )
        {
            timeTillFire -= deltaTime*1000;

            double distance =  Graph.GraphMath.DistanceBetweenVector2s( new Point((int)center.X, (int)center.Y),
                                                         new Point((int)thePlayer.Center.X,(int)thePlayer.Center.Y) );
            Vector2 towardPlayer = thePlayer.Center - center;
            Bullet bulletClone;

            if( distance < sight && timeTillFire < 1 )
            {
                timeTillFire = timeBetweenFire;
                bulletClone = new Bullet( center, bullet.SizeInTiles.X, bullet.Damage, bullet.Velocity, towardPlayer, bullet.Texture, false );                
                bulletClone.BulletColor = bullet.BulletColor;

                return bulletClone;
            }
            else
                return null;
        }

        public void PursuePlayer( )
        {
            double distance =  Graph.GraphMath.DistanceBetweenVector2s( new Point((int)center.X, (int)center.Y),
                                                         new Point((int)thePlayer.Center.X,(int)thePlayer.Center.Y) );
            if( distance < sight && (distance > avoidanceNum+1 || distance < avoidanceNum -1 ))
            {   
                if( distance > avoidanceNum )
                    direction = thePlayer.Center - center;
                else
                    direction = center - thePlayer.Center;
            }
            else
                direction = Vector2.Zero;
        }

        static public List<Texture2D> EnemyTextures
        {
            get { return enemyTextures; }
            set { enemyTextures = value; }
        }
    }
}
