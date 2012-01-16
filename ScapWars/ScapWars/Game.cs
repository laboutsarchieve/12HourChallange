////////////////////////////////////////////////////////////////////////////////////////////////////////
// This game was produced in Twelve hours. 
// In the rush to complete as much as possible in as short a time frame as possible, corners were cut.
// Much of the code is, frankly, aweful. It represnts some of my worst work.
// Have fun looking though it.
//
// Author: Benjamin Sergent
// Date: Jan 14, 2012
////////////////////////////////////////////////////////////////////////////////////////////////////////

using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;

using ScrapWars.Map;
using System.IO;
using ScrapWars.View;
using ScrapWars.Object;
using ScrapWars.Object.Actors;
using ScrapWars.Data;

namespace ScrapWars
{
    /// <summary>
    /// This is the main type for your game
    /// </summary>
    public class Game : Microsoft.Xna.Framework.Game
    {        
        GraphicsDeviceManager graphics;

        GameMap map;
        World gameWorld;
        Display display;
        Controller controller;

        public Game()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";

            IsMouseVisible = true;
        }

        /// <summary>
        /// Allows the game to perform any initialization it needs to before starting to run.
        /// This is where it can query for any required services and load any non-graphic
        /// related content.  Calling base.Initialize will enumerate through any components
        /// and initialize them as well.
        /// </summary>
        protected override void Initialize()
        {
            MapParams createdMapParams;

            PartMenu partsMenu = new PartMenu( );

            display = new Display(GraphicsDevice, Content, Window, partsMenu );  
            controller = new Controller( partsMenu );            
            gameWorld = new World( );

            createdMapParams = CreateMap( );
            ExportMap(createdMapParams);            
            
            gameWorld.MainPlayer = new Player( Vector2.Zero );
            gameWorld.WorldMap = map;            

            display.SetGameWorld( gameWorld );
            controller.SetDisplay( display );
            controller.SetWorld( gameWorld );

            base.Initialize();
        }

        private MapParams CreateMap( )
        {
            MapCreator mapc = new MapCreator( );

            map = mapc.CreateMap( );

            return mapc.mapParams;
        }

        private void ExportMap(MapParams mapParams)
        {
            Texture2D tex = MapExport.TexFromMap( GraphicsDevice, map );

            Directory.CreateDirectory( "Maps/Tests/create" );

            using( FileStream file = File.Open( "Maps/Tests/create/map" + mapParams.seed + ".png", FileMode.Create ) )
            {
                tex.SaveAsPng( file, mapParams.size.X, mapParams.size.Y );
            }
        }

        /// <summary>
        /// LoadContent will be called once per game and is the place to load
        /// all of your content.
        /// </summary>
        protected override void LoadContent()
        {

        }

        /// <summary>
        /// UnloadContent will be called once per game and is the place to unload
        /// all content.
        /// </summary>
        protected override void UnloadContent()
        {
            
        }

        /// <summary>
        /// Allows the game to run logic such as updating the world,
        /// checking for collisions, gathering input, and playing audio.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Update(GameTime gameTime)
        {
            float deltaTime = gameTime.ElapsedGameTime.Milliseconds;
            // Allows the game to exit
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed)
                this.Exit();

            controller.ProcessKeyboard( Keyboard.GetState( ) );
            controller.ProcessMouse(Mouse.GetState( ), deltaTime );
            gameWorld.Update( deltaTime );
            display.Update( deltaTime );

            base.Update(gameTime);
        }

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            display.Draw( );

            base.Draw(gameTime);
        }
    }
}
