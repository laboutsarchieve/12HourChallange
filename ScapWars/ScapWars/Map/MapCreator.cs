using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

using Noise2D;

namespace ScapWars.Map
{
    class MapCreator
    {
        public MapParams mapParams;

        private Random rng;

        public MapCreator( )
        {
            mapParams = new MapParams( );
            rng = new Random( );

            SetDefaultParams( );
        }

        public void SetDefaultParams( )
        {           
            // These magic numbers tend to produce decent maps

            mapParams.seed = rng.Next( );
            mapParams.size = new Point( 200, 200 );
            mapParams.octaves = 6;
            mapParams.persistence = 0.5;
            mapParams.frequencyMulti = 2;
            mapParams.zoomOut = 20;

            mapParams.waterLevel = 0.35;
            mapParams.sandLevel = 0.45;
            mapParams.grassLevel = 0.6;

            mapParams.maxRivers = 2;

            mapParams.volcanoRadius = 10;

            mapParams.minDistBossSpawn = 100;

            mapParams.percentGrassForested = .1;
            mapParams.percentDirtBoulder = .1;
        }

        public GameMap CreateMap( )    
        {
            ObjectPlacer objectPlacer;
            GameMap newMap = new GameMap(mapParams.size);

            rng = new Random( mapParams.seed );
            
            CreateBasics( newMap );
            AddRivers( newMap );
            CreateBossVolcano( newMap );

            objectPlacer = new ObjectPlacer( mapParams, rng );

            objectPlacer.PlaceObjects(newMap);

            return newMap;
        }

        private void CreateBasics( GameMap newMap )
        {
            double[,] noise = GetPositiveNoise( );            

            Tile currTile;

            for( int x = 0; x < newMap.Size.X; x++ )
            {
                for( int y = 0; y < newMap.Size.Y; y++ )
                {
                    currTile = TileFromValue(noise[x,y]);
                    newMap.SetTile(new Point(x,y), currTile); 
                }
            }
        }

        // PerlinNoise2D produces values from -1 to 1. This forces that noise into the range 0.0 to 1.0
        private double[,] GetPositiveNoise( )
        {
            double[,] noise = new double[mapParams.size.X, mapParams.size.Y];

            PerlinNoise2D.GenPerlinNoise( noise,
                                          mapParams.zoomOut,
                                          mapParams.frequencyMulti,
                                          mapParams.persistence,
                                          mapParams.octaves,
                                          mapParams.seed );

            for( int x = 0; x < mapParams.size.X; x++ )
            {
                for( int y = 0; y < mapParams.size.Y; y++ )
                {
                    noise[x,y] += 1;
                    noise[x,y] /= 2;
                }
            }

            return noise;
        }

        private Tile TileFromValue( double value )
        {
            Tile theTile;

            if( value < mapParams.waterLevel )
                theTile = Tile.Water;
            else if( value < mapParams.sandLevel )
                theTile = Tile.Sand;
            else if( value < mapParams.grassLevel )            
                theTile = Tile.Grass;
            else
                theTile = Tile.Dirt;
            
            return theTile;
        }

        private void AddRivers( GameMap newMap )
        {
            Point currLoc = new Point( );

            for( int k = 0; k < mapParams.maxRivers; k++ )
            {
                currLoc.X = rng.Next( newMap.Size.X-1 );
                currLoc.Y = rng.Next( newMap.Size.Y-1 );

                CreateRiver( newMap, currLoc );

            }
        }

        private void CreateRiver( GameMap newMap, Point start )
        {
            Point currLoc = start;

            Vector2 leadingEdge = new Vector2( (float)rng.NextDouble()-0.5f, (float)rng.NextDouble( )-0.5f);            

            leadingEdge.Normalize( );

            while( currLoc.X > -1 &&
                   currLoc.Y > -1 &&
                   currLoc.X < newMap.Size.X &&
                   currLoc.Y < newMap.Size.Y &&
                   newMap.GetTile( currLoc ) != Tile.Water )
            {
                newMap.SetTile( currLoc, Tile.Water );

                leadingEdge += new Vector2( ((float)rng.NextDouble()-0.5f)/1.5f, ((float)rng.NextDouble( )-0.5f)/1.5f );
                leadingEdge.Normalize( );

                currLoc.X += CalcChangeFromLead( leadingEdge.X );
                currLoc.Y += CalcChangeFromLead( leadingEdge.Y );
            }
        }

        private int CalcChangeFromLead( float lead )
        {
            if( lead > 0.33 )
                return 1;
            else if( lead > -0.33 )
                return 0;
            else
                return -1;
        }
        
        private void CreateBossVolcano( GameMap newMap )
        {
            Point volcanoCenter;

            do
            {
                volcanoCenter = new Point( rng.Next(mapParams.volcanoRadius, newMap.Size.X - mapParams.volcanoRadius),
                                           rng.Next(mapParams.volcanoRadius, newMap.Size.Y - mapParams.volcanoRadius) );
            }
            while( newMap.GetTile( volcanoCenter ) == Tile.Water || 
                   newMap.GetTile( volcanoCenter ) == Tile.Sand );

            newMap.BossPoint = volcanoCenter;

            int volcanoSquares = (int)Math.Pow(mapParams.volcanoRadius*Math.PI,2);

            int lavaSquares = volcanoSquares/4;
            
            CreateCircle( newMap, volcanoCenter, volcanoSquares, Tile.Volcano );
            CreateCircle( newMap, volcanoCenter, lavaSquares, Tile.Lava );

            
        }

        private void CreateCircle( GameMap newMap, Point center, int numSquares, Tile tileType )
        {
            int radius = (int)Math.Sqrt(numSquares/Math.PI);

            int currLineLength;

            Point currPoint;

            for( int k = 0; k < radius; k++ )
            {
                currLineLength = radius - 2*k;
                currPoint = new Point( center.X - currLineLength/2, center.Y+k );

                for( int j = 0; j < currLineLength; j++ )
                {
                    currPoint.X++;
                    newMap.SetTile( currPoint, tileType );
                }
            }
            for( int k = 0; k < radius; k++ )
            {
                currLineLength = radius - 2*k;
                currPoint = new Point( center.X - currLineLength/2, center.Y-k );

                for( int j = 0; j < currLineLength; j++ )
                {
                    currPoint.X++;
                    newMap.SetTile( currPoint, tileType );
                }
            }
        }


    }
}
