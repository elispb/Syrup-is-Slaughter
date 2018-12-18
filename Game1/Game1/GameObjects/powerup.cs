using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;

namespace SyrupIsSlaughter
{
    class powerup
    {
        public Random rand = new Random();
        int health;
        Rectangle powerUpRect;
        int velocity;
        int xPos;
        int yPos;
        bool activePowerUp = false;
        public powerup(int windowWidth, bool activePowerUpIn)
        {
            if (setrand(0, 3) == 2)
            {
                ActivePowerUp = activePowerUpIn;
            }
            if (ActivePowerUp)
            {
                Velocity = (int)setrand(1, 5);
                XPos = (int)setrand(0, windowWidth);
                Health = (int)setrand(5, 20);
                PowerUpRect = new Rectangle(XPos, 0, Health*3, Health*3);
            }
        }
        public int Health
        {
            get
            {
                return health;
            }

            set
            {
                health = value;
            }
        }

        public int Velocity
        {
            get
            {
                return velocity;
            }

            set
            {
                velocity = value;
            }
        }

        public int XPos
        {
            get
            {
                return xPos;
            }

            set
            {
                xPos = value;
            }
        }

        public int YPos
        {
            get
            {
                return yPos;
            }

            set
            {
                yPos = value;
            }
        }

        public bool ActivePowerUp
        {
            get
            {
                return activePowerUp;
            }

            set
            {
                activePowerUp = value;
            }
        }

        public Rectangle PowerUpRect
        {
            get
            {
                return powerUpRect;
            }
            set
            {
                powerUpRect = value;
            }
        }

        public int rectangleYPos
        {
            get
            {
                return powerUpRect.Y;
            }
            set
            {
                powerUpRect.Y += value;
            }
        }

        public double setrand(int low, int high)
        {
            return rand.Next(low, high);
        }
    }

}

