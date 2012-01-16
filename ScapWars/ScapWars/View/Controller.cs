using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework;
using ScrapWars.Data;

namespace ScrapWars.View
{
    class Controller
    {
        World gameWorld;
        Display display;

        PartMenu partsMenu;

        KeyboardState prevState;

        float tillBulletFire;
        float tillSword;
        float timeSwordUp;

        public Controller( PartMenu partsMenu )
        {
            prevState = new KeyboardState( );
            this.partsMenu = partsMenu;

            tillBulletFire = 0;
            timeSwordUp = 0;
        }

        public void SetDisplay( Display display )
        {
            this.display = display;        
        }

        public void SetWorld( World gameWorld )
        {
            this.gameWorld = gameWorld;
        }

        public void ProcessKeyboard( KeyboardState keyboardState )
        {
            if( partsMenu.Active )
                ProcessMenuRequest( keyboardState );
            else
                ProcessMoveRequest( keyboardState );

            ProcessUIRequest( keyboardState );

            prevState = keyboardState;
        }
        private void ProcessMoveRequest( KeyboardState keyboardState )
        {
            const float moveAmount = 1.0f;
            Vector2 movement = new Vector2( );
            
            if( keyboardState.IsKeyDown(Keys.W) )
                movement.Y -= moveAmount;
            if( keyboardState.IsKeyDown(Keys.S) )
                movement.Y += moveAmount;            
            if( keyboardState.IsKeyDown(Keys.A) )
                movement.X -= moveAmount;
            if( keyboardState.IsKeyDown(Keys.D) )
                movement.X += moveAmount;
                        
            if( movement != Vector2.Zero )
            {   
                gameWorld.MainPlayer.SetMoveDirection( movement );                
                gameWorld.MainPlayer.MoveNextUpdate( true );
            }
        }
        private void ProcessUIRequest( KeyboardState keyboardState )
        {           
            if( keyboardState.IsKeyDown( Keys.Tab ) && !prevState.IsKeyDown( Keys.Tab) )
            {
                if( partsMenu.Active )
                {
                    gameWorld.Paused = false;
                    partsMenu.ExitPartsMenu( );
                }
                else if( gameWorld.IsPlayerOnFactory( ) )
                {
                    gameWorld.Paused = true;
                    partsMenu.ShowPartMenu( gameWorld.MainPlayer.AvailableParts );
                }
            }
            
            if( keyboardState.IsKeyDown( Keys.Enter ))
            {
                if( partsMenu.Active )
                    gameWorld.MainPlayer.EquipPart( partsMenu.SelectPart( ) );

                if( gameWorld.GameOver )
                    gameWorld.Reset( );
            }           
        }
        private void ProcessMenuRequest( KeyboardState keyboardState )
        {
            if( keyboardState.IsKeyDown(Keys.Up) && !prevState.IsKeyDown( Keys.Up ))
                partsMenu.MoveUp( );
            if( keyboardState.IsKeyDown(Keys.Down) && !prevState.IsKeyDown( Keys.Down ))
                partsMenu.MoveDown( );
        }

        public void ProcessMouse( MouseState mouseState, float deltaTime )
        {
            

            ProcessFacing(mouseState);
            ProcessViolence(mouseState, deltaTime );           
        }
        public void ProcessFacing( MouseState mouseState )
        {
            float angle;
            Vector2 mousePos = new Vector2(mouseState.X, mouseState.Y)*(1/(float)Display.TILE_SIZE);
            Vector2 playerPos = display.GetScreenPos(gameWorld.MainPlayer.Center);
            Vector2 toMouse = playerPos - mousePos;
            angle = (float)Math.Atan2(toMouse.Y, toMouse.X);

            gameWorld.MainPlayer.FacingAngle = angle - (float)Math.PI/2;
        }
        public void ProcessViolence( MouseState mouseState, float deltaTime )
        {
            tillBulletFire -= deltaTime;
            tillSword -= deltaTime;
            if( mouseState.RightButton == ButtonState.Pressed && tillBulletFire < 0 )
            {
                gameWorld.FireBullet( );
                tillBulletFire = 40;
            }
            if( mouseState.LeftButton == ButtonState.Pressed && tillSword < 1 )
            {
                gameWorld.StabSword( );
                timeSwordUp += deltaTime;

                if( timeSwordUp > 200 )                
                {
                    tillSword = 1000;
                    timeSwordUp = 0;
                }
            }
        }
    }
}