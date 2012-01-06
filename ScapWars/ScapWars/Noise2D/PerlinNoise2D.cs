using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Noise2D
{
    static class PerlinNoise2D
    {
        static public void GenPerlinNoise( double[,] toFill,
                                           int sqrtNumFeatures,
                                           double frequencyMulti,
                                           double persistence,
                                           int octaves,
                                           int seed )
        {
            GenPerlinNoise( toFill, sqrtNumFeatures, frequencyMulti, persistence, octaves, 0, 0, seed );
        }

        static public void GenPerlinNoise( double[,] toFill,
                                           int zoom,
                                           double frequencyMulti,
                                           double persistence,
                                           int octaves,
                                           int startingX,
                                           int startingY,
                                           int seed )
        {
            int width;
            int height;

            int effectiveX;
            int effectiveY;

            double regionSize;

            double frequency;
            double amplitude; 

            width  = toFill.GetLength(1);  
            height = toFill.GetLength(0);

            regionSize = width / zoom; 
        
            for( int x = 0; x < width; x++ )
            {
                for( int y = 0; y < height; y++ )
                {
                    effectiveX = x + startingX;
                    effectiveY = y + startingY;

                    frequency = 1;
                    amplitude = 1;

                    for( int oct = 0; oct < octaves; oct++ )
                    {                    
                        toFill[x,y] += SimpleNoise2D.GenInterpolatedNoise(effectiveX/(float)width * regionSize * frequency,
                                                                          effectiveY/(float)width * regionSize * frequency, seed) * amplitude;

                        frequency *= frequencyMulti;
                        amplitude *= persistence   ;
                    }       
                }
            }      
        }
    }
}
