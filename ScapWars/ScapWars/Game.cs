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

using ScapWars.Map;
using System.IO;
using ScapWars.View;
using ScapWars.Object;

namespace ScapWars
{
    /// <summary>
    /// This is the main type for your game
    /// </summary>
    public class Game : Microsoft.Xna.Framework.Game
    {        
        GraphicsDeviceManager graphics;

        GameMap map;
        Display display;
        Controller controller;

        public Game()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
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

            display = new Display(GraphicsDevice, Content, Window );  
            controller = new Controller( );

            controller.SetDisplay( display );

            createdMapParams = CreateMap( );
            ExportMap(createdMapParams);            

            display.SetGameMap( map );

            base.Initialize();
        }

        private MapParams CreateMap( )
        {
            MapCreator mapc = new MapCreator( );

            mapc.mapParams.seed = 1;

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
            // Allows the game to exit
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed)
                this.Exit();

            controller.ProcessInput( Keyboard.GetState( ) );

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
