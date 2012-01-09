using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Graph;
using ScrapWars.Object;

namespace ScrapWars.Map
{
    class ObjectPlacer
    {
        private MapParams mapParams;
        private Random rng;

        public ObjectPlacer( MapParams param, Random random )
        {
            mapParams = param;
            rng = random;
        }

        public void PlaceObjects(GameMap newMap)
        {
            SetSpawn( newMap );   

            PlaceTrees(newMap);
            PlaceBoulders(newMap);
            PlaceFactories(newMap);
        }

        public void PlaceTrees(GameMap newMap)
        {
            Point currPoint;
            for( int x = 0; x < newMap.Size.X; x++ )
            {
                for( int y = 0; y < newMap.Size.Y; y++ )
                {
                    currPoint = new Point(x,y);

                    if( newMap.GetTile(currPoint) != Tile.Grass )
                        continue;

                    if( rng.NextDouble( ) < mapParams.percentGrassForested )
                        newMap.AddDestructibleObject( new Tree( currPoint ) );
                }
            }
        }

        public void PlaceBoulders(GameMap newMap)
        {
            Point currPoint;
            for( int x = 0; x < newMap.Size.X; x++ )
            {
                for( int y = 0; y < newMap.Size.Y; y++ )
                {
                    currPoint = new Point(x,y);

                    if( newMap.GetTile(currPoint) != Tile.Dirt )
                        continue;

                    if( rng.NextDouble( ) < mapParams.percentDirtBoulder )
                        newMap.AddDestructibleObject( new Boulder( currPoint ) );
                }
            }
        }

        public void PlaceFactories(GameMap newMap)
        {
            Point currPoint = new Point( );

            int factoriesPlaced = 0;

            while( factoriesPlaced < mapParams.numFactories )
            {
                currPoint.X = rng.Next( 2, newMap.Size.X-1 );
                currPoint.Y = rng.Next( 2, newMap.Size.Y-1 );

                if( newMap.GetTile(currPoint) == Tile.Dirt )
                {
                    newMap.AddFactory( currPoint );

                    factoriesPlaced++;
                }
            }
        }



        private void SetSpawn( GameMap newMap )
        {
            const int TIMEOUT = 500;
            int trys = 0;

            Point spawn;

            do
            {
                spawn = new Point(rng.Next(0, newMap.Size.X-1),rng.Next(0, newMap.Size.Y-1));
                trys++;
            }
            while( newMap.GetTile(spawn) != Tile.Water &&
                   GraphMath.DistanceBetweenPoints( spawn, newMap.BossPoint ) > mapParams.minDistBossSpawn &&
                   trys < TIMEOUT );
                   
            newMap.SpawnPoint = spawn;            
        }        
    }
}
