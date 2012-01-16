using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ScrapWars.Items;

namespace ScrapWars.View
{
    class PartMenu
    {
        bool active;

        
        int currentSelection;

        List<Part> parts;

        public PartMenu( )
        {
        }

        public void ShowPartMenu( List<Part> Parts )
        {
            currentSelection = 0;
            active = true;

            parts = Parts;
        }

        public void MoveUp( )
        {
            if( currentSelection > 0 )
                currentSelection--;
        }
        public void MoveDown( )
        {
            if( currentSelection < parts.Count-1 )
                currentSelection++;
        }
        public Part SelectPart( )
        {
            return parts[currentSelection];
        }

        public void ExitPartsMenu( )
        {
            active = false;
        }


        public int CurrentSelection
        {
          get { return currentSelection; }
        }
        public int NumParts
        {
            get { return parts.Count; }
        }
        public String NameOfPart( int partNum ) // I am truly sorry for this method!
        {
            String name = "";
            Part thePart = parts[partNum];
   
            if( thePart is Head )
            {
                Head head = (Head) thePart;
                name = "Head: " + thePart.PartName;
                name += " Class: " + (int)head.PartRank;
                name += " Scan Level: " + (int)head.EnemyScanAbility;
                name += " Missle Defense Rate: " + head.MissileInterceptionRate;
            }
            if( thePart is Core )
            {
                Core core = (Core) thePart;
                name = "Core: " + thePart.PartName;  
                name += " Class:" + (int)core.PartRank;
                name += " HP: " + core.MaxHp;
            }
            if( thePart is LeftArm )
            {
                LeftArm leftArm = (LeftArm) thePart;
                name = "Left Arm: " + thePart.PartName;
                name += " Class: " + (int)leftArm.PartRank;
                name += " Blade Length: " + leftArm.SwordLength;
                name += " Blade Damage: " + leftArm.SwordDamage;
            }
            if( thePart is RightArm )
            {
                RightArm rightArm = (RightArm) thePart;
                name = "Right Arm: " + thePart.PartName;
                name += " Class: " +  (int)rightArm.PartRank;
                name += " Size: " + rightArm.ProjectileSize;
                name += " Damage: " + rightArm.Damage;
            }
            if( thePart is Shoulders )
            {
                Shoulders shoudlers = (Shoulders) thePart;
                name = "Shoulder: " + thePart.PartName;
                name += " Class: " + (int)shoudlers.PartRank;
                name += " Size: " + shoudlers.MissleSize;
                name += " Damage: " + shoudlers.Damage;
            }
            if( thePart is Legs )
            {
                Legs legs = (Legs) thePart;
                name = "Legs: " + thePart.PartName;
                name += " Class: " + (int)legs.PartRank;
                name += " Speed: " + legs.SpeedTilesPerSecond;
                name += " Hover: " + ((legs.Movement.Water != MoveQuality.none ) ? "True" : "False");
                name += " Heat Resist: " + ((legs.Movement.Water != MoveQuality.none ) ? "True" : "False");                
            }

            return name ;
        }
        public bool Active
        {
            get { return active; }
        }
    }
}
