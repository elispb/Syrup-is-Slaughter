using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SyrupIsSlaughter
{
    class TreeEnt : Sprite
    {   
        public TreeEnt(int xPosIn, int yPosIn, float speedAttackIn, int speedMovementIn, float damageIn, float healthIn, float massIn, int WindowWidthIn) :
            base(xPosIn, yPosIn, speedAttackIn, speedMovementIn, damageIn, healthIn, massIn, WindowWidthIn)
        {
            Health = 150;
            Damage = 8;
            SpeedMovement = 2;
            SpeedAttack = 100;
            Mass = 2;
            fistRectangle.Width = 50;
        }        
    }
    class GreenPeace : Sprite
    {
        public GreenPeace(int xPosIn, int yPosIn, float speedAttackIn, int speedMovementIn, float damageIn, float healthIn, float massIn, int WindowWidthIn) :
            base(xPosIn, yPosIn, speedAttackIn, speedMovementIn, damageIn, healthIn, massIn, WindowWidthIn)
        {
            Health = 75;
            Damage = 6;
            SpeedMovement = 8;
            SpeedAttack = 500;
            Mass = 1.4f;
        }

        

    }
    class LumberJack : Sprite
    {        
        public LumberJack(int xPosIn, int yPosIn, float speedAttackIn, int speedMovementIn, float damageIn, float healthIn, float massIn, int WindowWidthIn) :
            base(xPosIn, yPosIn, speedAttackIn, speedMovementIn, damageIn, healthIn, massIn, WindowWidthIn)
        {
            Health = 100;
            Damage = 10;
            SpeedMovement = 5;
            SpeedAttack = 750;
            Mass = 1;
        }
    }
    class SyrupLover : Sprite
    {
        public SyrupLover(int xPosIn, int yPosIn, float speedAttackIn, int speedMovementIn, float damageIn, float healthIn, float massIn, int WindowWidthIn) :
            base(xPosIn, yPosIn, speedAttackIn, speedMovementIn, damageIn, healthIn, massIn, WindowWidthIn)
        {
            Health = 200;
            Damage = 6;
            SpeedMovement = 6;
            SpeedAttack = 1000;
            Mass = 2.5f;
        }
    }
}
