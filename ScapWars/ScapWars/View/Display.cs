using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using ScapWars.Map;
using Microsoft.Xna.Framework.Graphics;
using ScapWars.Object;
using Microsoft.Xna.Framework.Content;

namespace ScapWars.View
{
    class Display
    {
        const int TEXTURE_SIZE = 64;
        const int TILE_SIZE = 24;

        Point upperLeft;

        GameWindow window;
        GameMap map;

        SpriteBatch spriteBatch;

        Dictionary<Tile, Texture2D> tileTextures;

        public Display( GraphicsDevice graphics, ContentManager content, GameWindow gameWindow )
        {
            window = gameWindow;

            upperLeft = new Point(0, 0);

            spriteBatch = new SpriteBatch( graphics );
            tileTextures = new Dictionary<Tile,Texture2D>( );

            LoadTileTextures(content);
            LoadObjectTextures(content);
        }

        public void SetGameMap( GameMap gameMap )
        {
            map = gameMap;
        }

        public void LoadTileTextures(ContentManager content)
        {
            Texture2D dirt = content.Load<Texture2D>("Dirt");
            Texture2D grass = content.Load<Texture2D>("Grass");
            Texture2D sand = content.Load<Texture2D>("Sand");
            Texture2D water = content.Load<Texture2D>("Water");
            Texture2D lava = content.Load<Texture2D>("Lava");
            Texture2D volcano = content.Load<Texture2D>("Volcano");

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
        }

        public void Draw( )
        {
            Point screenInTiles = calcScreenInTiles( );

            spriteBatch.Begin( );
                DrawTiles(screenInTiles);
                DrawObjects(screenInTiles);
            spriteBatch.End( );
        }

        public void DrawTiles(Point screenInTiles)
        {
            Texture2D objectTex;
            for( int x = 0; x < screenInTiles.X && x < map.Size.X; x++ )
            {
                for( int y = 0; y < screenInTiles.Y && y < map.Size.Y; y++ )
                {
                    objectTex = tileTextures[map.GetTile( new Point(upperLeft.X+x,upperLeft.Y+y))];

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

        public void DrawObjects(Point screenInTiles)
        {
            Texture2D objectTex;
            for( int x = 0; x < screenInTiles.X && x < map.Size.X; x++ )
            {
                for( int y = 0; y < screenInTiles.Y && y < map.Size.Y; y++ )
                {
                    objectTex = map.GetTextureOfObject(new Point(upperLeft.X+x,upperLeft.Y+y));

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

        private Point calcScreenInTiles( )
        {
            const int PADDING = 1;
            Point screenInTiles = new Point( );

            screenInTiles.X = window.ClientBounds.Width/TILE_SIZE + PADDING;
            screenInTiles.Y = window.ClientBounds.Height/TILE_SIZE + PADDING;

            return screenInTiles;
        }

        public void moveUpperLeft( Point movement )
        {
            upperLeft.X += movement.X;
            upperLeft.Y += movement.Y;

            ValidateUpperLeft( );
        }

        private void ValidateUpperLeft( )
        {
            if( upperLeft.X < 0 )
                upperLeft.X = 0;
            if( upperLeft.Y < 0 )
                upperLeft.Y = 0;

            if( upperLeft.X > map.Size.X-1 )
                upperLeft.X = map.Size.X-1;
            if( upperLeft.Y > map.Size.Y-1 )
                upperLeft.Y = map.Size.Y-1;
        }
    }
}
