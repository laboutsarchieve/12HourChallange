using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework;

namespace ScapWars.View
{
    class Controller
    {
        Display display;

        public Controller( )
        {
        }

        public void SetDisplay( Display gameDisplay )
        {
            display = gameDisplay;
        }

        public void ProcessInput( KeyboardState keyboardState )
        {
            Point movement = new Point( );

            if( keyboardState.IsKeyDown(Keys.Up) )
                movement.Y -= 1;
            if( keyboardState.IsKeyDown(Keys.Down) )
                movement.Y += 1;
            if( keyboardState.IsKeyDown(Keys.Right) )
                movement.X += 1;
            if( keyboardState.IsKeyDown(Keys.Left) )
                movement.X -= 1;

            display.moveUpperLeft( movement );
        }
    }
}
