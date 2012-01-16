using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ScrapWars.Object;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

using ScrapWars.Items;
using ScrapWars.Map;

namespace ScrapWars.Object.Actors
{
    class Player : TileObject
    {
        static Texture2D playerTexture;

        bool moveNextUpdate;
        Vector2 moveDirection;

        int health;        
        int numMissle;

        float facingAngle;

        float tillFire;

        List<Part> availableParts;        

        Head head;        
        Core core;        
        Legs legs;        
        LeftArm leftArm;        
        RightArm rightArm;        
        Shoulders shoulders;        

        public Player( Vector2 position ) : base( position, new Vector2(2,2), playerTexture )
        {
            const int START_MISSLES = 5;

            availableParts = new List<Part>( );

            LoadDefaultParts( );    
            moveNextUpdate = false;

            numMissle = START_MISSLES;

            tillFire = 0;
        }
        public void Update( float deltaTime, Tile tileType )
        {
            tillFire -= deltaTime * 1000;

            Move( deltaTime, tileType );            
        }
        public bool takeDamage( int damage )
        {
            health -= damage;
            return health < 0;                
        }
        private float MoveModeOn( Tile tileType )
        {
            MoveQuality moveQuality = legs.MoveQualityOn( tileType );

            if( moveQuality == MoveQuality.none )
                return 0;
            else
                return (int)moveQuality * 0.5f;
        }
        public Bullet FireBullet( )
        {
            const float BUFFER = 1.25f;
            
            if( tillFire < 1 )
            {
                Vector2 direction = Direction;

                tillFire = rightArm.TimeToFire;

                return new Bullet( Center + BUFFER * direction,
                                  rightArm.ProjectileSize,
                                  rightArm.Damage,
                                  rightArm.Velocity,
                                  direction,
                                  rightArm.BulletTexure,
                                  true);                
            }

            return null; // I need a special bullet object insteed of nulls
        }
        public void Move( float deltaTime, Tile tileType )
        {
            float moveMod = MoveModeOn( tileType );

            if( moveNextUpdate )
            {
                center += moveMod * Speed * moveDirection;
                moveNextUpdate = false;
            }
        }
        public void SetMoveDirection( Vector2 direction )
        {
            moveDirection = direction;
        }
        public void MoveNextUpdate( bool moveNextUpdate )
        {
            this.moveNextUpdate = moveNextUpdate;
        }
        public void TeleportTo( Vector2 location )
        {
            center = location;
        }
        public void ForceMove( Vector2 movement )
        {
            center += movement;
        }

        public void EquipPart( Part thePart )
        {
        }

        public void LoadDefaultParts()
        {
            AvailableParts.Clear( );

            this.Head = new Head( "DMB-Head", PartClass.ClassOne, EnemyScanAbility.None, 0 );
            this.Core = new Core( "BC-STD Base", PartClass.ClassOne, 10, false );
            this.LeftArm = new LeftArm("STAB-STD", PartClass.ClassOne, 1, 3 );
            this.RightArm = new RightArm("PEA-SHOT", PartClass.ClassOne, 0.25f, 5, 250, 4, RightArmGenerator.bulletTextureList[0]);
            this.Shoulders = new Shoulders("LTT-BOM", PartClass.ClassOne, 0.5f, 5, 0 );
            this.Legs = new Legs( "LG-Basic I", PartClass.ClassOne, 0.2f, MovementType.BasicGround( ) );    
        
            AvailableParts.Add( this.head );
            AvailableParts.Add( this.core );
            AvailableParts.Add( this.leftArm );
            AvailableParts.Add( this.rightArm );
            AvailableParts.Add( this.shoulders );
            AvailableParts.Add( this.legs );
        }

        public void LoadRandomStartParts()
        {
            AvailableParts.Clear( );

            this.Head = HeadGenerator.GenerateRandomHead( PartClass.ClassOne );
            this.Core = CoreGenerator.GenerateRandomCore( PartClass.ClassOne );
            this.LeftArm = LeftArmGenerator.GenerateLeftArm( PartClass.ClassOne );
            this.RightArm = RightArmGenerator.GenerateRightArm( PartClass.ClassOne );
            this.Shoulders = ShoulderGenerator.GetRandomShoulder( PartClass.ClassOne );
            this.Legs = LegGenerator.GenerateRandomLegs( PartClass.ClassOne );
            
            AvailableParts.Add( this.head );
            AvailableParts.Add( this.core );
            AvailableParts.Add( this.leftArm );
            AvailableParts.Add( this.rightArm );
            AvailableParts.Add( this.shoulders );
            AvailableParts.Add( this.legs );
        }

        public int PlayerLevel
        {
            get
            {
                int sumClass = (int)head.PartRank + (int)core.PartRank*3 + (int)legs.PartRank +
                               (int)leftArm.PartRank + (int)rightArm.PartRank + (int)shoulders.PartRank*2;

                return (int)Math.Round(sumClass/9.0);
            }
        }

        public MovementType Movement
        {
            get
            {
                return legs.Movement;
            }
        }
        public float Speed
        {
            get
            {
                return legs.SpeedTilesPerSecond; // Get from legs
            }
        }
        public float FacingAngle
        {
            get { return facingAngle; }
            set { facingAngle = value; }
        }

        public List<Part> AvailableParts
        {
            get { return availableParts; }
            set { availableParts = value; }
        }

        public Head Head
        {
            get { return head; }
            set { head = value; }
        }
        public Core Core
        {
            get { return core; }
            set 
            {               
                core = value;
            
                health = (int)(core.MaxHp);
            }
        }
        public RightArm RightArm
        {
            get { return rightArm; }
            set { rightArm = value; }
        }
        public LeftArm LeftArm
        {
            get { return leftArm; }
            set { leftArm = value; }
        }        
        public Legs Legs
        {
            get { return legs; }
            set { legs = value; }
        }
        public Shoulders Shoulders
        {
            get { return shoulders; }
            set { shoulders = value; }
        }
        public Vector2 Direction
        {
            get
            {
                return new Vector2((float)Math.Cos(facingAngle-Math.PI/2),(float)Math.Sin(facingAngle-Math.PI/2));
            }
        }
        public int Health
        {
            get { return health; }
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

