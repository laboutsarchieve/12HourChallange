using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

namespace ScapWars.Map
{
    class MapParams
    {
        public int seed;
        public Point size;
        
        public int zoomOut;
        public double frequencyMulti;
        public double persistence;
        public int octaves;

        public double waterLevel;
        public double sandLevel;
        public double grassLevel;
        
        public int maxRivers;
        public int minRiverLength;
        public int maxRiverLength;

        public int volcanoRadius;

        public int minDistBossSpawn;

        public double percentGrassForested;
        public double percentDirtBoulder;

        public int numFactories;
    }
}
