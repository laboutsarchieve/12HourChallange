using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;

namespace ScrapWars.Items
{
    class RightArmGenerator : PartGenerator
    {
        public static List<Texture2D> bulletTextureList;

        static public RightArm GenerateRightArm( PartClass partClass )
        {
            String name;
            int velocity;
            float timeToFire;

            int damage;

            damage = 3 + (int)((int)partClass/2.0 * Rng.Next( 1, 6 ));
                       
            velocity = 4 + Rng.Next( 1,2+(int)partClass );

            float size = 0.1f * damage * (float)Rng.NextDouble( );

            size = Math.Max( 0.2f, size );
            size = Math.Min( 2, size );

            timeToFire = Rng.Next( 500 - 50 * (int)partClass, 1000 - 100 * (int)partClass );

            name = ChooseArmName( partClass, velocity, damage  );

            return new RightArm( name, partClass, size, velocity, timeToFire, damage, bulletTextureList[0] );
        }

        static private String ChooseArmName( PartClass partClass,
                                             int velo,
                                             int damage )
        {
            const String ARM_NAME_LETTERS = "rrruib";

            String name;
            name = ChooseFromLetters( ARM_NAME_LETTERS, 4 );

            name += "-" + (int)partClass;

            if( velo > 8 )
                name += "-MCH";
            else
                name += "-PST";

            if( damage > 4 )            
                name += "!BMB";            
            else
                name += "!PEA";
            
            return name;
        }
    }
}
