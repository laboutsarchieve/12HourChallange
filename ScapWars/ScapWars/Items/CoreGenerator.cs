using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ScrapWars.Items
{
    class CoreGenerator : PartGenerator
    {
        static public Core GenerateRandomCore( PartClass partClass )
        {            
            int classNum = (int)partClass;

            String name;
            int maxHp;       
            bool resistLava;

            maxHp = DecideMaxHP( partClass );

            resistLava = DecideResistLava( partClass );            
                
            name = ChooseCoreName( partClass, maxHp, resistLava );

            return new Core( "", partClass, maxHp, resistLava);
        }

        static private String ChooseCoreName( PartClass partClass, int maxHp, bool resistLava )
        {
            const String CORE_NAME_LETTERS = "csuokjmxz";

            String name;
            name = ChooseFromLetters( CORE_NAME_LETTERS, 8 );

            name += "-" + (int)partClass;

            if( resistLava )
                name += "H+";

            name+= "-" + ChooseFromLetters( CORE_NAME_LETTERS, 3 );
            return name;
        }
        static private int DecideMaxHP( PartClass partClass )
        {
            return (int)(Rng.Next(1,10)/2.0 * (int)partClass);
        }
        static private bool DecideResistLava( PartClass partClass )
        {
            double resistLavaChance = -0.4 + 0.2*(int)partClass;

            return (Rng.NextDouble( ) < resistLavaChance);
        }
    }
}
