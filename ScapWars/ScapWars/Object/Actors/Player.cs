using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ScrapWars.Object;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace ScrapWars.Object.Actors
{
    class Player : TileObject
    {
        static Texture2D playerTexture;

        int health;
        // Parts

        public Player( Point position ) : base( position, new Point(2,2), playerTexture )
        {

        }

        public MovementType Movement
        {
            get
            {
                // Get from legs
                MovementType move = new MovementType( );
                move.Dirt = MoveQuality.normal;
                move.Grass = MoveQuality.normal;
                move.Sand = MoveQuality.slow;
                move.Water = MoveQuality.none;
                move.Lava = MoveQuality.none;

                return move;
            }
        }

        public int Speed
        {
            get
            {
                return 1; // Get from legs
            }
        }

        static public Texture2D PlayerTexture
        {
            set
            {
                playerTexture = value;
            }
        }
    }
}

