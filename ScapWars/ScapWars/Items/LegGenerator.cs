using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ScrapWars.Items
{
    class LegGenerator : PartGenerator
    {
        static public Legs GenerateRandomLegs( PartClass partClass )
        {
            String name;
            float speedTilesPerSecond;
            MovementType movement;
                       
            speedTilesPerSecond = DecideSpeedTilesPerSecond( partClass );
            movement = DecideMoveType( partClass );

            name = ChooseLegName( partClass, speedTilesPerSecond, movement );

            return new Legs( name, partClass, speedTilesPerSecond, movement );
        }
        static private String ChooseLegName( PartClass partClass,
                                             float speed,
                                             MovementType move )
        {
            const String LEG_NAME_LETTERS = "lgsfsvvcx";

            String name;
            name = ChooseFromLetters( LEG_NAME_LETTERS, 8 );

            name += "-" + (int)partClass;

            if( speed > 2 )
                name += "(MTR)";
            else
                name += "(MUD)";

            if( move.Water != MoveQuality.none )
                name += "~FSH";

            if( move.Lava != MoveQuality.none )
                name += "`HTT`";

            return name;
        }
        static private float DecideSpeedTilesPerSecond( PartClass partClass )
        {
            float speedTilesPerSecond;
            speedTilesPerSecond = Rng.Next( 0, (int)partClass );
            speedTilesPerSecond *= (float)Rng.NextDouble()/2;

            speedTilesPerSecond = Math.Max( speedTilesPerSecond, 0.2f );

            return speedTilesPerSecond;
        }
        static private MovementType DecideMoveType( PartClass partClass )
        {
            MovementType movement = new MovementType( );
            int classNum = (int)partClass;

            if( classNum > 2 )
            {
                movement.Dirt = (MoveQuality)Rng.Next( 1, (classNum+1)/2 );
                movement.Grass = (MoveQuality)Rng.Next( 1, (classNum+1)/2 );                               
                movement.Sand = (MoveQuality)Rng.Next( 0, (classNum+1)/2 );
                movement.Water = (MoveQuality)Rng.Next( 0, (classNum+1)/2 );
                movement.Lava = (MoveQuality)Rng.Next( 0, (classNum-1)/2 );                
            }
            else
            {
                movement.Dirt = (MoveQuality)Rng.Next( 1, 3 );
                movement.Grass = (MoveQuality)Rng.Next( 1, 3 );
                movement.Sand = (MoveQuality)Rng.Next( 0, 2 );
                movement.Water = (MoveQuality)Rng.Next( 0, classNum );
                movement.Lava = MoveQuality.none;
            }           

            return movement;
        }
    }
}
