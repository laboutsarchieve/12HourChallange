using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ScrapWars.Items
{
    class ShoulderGenerator : PartGenerator
    {
        static public Shoulders GetRandomShoulder( PartClass partClass )
        {
            String name;

            name = "Shoudler" + (int)partClass;
            return new Shoulders( name, partClass, 1, 4, 0 );
        }
    }
}
