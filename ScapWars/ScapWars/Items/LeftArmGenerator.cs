using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ScrapWars.Items
{
    class LeftArmGenerator : PartGenerator
    {
        static public LeftArm GenerateLeftArm( PartClass partClass )
        {
            String name;

            

            float length;
            int damage;

            length = (float)(0.5f + Rng.NextDouble( )/2 * (int) partClass);
            damage = 10;

            name = ChooseArmName( partClass, length, damage );

            return new LeftArm( name, partClass, length, damage );
        }

        static private String ChooseArmName( PartClass partClass,
                                             float length,
                                             int damage )
        {
            const String ARM_NAME_LETTERS = "llleoid";

            String name;
            name = ChooseFromLetters( ARM_NAME_LETTERS, 4 );

            name += "-" + (int)partClass;

            if( length > 1.5 )
                name += "-LNG";
            else
                name += "-SRT";

            if( damage > 15 )            
                name += "^DTH";            
            else
                name += "^DLL";
            
            return name;
        }
    }
}
