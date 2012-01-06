using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;

namespace ScapWars.Map
{
    class MapExport
    {
        public static Texture2D TexFromMap( GraphicsDevice graphics, GameMap theMap )
        {
            Texture2D newTexture = new Texture2D( graphics, theMap.Size.X,theMap.Size.Y);

            Color[] texColors = new Color[theMap.Size.X*theMap.Size.Y];
            Color currColor;

            int pixelNumber;

            for( int x = 0; x < theMap.Size.X; x++ )
            {
                for( int y = 0; y < theMap.Size.Y; y++ )
                {
                    pixelNumber = x*theMap.Size.Y+y;

                    currColor = ColorFromTile(theMap.GetTile(new Point(x,y)));
                    texColors[pixelNumber] = currColor;
                }
            }

            newTexture.SetData<Color>(texColors);

            return newTexture;
        }

        private static Color ColorFromTile( Tile theTile )
        {
            Color theColor;

            switch( theTile )
            {
                case Tile.Dirt:
                    theColor = Color.Brown;
                    break;
                case Tile.Grass:
                    theColor = Color.Green;
                    break;
                case Tile.Sand:
                    theColor = Color.Yellow;
                    break;
                case Tile.Water:
                    theColor = Color.Blue;
                    break;
                case Tile.Lava:
                    theColor = Color.Red;
                    break;
                case Tile.Volcano:
                    theColor = Color.Black;
                    break;
                default:
                    theColor = Color.Magenta;
                    break;
            }

            return theColor;
        }       
    }
}
