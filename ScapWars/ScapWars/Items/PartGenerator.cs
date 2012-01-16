using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ScrapWars.Items
{
    // This BADLY needs to be changed to one generator per part when polishing
    class PartGenerator
    {
        static private Random rng;

        static protected String ChooseFromLetters(String charSelection, int maxLength)
        {
            String firstName = "";
            int totalChars = charSelection.Length;                       
            int numLetters = rng.Next( 1, maxLength );

            charSelection = charSelection.ToLower( );

            char nextLetter;
            for( int k = 0; k < numLetters; k++ )
            {             
                nextLetter = charSelection.ElementAt( rng.Next( 0, totalChars-1 ) );
                if( k == 0 )
                    Char.ToUpper(nextLetter);

                firstName += charSelection.ElementAt( rng.Next( 0, totalChars-1 ) );                
            }

            return firstName;
        }

        static protected Random Rng
        {
            get
            {
                if( rng == null )
                    rng = new Random( );

                return rng;
            }
        }
    }
}
