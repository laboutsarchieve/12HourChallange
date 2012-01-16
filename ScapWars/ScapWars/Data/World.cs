using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using ScrapWars.Object;
using ScrapWars.Object.Actors;
using ScrapWars.Map;
using Microsoft.Xna.Framework;
using ScrapWars.Items;
using Microsoft.Xna.Framework.Graphics;

namespace ScrapWars.Data
{
    class World
    {
        GameMap worldMap;
        Player mainPlayer;

        List<Bullet> bulletList;
        List<Vector2> recentHitList;
        List<Enemy> enemyList;        

        Random rng;

        bool gameOver;       

        int maxEnemies;
        
        public World( )
        {
            bulletList = new List<Bullet>( );
            recentHitList = new List<Vector2>( );
            enemyList = new List<Enemy>( );
            Paused = false;

            rng = new Random( );

            maxEnemies = 45;
        }

        public GameMap WorldMap
        {
            get { return worldMap; }
            set 
            { 
                worldMap = value; 
                MainPlayer.TeleportTo(new Vector2(worldMap.SpawnPoint.X,worldMap.SpawnPoint.Y));
            }
        } 

        public void FireBullet( )
        {
            Bullet bullet = mainPlayer.FireBullet( );
            if( bullet != null )
                bulletList.Add( bullet );
        }
        internal void StabSword()
        {
            Stabbing = true;

            Point currPoint;
            List<Point> testedPoints = new List<Point>( );

            for( float along = 0.0f; along < mainPlayer.LeftArm.SwordLength; along += 0.25f )
            {
                currPoint = Vector2ToPoint(mainPlayer.Center + along * mainPlayer.Direction);
                if( worldMap.IsDestructableHere( currPoint ) )
                {
                    if( !testedPoints.Contains( currPoint ) )
                    {
                        worldMap.DamageObject(currPoint, mainPlayer.LeftArm.SwordDamage);
                        recentHitList.Add( new Vector2(currPoint.X, currPoint.Y) );
                        testedPoints.Add(currPoint);                        
                    }
                }
            }
        }
        public void Update(float deltaTime)
        { 
            float deltaSeconds = deltaTime/1000.0f;

            if( enemyList.Count < maxEnemies )
            {
                SpawnEnemy( );
            }
            if( !Paused )
            {
                UpdateEnemies(deltaSeconds);
                UpdatePlayer(deltaSeconds);
                UpdateBullets(deltaSeconds);
            }
        }
        private void SpawnEnemy()
        {            
            Vector2 enemyPos = new Vector2(rng.Next( 0, worldMap.Size.X ),rng.Next( 0, worldMap.Size.X ));
            Texture2D enemyTexture = Enemy.EnemyTextures[rng.Next(0,2)];

            enemyList.Add( new Enemy( enemyPos, enemyTexture, new Vector2( 1,1 ), mainPlayer ));
        }
        private void UpdateEnemies(float deltaTime)
        {
            Bullet bullet;
            foreach( Enemy enemy in enemyList )
            {
                bullet = enemy.Update( deltaTime );

                if( bullet != null )
                    bulletList.Add( bullet );
            }
        }
        private void UpdatePlayer(float deltaTime)
        {
            Vector2 origPosition = MainPlayer.Center;
            Point oldPoint;
            Point newPoint;
            Tile currTile;
            Tile newTile;

            oldPoint = Vector2ToPoint( MainPlayer.Center );

            currTile = worldMap.GetTile(oldPoint);
            MainPlayer.Update( deltaTime, currTile );                

            newPoint = Vector2ToPoint(MainPlayer.Center);
            newTile = worldMap.GetTile(newPoint);

            if( MainPlayer.Legs.MoveQualityOn( newTile ) == MoveQuality.none ||
                worldMap.IsDestructableHere(newPoint) )
                MainPlayer.TeleportTo( origPosition );

        }
        private void UpdateBullets(float deltaTime)
        {
            double distance;
            List<Bullet> toRemoveBullet = new List<Bullet>( );
            List<Enemy> toRemoveEnemy = new List<Enemy>( );

            foreach( Bullet bullet in bulletList )
            {
                bullet.Update( deltaTime );
                Point newPos = Vector2ToPoint(bullet.Center);

                if( worldMap.IsDestructableHere(newPos))
                {
                    worldMap.DamageObject(newPos, bullet.Damage);
                    recentHitList.Add( bullet.Center );
                    toRemoveBullet.Add( bullet );
                }

                if( !bullet.PlayerBullet )
                {
                    distance =  Graph.GraphMath.DistanceBetweenVector2s( new Point((int)bullet.Center.X, (int)bullet.Center.Y),
                                                                         new Point((int)mainPlayer.Center.X,(int)mainPlayer.Center.Y) );

                    if( distance < 1.5 )
                    {
                        if( mainPlayer.takeDamage( bullet.Damage ) )
                        {
                            Paused = true;
                            gameOver = true;                            
                        }
                        toRemoveBullet.Add(bullet);
                    }
                }
                else
                {
                    foreach( Enemy enemy in enemyList )
                    {
                        if( enemy.Center.X - bullet.Center.X < 1 &&
                            enemy.Center.Y - bullet.Center.Y < 1 )
                        {
                            if( enemy.takeDamage( bullet.Damage ) )
                            {                                
                                toRemoveEnemy.Add(enemy);

                                if( rng.NextDouble( ) > 0.5) 
                                    mainPlayer.AvailableParts.Add( DropPart( ) );
                            }
                            recentHitList.Add( bullet.Center );
                            toRemoveBullet.Add(bullet);
                        }
                    }
                }

                if( !worldMap.IsInside(bullet.Center) )
                    toRemoveBullet.Add( bullet );
            }

            foreach( Bullet bullet in toRemoveBullet )
            {
                bulletList.Remove(bullet);
            }
            foreach( Enemy enemy in toRemoveEnemy )
                enemyList.Remove(enemy);
        }

        private Part DropPart()
        {
            Part newPart;
            int type = rng.Next( 1, 6 );

            PartClass partClass = GetLeveledClass( );

            switch( type )
            {
                case 1:
                    newPart = HeadGenerator.GenerateRandomHead(partClass);
                    break;
                case 2:
                    newPart = CoreGenerator.GenerateRandomCore(partClass);
                    break;
                case 3:
                    newPart = LeftArmGenerator.GenerateLeftArm(partClass);
                    break;
                case 4:
                    newPart = RightArmGenerator.GenerateRightArm(partClass);
                    break;
                case 5:
                    newPart = LegGenerator.GenerateRandomLegs( partClass );
                    break;
                default:
                    newPart = new Head( "ERROR", PartClass.ClassFive, EnemyScanAbility.None, 0.0f );
                    break;
            }

            return newPart;
        }

        private PartClass GetLeveledClass()
        {
            int playerClass = mainPlayer.PlayerLevel;

            playerClass += (int)Math.Pow( -1, rng.Next(1,3) );

            playerClass = Math.Min( 5, playerClass );
            playerClass = Math.Max( 1, playerClass );

            return (PartClass)playerClass;
        }
        public bool IsPlayerOnFactory( )
        {
            Point playerPos = new Point((int)MainPlayer.Center.X,(int)MainPlayer.Center.Y);

            return worldMap.IsFactoryHere( playerPos );
        }
        public Point Vector2ToPoint( Vector2 vec )
        {
            return new Point( (int)vec.X, (int)vec.Y );
        }
        public Player MainPlayer
        {
            get { return mainPlayer; }
            set { mainPlayer = value; }
        }        
        internal List<Enemy> EnemyList
        {
            get { return enemyList; }
        }
        public List<Bullet> BulletList
        {
            get { return bulletList; }
        }
        public List<Vector2> RecentHitList
        {
            get { return recentHitList; }
        }
        public bool GameOver
        {
            get { return gameOver; }
            set { gameOver = value; }
        }

        public bool Stabbing { get; set; }
        public bool Paused{ get; set; }

        public void Reset()
        {
            MainPlayer = new Player( Vector2.Zero );

            MapCreator mapc = new MapCreator( );

            gameOver = false;
            Paused = false;
            WorldMap = mapc.CreateMap( );
        }
    }
}
