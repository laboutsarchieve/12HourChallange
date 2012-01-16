using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ScrapWars.Items
{
    class HeadGenerator : PartGenerator
    {
        static public Head GenerateRandomHead( PartClass partClass )
        {
            string name;
            EnemyScanAbility enemyScanAbility;
            float missileInterceptionRate;

            enemyScanAbility = DecideScanAbility( partClass );
            missileInterceptionRate = DecideMissleInterceptRate( partClass );

            name = ChooseHeadName( partClass, enemyScanAbility, missileInterceptionRate );
            
            return new Head( name, partClass, enemyScanAbility, missileInterceptionRate );
        }
        static private String ChooseHeadName( PartClass partClass,
                                              EnemyScanAbility enemyScanAbility,
                                              float missileInterceptionRate )
        {
            const String HEAD_NAME_LETTERS = "zbpquxd";

            String name;
            name = ChooseFromLetters( HEAD_NAME_LETTERS, 8 );

            name += "-" + (int)partClass;

            if( enemyScanAbility != EnemyScanAbility.None )
                name += "XXSMT";
            else
                name += "XXDMB";

            if( missileInterceptionRate > 0.5f )
                name += "+FST";
            else
                name += "+SLO";
                        
            return name;
        }
        static private EnemyScanAbility DecideScanAbility( PartClass partClass )
        {
            int scanNumber;
            scanNumber = 5;

            for( int k = 0; k < (int)partClass; k++ )
            {
                scanNumber -= Rng.Next(0,3);
            }

            scanNumber = Math.Min( scanNumber, 3 );
            scanNumber = Math.Max( scanNumber, 0 );

            return (EnemyScanAbility)scanNumber;
        }
        static private float DecideMissleInterceptRate( PartClass partClass )
        {
            return (float)Rng.NextDouble( )/(5-(int)partClass);
        }
    }
}
