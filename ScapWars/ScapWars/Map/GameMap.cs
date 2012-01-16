using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using ScrapWars.Object;
using Microsoft.Xna.Framework.Graphics;

namespace ScrapWars.Map
{
    enum Tile
    {
        Dirt,
        Grass,
        Sand,
        Water,
        Volcano,
        Lava,
        Air
    }

    class GameMap
    {
        Tile[,] tileMap;

        Point spawnPoint;
        Point bossPoint;  

        Point size;

        Dictionary<Point, DestructibleObject> destObjectsMap;
        Dictionary<Point, Factory> factoryMap;

        public GameMap( Point size )
        {
            tileMap = new Tile[ size.X, size.Y ];
            this.size = size;            

            destObjectsMap = new Dictionary<Point,DestructibleObject>( );
            factoryMap = new Dictionary<Point,Factory>( );
        }

        public void AddFactory( Point location )
        {            
            if( !factoryMap.ContainsKey( location ) )                
                factoryMap.Add( location, new Factory( new Vector2(location.X,location.Y) ) );
        }

        public Tile GetTile( Point loc )
        {
            return tileMap[loc.X,loc.Y];
        }

        public bool IsFactoryHere( Point loc )
        {
            const int FACTORY_SIZE = 5;

            for( int x = 0; x < FACTORY_SIZE; x++ )
            {
                for( int y = 0; y < FACTORY_SIZE; y++ )
                {
                    if( factoryMap.ContainsKey( new Point(loc.X-x, loc.Y-y) ))
                        return true;
                }
            }

            return false;
        }
        public bool IsDestructableHere( Point loc )
        {
            const int DEST_SIZE = 2;

            for( int x = 0; x < DEST_SIZE; x++ )
            {
                for( int y = 0; y < DEST_SIZE; y++ )
                {
                    if( destObjectsMap.ContainsKey( new Point(loc.X-x, loc.Y-y) ))
                        return true;
                }
            }

            return false;
        }
        public void DamageObject(Point location, int damage)
        {         
            const int DEST_SIZE = 2;
            for( int x = 0; x < DEST_SIZE; x++ )
            {
                for( int y = 0; y < DEST_SIZE; y++ )
                {
                    if( destObjectsMap.ContainsKey( new Point(location.X-x, location.Y-y) ) )
                    {
                        if( destObjectsMap[new Point(location.X-x, location.Y-y)].dealDamage(damage) )
                            destObjectsMap.Remove(new Point(location.X-x, location.Y-y));
                    }
                }
            }
            
        }

        public Texture2D GetTextureOfObject( Point upperLeft )
        {
            if( factoryMap.ContainsKey( new Point( upperLeft.X+2, upperLeft.Y+2 ) ) )
                return factoryMap[new Point( upperLeft.X+2, upperLeft.Y+2 )].Texture;
            
            if( destObjectsMap.ContainsKey( upperLeft ) )
                return destObjectsMap[upperLeft].Texture;
                       
            // I don't like this, change when polishing
            return null;
        }

        public void SetTile( Point loc, Tile tile )
        {
            tileMap[loc.X,loc.Y] = tile;
        }

        public void AddDestructibleObject(DestructibleObject theObject)
        {
            destObjectsMap.Add(new Point((int)theObject.Center.X,(int)theObject.Center.Y), theObject);
        }

        public bool IsInside(Vector2 vector2)
        {
            return ( vector2.X > -1 ||
                     vector2.Y > -1 ||
                     vector2.X < size.X ||
                     vector2.Y < size.Y );
                
        }
       
        public Point SpawnPoint
        {
            get { return spawnPoint; }
            set { spawnPoint = value; }
        }        

        public Point BossPoint
        {
            get { return bossPoint; }
            set { bossPoint = value; }
        }

        public Point Size
        { 
            get{ return size;} 
            private set { size = value; }
        }

        
    }
}
