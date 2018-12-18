using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace SyrupIsSlaughter
{
    class HealthBar
    {
        float health;
        Rectangle HealthBarRect;
        int windowWidth;

        public HealthBar(float healthIn, int xPos, int windowWidthIn)
        {
            WindowWidth = windowWidthIn;
            Health = healthIn;
            if (windowWidthIn > 0)
            {
                HealthBarRect = new Rectangle(windowWidthIn - (int)Health, 10, (int)health, 50);
            }
            else
            {
                HealthBarRect = new Rectangle(0, 10, (int)health, 50);

            }
        }

        public float Health
        {
            get
            {
                return health;
            }

            set
            {
                health = value;
                if (health < 0)
                {
                    health = 0;
                }
                HealthBarRect.Width = (int)health;
                if (windowWidth > 0)
                {
                    HealthBarRect.X = windowWidth - (int)Health;
                }                
            }
        }

        public Rectangle HealthBarRect1
        {
            get
            {
                return HealthBarRect;
            }

            set
            {
                HealthBarRect = value;
            }
        }

        public int WindowWidth
        {
            get
            {
                return windowWidth;
            }

            set
            {
                windowWidth = value;
            }
        }
    }
}
