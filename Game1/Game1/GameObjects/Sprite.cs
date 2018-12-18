using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.Timers;

namespace SyrupIsSlaughter
{
    class Sprite
    {
        int XPos;
        int YPos;
        float health = 0;
        float damage;
        float speedAttack;
        float yvelocity = 0;
        float xvelocity = 0;
        float mass;
        int speedMovement;
        bool canPunch = true;
        bool canSlam = true;
        bool isJumping = false;
        bool isPunching = false;
        bool isSlamming = false;
        bool isHit = false;
        bool leftPunch = false;
        bool rightPunch = false;
        HealthBar healthbar;

        //Timer timer;
        public Sprite(int xPosIn, int yPosIn, float speedAttackIn, int speedMovementIn, float damageIn, float healthIn, float massIn, int windowWidthIn)
        {
            Healthbar = new HealthBar(this.Health, xPosIn, windowWidthIn);
            this.XPos = xPosIn;
            this.YPos = yPosIn;
            this.SpeedAttack = speedAttackIn;
            this.SpeedMovement = speedMovementIn;
            this.Damage = damageIn;
            this.Health = healthIn;
            this.bodyRectangle.X = xPosIn;
            this.bodyRectangle.Y = yPosIn;
            this.mass = massIn;
            
        }

        public Rectangle bodyRectangle = new Rectangle(50, 50, 50, 50);
        public Rectangle fistRectangle = new Rectangle(50, 50, 25, 25);
        public int xPos
        {
            get
            {
                return XPos;
            }
            set
            {
                XPos = value;
            }
        }
        public int yPos
        {
            get
            {
                return YPos;
            }
            set
            {
                YPos = value;
            }
        }

        public float Health { get { return health; } set { health = value; Healthbar.Health = health; } }
        public float Damage { get { return damage; } set { damage = value; } }
        public int SpeedMovement { get { return speedMovement; } set { speedMovement = value; } }
        public float SpeedAttack { get { return speedAttack; } set { speedAttack = value; } }
        public float Mass { get { return mass; } set { mass = value; } }
        public float YVelocity { get { return yvelocity; } set { yvelocity = value; } }
        public float XVelocity { get { return xvelocity; } set { xvelocity = value; } }

        public bool CanPunch
        {
            get
            {
                return canPunch;
            }

            set
            {
                canPunch = value;
            }
        }

        public bool IsJumping
        {
            get
            {
                return isJumping;
            }

            set
            {
                isJumping = value;
            }
        }

        public bool IsPunching
        {
            get
            {
                return isPunching;
            }

            set
            {
                isPunching = value;
            }
        }

        public bool IsHit
        {
            get
            {
                return isHit;
            }

            set
            {
                isHit = value;
            }
        }

        internal HealthBar Healthbar
        {
            get
            {
                return healthbar;
            }

            set
            {
                healthbar = value;
            }
        }

        public bool CanSlam
        {
            get
            {
                return canSlam;
            }

            set
            {
                canSlam = value;
            }
        }

        public bool IsSlamming
        {
            get
            {
                return isSlamming;
            }

            set
            {
                isSlamming = value;
            }
        }

        public bool LeftPunch
        {
            get
            {
                return leftPunch;
            }

            set
            {
                leftPunch = value;
            }
        }

        public bool RightPunch
        {
            get
            {
                return rightPunch;
            }

            set
            {
                rightPunch = value;
            }
        }

        public void Draw(SpriteBatch spriteBatch, Texture2D Texture)
        {
            spriteBatch.Begin();
            spriteBatch.Draw(Texture, bodyRectangle, Color.White);
            spriteBatch.End();
        }
    }
}
