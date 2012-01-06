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
            Random rng = new Random( );

            mapParams = new MapParams( );
        }

        public void SetDefaultParams( )
        {           
            // These magic numbers tend to produce decent maps

            mapParams.seed = rng.Next( );
            mapParams.size = new Point( 200, 200 );
            mapParams.octaves = 6;
            mapParams.persistence = 0.5;
            mapParams.frequencyMulti = 2;
            mapParams.zoomOut = 40;
        }

        public Map CreateMap( )    
        {
            Map newMap = new Map(mapParams.size);

            CreateBasics( newMap );

            return newMap;
        }

        private void CreateBasics( Map newMap )
        {
            double[,] noise = new double[mapParams.size.X, mapParams.size.Y];

            PerlinNoise2D.GenPerlinNoise( noise,
                                          mapParams.zoomOut,
                                          mapParams.frequencyMulti,
                                          mapParams.persistence,
                                          mapParams.octaves,
                                          mapParams.seed );

            Tile currTile;

            for( int x = 0; x < mapParams.size.X; x++ )
            {
                for( int y = 0; y < mapParams.size.Y; y++ )
                {
                    currTile = TileFromValue(noise[x,y]+0.5);
                    newMap.SetTile(new Point(x,y), currTile); 
                }
            }
        }

        private Tile TileFromValue( double value )
        {
            Tile theTile;

            if( value < 0.2 )
                theTile = Tile.Water;
            else if( value < 0.3 )
                theTile = Tile.Sand;
            else if( value < 0.6 )            
                theTile = Tile.Grass;
            else
                theTile = Tile.Dirt;
            
            return theTile;
        }
    }
}
