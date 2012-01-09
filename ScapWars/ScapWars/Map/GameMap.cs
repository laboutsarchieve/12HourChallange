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

        Dictionary<Point, DestructibleObject> destObjectsMap;
        Dictionary<Point, Factory> factoryMap;

        public GameMap( Point size )
        {
            tileMap = new Tile[ size.X, size.Y ];
            Size = size;            

            destObjectsMap = new Dictionary<Point,DestructibleObject>( );
            factoryMap = new Dictionary<Point,Factory>( );
        }

        public void AddFactory( Point location )
        {            
            if( !factoryMap.ContainsKey( location ) )                
                factoryMap.Add( location, new Factory( location ) );
        }

        public Tile GetTile( Point loc )
        {
            return tileMap[loc.X,loc.Y];
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
            destObjectsMap.Add(theObject.Center, theObject);
        }

        public void DamageObject(Point location, int damage)
        {            
            if( destObjectsMap[location].dealDamage(damage) )
                destObjectsMap.Remove(location);
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

        public Point Size{ get; private set; }
    }
}
