using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using ScrapWars.Map;
using Microsoft.Xna.Framework.Graphics;
using ScrapWars.Object;
using Microsoft.Xna.Framework.Content;
using ScrapWars.Object.Actors;
using ScrapWars.Data;
using ScrapWars.Items;

namespace ScrapWars.View
{
    class Display
    {
        public const int TEXTURE_SIZE = 24;
        public const int TILE_SIZE = 24;

        Vector2 upperLeft;

        GameWindow window;
        World gameWorld;
        PartMenu partsMenu;

        SpriteBatch spriteBatch;
        SpriteFont spriteFont;

        Dictionary<Tile, Texture2D> tileTextures;
        Texture2D dummy;
        Texture2D explosion;

        Vector2 lastPlayerPos;

        public Display( GraphicsDevice graphics, ContentManager content,
                        GameWindow gameWindow, PartMenu partsMenu )
        {
            window = gameWindow;
            this.partsMenu = partsMenu;

            upperLeft = new Vector2(0, 0);
            lastPlayerPos = new Vector2(0, 0);

            spriteBatch = new SpriteBatch( graphics );
            spriteFont = content.Load<SpriteFont>("GameFont");
            tileTextures = new Dictionary<Tile,Texture2D>( );            

            LoadTileTextures(content);
            LoadObjectTextures(content);
            LoadActorTextures(content);
        }        

        public void Update(float deltaTime)
        {
            Vector2 quarterScreen = 0.25f * CalcScreenInTiles( );
            Vector2 playerScreenPos = GetScreenPos(gameWorld.MainPlayer.Center);

            if( playerScreenPos.X < quarterScreen.X )
            {
                moveUpperLeft( new Vector2( playerScreenPos.X - quarterScreen.X, 0 ));
            }
            else if( playerScreenPos.X > window.ClientBounds.Width/TILE_SIZE - quarterScreen.X )
            {
                moveUpperLeft( new Vector2( playerScreenPos.X - (window.ClientBounds.Width/TILE_SIZE - quarterScreen.X), 0 ));
            }
            
            if( playerScreenPos.Y < quarterScreen.Y )
            {
                moveUpperLeft( new Vector2( 0, playerScreenPos.Y - quarterScreen.Y ));
            }
            else if( playerScreenPos.Y > window.ClientBounds.Height/TILE_SIZE - quarterScreen.Y )
            {
                moveUpperLeft( new Vector2( 0, playerScreenPos.Y - (window.ClientBounds.Height/TILE_SIZE - quarterScreen.Y)  ));
            }

            lastPlayerPos = gameWorld.MainPlayer.Center;
        }

        public void Draw( )
        {
            if( gameWorld.GameOver )
                DrawGameOver( );            
            else if( partsMenu.Active )
                DrawMenu( );
            else
                DrawPlaying( );
        }
        private void DrawGameOver()
        {
            Vector2 screenInTiles = CalcScreenInTiles( );

            spriteBatch.Begin( );
                DrawTiles(screenInTiles);
                DrawObjects(screenInTiles);
                DrawActors( );
                DrawGameOverScreen( );
            spriteBatch.End( );
        }
        public void DrawMenu( )
        {
            Vector2 screenInTiles = CalcScreenInTiles( );

            spriteBatch.Begin( );
                DrawTiles(screenInTiles);
                DrawObjects(screenInTiles);
                DrawPartMenu( );
                DrawHUD( );
            spriteBatch.End( );
        }
        private void DrawPlaying( )
        {
            Vector2 screenInTiles = CalcScreenInTiles( );

            spriteBatch.Begin( );
                DrawTiles(screenInTiles);
                DrawObjects(screenInTiles);
                DrawActors( );
                DrawBullets( );
            DrawHUD( );
                if( gameWorld.Stabbing )
                    DrawSword( );
            spriteBatch.End( );
        }

        public void DrawTiles(Vector2 screenInTiles)
        {
            Texture2D objectTex;
            for( int x = 0; x < screenInTiles.X && upperLeft.X+x < gameWorld.WorldMap.Size.X; x++ )
            {
                for( int y = 0; y < screenInTiles.Y && upperLeft.Y+y < gameWorld.WorldMap.Size.Y; y++ )
                {
                    objectTex = tileTextures[gameWorld.WorldMap.GetTile( new Point((int)upperLeft.X+x,(int)upperLeft.Y+y))];

                    spriteBatch.Draw( objectTex,
                                      new Vector2(x*TILE_SIZE,y*TILE_SIZE),
                                      null,
                                      Color.White,
                                      0.0f,
                                      Vector2.Zero,
                                      TILE_SIZE/(float)TEXTURE_SIZE,
                                      SpriteEffects.None,
                                      0.5f  );
                }
            }
        }

        public void DrawObjects(Vector2 screenInTiles)
        {
            Texture2D objectTex;
            for( int x = 0; x < screenInTiles.X && x < upperLeft.X+gameWorld.WorldMap.Size.X; x++ )
            {
                for( int y = 0; y < screenInTiles.Y && upperLeft.Y+y < gameWorld.WorldMap.Size.Y; y++ )
                {
                    objectTex = gameWorld.WorldMap.GetTextureOfObject(new Point((int)upperLeft.X+x,(int)upperLeft.Y+y));

                    if( objectTex != null )
                    {
                        spriteBatch.Draw( objectTex,
                                          new Vector2(x*TILE_SIZE,y*TILE_SIZE),
                                          null,
                                          Color.White,
                                          0.0f,
                                          Vector2.Zero,
                                          TILE_SIZE/(float)TEXTURE_SIZE,
                                          SpriteEffects.None,
                                          0.5f  );
                    }

                }
            }
        }

        public void DrawActors( )
        {
            Vector2 offset = GetScreenPos( gameWorld.MainPlayer.Center );

            if( !gameWorld.GameOver )
            {
                spriteBatch.Draw( gameWorld.MainPlayer.Texture,
                                  new Vector2(offset.X*TILE_SIZE,offset.Y*TILE_SIZE),
                                  null,
                                  Color.White,
                                  gameWorld.MainPlayer.FacingAngle,
                                  new Vector2(TILE_SIZE,TILE_SIZE),
                                  TILE_SIZE/(float)TEXTURE_SIZE,
                                  SpriteEffects.None,
                                  0.6f  );
            }

            foreach( TileObject enemy in gameWorld.EnemyList )
            {
                offset = GetScreenPos( enemy.Center );

                spriteBatch.Draw( enemy.Texture,
                                  new Vector2(offset.X*TILE_SIZE,offset.Y*TILE_SIZE),
                                  null,
                                  Color.White,
                                  0.0f,
                                  new Vector2(TILE_SIZE/2,TILE_SIZE/2),
                                  enemy.SizeInTiles,
                                  SpriteEffects.None,
                                  0.7f  );
            }
        }
        public void DrawBullets( )
        {
            List<Vector2> toRemove = new List<Vector2>( );
            Vector2 offset;
            foreach( Bullet bullet in gameWorld.BulletList )
            {
                offset = GetScreenPos( bullet.Center );

                spriteBatch.Draw( bullet.Texture,
                                  new Vector2(offset.X*TILE_SIZE,offset.Y*TILE_SIZE),
                                  null,
                                  bullet.BulletColor,
                                  0.0f,
                                  new Vector2(TILE_SIZE/2,TILE_SIZE/2),
                                  bullet.SizeInTiles,
                                  SpriteEffects.None,
                                  0.7f  );
            }

            foreach( Vector2 hit in gameWorld.RecentHitList )
            {
                offset = GetScreenPos( hit );
                spriteBatch.Draw( explosion,
                                  new Vector2(offset.X*TILE_SIZE,offset.Y*TILE_SIZE),
                                  null,
                                  Color.Yellow,
                                  0.0f,
                                  new Vector2(TILE_SIZE,TILE_SIZE),
                                  0.5f,
                                  SpriteEffects.None,
                                  0.7f  );

                toRemove.Add( hit );
            }

            foreach( Vector2 remove in toRemove )
            {
                gameWorld.RecentHitList.Remove( remove );
            }
        }
        public void DrawSword( )
        {
            Vector2 offset = GetScreenPos( gameWorld.MainPlayer.Center );
            Vector2 direction = gameWorld.MainPlayer.Direction;

            float angle = gameWorld.MainPlayer.FacingAngle;
            float length = gameWorld.MainPlayer.LeftArm.SwordLength;

            spriteBatch.Draw( dummy,
                              TILE_SIZE/2*direction + new Vector2(offset.X*TILE_SIZE,offset.Y*TILE_SIZE),
                              null,
                              Color.LightGray,
                              angle,
                              Vector2.One,
                              new Vector2( 0.25f*TILE_SIZE, length*TILE_SIZE ),
                              SpriteEffects.None,
                              0.9f  );

            gameWorld.Stabbing = false;
        }
        public void DrawPartMenu( )
        {
            const int BUFFER = 25;
            const int MAX_SHOW = 10;
            int numShown = 0;

            Color currColor;

            spriteBatch.Draw(dummy, new Rectangle( 25, 25, window.ClientBounds.Width  - 50, window.ClientBounds.Height - 50 ), Color.DarkGray );

            for( int k = partsMenu.CurrentSelection; k < partsMenu.NumParts && numShown < MAX_SHOW ; k++ )
            {
                currColor = ( k == partsMenu.CurrentSelection ) ? Color.Yellow : Color.Black;
                spriteBatch.DrawString( spriteFont,
                                       partsMenu.NameOfPart(k),
                                       new Vector2( BUFFER, BUFFER + numShown * 30 ),
                                       currColor );

                numShown++;
            }
        }
        private void DrawHUD( )
        {
            spriteBatch.DrawString( spriteFont, "Core Integrety: " + gameWorld.MainPlayer.Health, new Vector2( 20, 20), Color.White );
        }
        private void DrawGameOverScreen()
        {
            String msg = "YOU WERE DESTROYED!";
            float halfWidth = spriteFont.MeasureString( msg ).X/2;
            spriteBatch.DrawString( spriteFont,
                                    "YOU WERE DESTROYED!",
                                    new Vector2( window.ClientBounds.Width/2-halfWidth, window.ClientBounds.Height/2 ),
                                    Color.Crimson );
        }
        public Vector2 GetScreenPos( Vector2 worldPos ) // This is more wrong the further I get from the upper left
        {
            Vector2 screenPos = worldPos - upperLeft;

            return screenPos;
        }
        private Vector2 CalcScreenInTiles( )
        {
            const int PADDING = 1;
            Vector2 screenInTiles = new Vector2( );

            screenInTiles.X = window.ClientBounds.Width/TILE_SIZE + PADDING;
            screenInTiles.Y = window.ClientBounds.Height/TILE_SIZE + PADDING;

            return screenInTiles;
        }

        public void setUpperLeft( Vector2 upperLeft )
        {
            this.upperLeft = upperLeft;

            ValidateUpperLeft( );
        }
        public void moveUpperLeft( Vector2 movement )
        {
            upperLeft += movement;

            ValidateUpperLeft( );
        }

        private void ValidateUpperLeft( )
        {
            if( upperLeft.X < 0 )
                upperLeft.X = 0;
            if( upperLeft.Y < 0 )
                upperLeft.Y = 0;

            if( upperLeft.X > gameWorld.WorldMap.Size.X-1 )
                upperLeft.X = gameWorld.WorldMap.Size.X-1;
            if( upperLeft.Y > gameWorld.WorldMap.Size.Y-1 )
                upperLeft.Y = gameWorld.WorldMap.Size.Y-1;
        }

        public void LoadTileTextures(ContentManager content)
        {
            Texture2D dirt = content.Load<Texture2D>("Dirt");
            Texture2D grass = content.Load<Texture2D>("Grass");
            Texture2D sand = content.Load<Texture2D>("Sand");
            Texture2D water = content.Load<Texture2D>("Water");
            Texture2D lava = content.Load<Texture2D>("Lava");
            Texture2D volcano = content.Load<Texture2D>("Volcano");

            dummy = content.Load<Texture2D>("Dummy");

            tileTextures.Add( Tile.Dirt, dirt );
            tileTextures.Add( Tile.Grass, grass );
            tileTextures.Add( Tile.Sand, sand );
            tileTextures.Add( Tile.Water, water );
            tileTextures.Add( Tile.Lava, lava );
            tileTextures.Add( Tile.Volcano, volcano );
        }

        public void LoadObjectTextures(ContentManager content)
        {
            Tree.TreeTexture = content.Load<Texture2D>("BadTree");
            Boulder.BoulderTexture = content.Load<Texture2D>("Boulder");
            Factory.FactoryTexture = content.Load<Texture2D>("Factory");

            explosion = content.Load<Texture2D>("Explosion");
        }

        public void LoadActorTextures(ContentManager content)
        {
            Player.PlayerTexture = content.Load<Texture2D>("Player");
            Enemy.EnemyTextures = new List<Texture2D>( );
            Enemy.EnemyTextures.Add( content.Load<Texture2D>("SkyDrone") );
            Enemy.EnemyTextures.Add( content.Load<Texture2D>("LandDrone") );

            List<Texture2D> bulletList = new List<Texture2D>( );
            bulletList.Add(content.Load<Texture2D>("BulletOne"));

            RightArmGenerator.bulletTextureList = bulletList;
        }

        public void SetGameWorld( World gameWorld )
        {           
            this.gameWorld = gameWorld;

            upperLeft = gameWorld.MainPlayer.Center - 0.5f * new Vector2(window.ClientBounds.Width/(2*TILE_SIZE),window.ClientBounds.Height/(2*TILE_SIZE));
        }
    }
}
