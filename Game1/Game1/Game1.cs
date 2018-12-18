using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System.Timers;
using System.Collections.Generic;
using System;

namespace SyrupIsSlaughter
{
    /// <summary>
    /// This is the main type for your game.
    /// </summary>
    public class Game1 : Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;

        public delegate void ElementClicked();

        Timer AttackPlayer1Timer = new Timer();
        Timer AttackPlayer2Timer = new Timer();
        Timer CooldownPlayer1Timer = new Timer();
        Timer CooldownPlayer2Timer = new Timer();
        Timer HitCooldownTimerP1 = new Timer();
        Timer HitCooldownTimerP2 = new Timer();

        float coefficientOfRestitution = 0.6f;
        float g = 9.81f;
        bool player1Ready = false;
        bool player2Ready = false;
        int p1 = 0;
        int p2 = 3;
        enum gamestate
        {
            startmenu,
            gameplay,
            gameover,
        }
        gamestate state;

        enum Character
        {
            TreeEnt,
            GreenPeace,
            Lumberjack,
            SyrupLover,
        }

        SimpleMenu menu;

        //Textures
        Texture2D TreeEntLeft;
        Texture2D TreeEntRight;
        Texture2D TreeEntPunchLeft;
        Texture2D TreeEntPunchRight;
        Texture2D HippieLeft;
        Texture2D HippieRight;
        Texture2D HippiePunchLeft;
        Texture2D HippiePunchRight;
        Texture2D SyrupLoverLeft;
        Texture2D SyrupLoverRight;
        Texture2D SyrupLoverPunchLeft;
        Texture2D SyrupLoverPunchRight;
        Texture2D lumberJackLeft;
        Texture2D lumberJackRight;
        Texture2D lumberJackPunchLeft;
        Texture2D lumberJackPunchRight;
        Texture2D Player1BodyLeft;
        Texture2D Player1BodyRight;
        Texture2D Player1Left;
        Texture2D Player1Right;
        Texture2D Player2BodyLeft;
        Texture2D Player2BodyRight;
        Texture2D Player2Left;
        Texture2D Player2Right;
        Texture2D powerup;
        Texture2D background;
        Texture2D bar;

        Texture2D playerRand;
        Texture2D playerReady;
        Texture2D playerCharEnt;
        Texture2D playerCharLumberJack;
        Texture2D MainMenu;
        Texture2D GameOver;
        Texture2D UnknownSoldier;

        //Rectangles
        Rectangle player1Avatar;
        Rectangle player2Avatar;
        Rectangle theMotherFuckingGround;
        Rectangle player1fist;
        Rectangle player2fist;

        Rectangle player1randRectangle;
        Rectangle player2randRectangle;
        Rectangle player1readyRectangle;
        Rectangle player2readyRectangle;
        Rectangle player1charRectangle;
        Rectangle player2charRectangle;
        Rectangle backgroundRect;

        Rectangle MainMenuRectangle;

        int windowHeight = 600;
        int windowWidth = 800;

        Sprite[] players = new Sprite[2];
        List<powerup> listOfPowerups = new List<powerup>();

        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = true;
        }


        protected override void Initialize()
        {
            // TODO: Add your initialization logic here
            graphics.PreferredBackBufferHeight = windowHeight;
            graphics.PreferredBackBufferWidth = windowWidth;

            player1randRectangle = new Rectangle(150, 200, 150, 120);
            player2randRectangle = new Rectangle(500, 200, 150, 120);
            player1readyRectangle = new Rectangle(150, 350, 150, 70);
            player2readyRectangle = new Rectangle(500, 350, 150, 70);
            player1charRectangle = new Rectangle(150, 50, 150, 120);
            player2charRectangle = new Rectangle(500, 50, 150, 120);
            backgroundRect = new Rectangle(0, 0, windowWidth, windowHeight);

            MainMenuRectangle = new Rectangle(-245, 10, 1500, 600);

            base.Initialize();
        }

        protected override void LoadContent()
        {
            state = gamestate.startmenu;
            // Create a new SpriteBatch, which can be used to draw textures.
            spriteBatch = new SpriteBatch(GraphicsDevice);

            // TODO: use this.Content to load your game content here
            TreeEntLeft = Content.Load<Texture2D>("Tree2Left");
            TreeEntRight = Content.Load<Texture2D>("Tree2Right");
            TreeEntPunchLeft = Content.Load<Texture2D>("treeattackleft");
            TreeEntPunchRight = Content.Load<Texture2D>("treeattackright");
            HippieLeft = Content.Load<Texture2D>("IdleLeftH3");
            HippieRight = Content.Load<Texture2D>("IdleRightH3");
            HippiePunchLeft = Content.Load<Texture2D>("HippieWeaponLeft");
            HippiePunchRight = Content.Load<Texture2D>("HippieWeaponRight");
            SyrupLoverLeft = Content.Load<Texture2D>("IdleLeftH");
            SyrupLoverRight = Content.Load<Texture2D>("IdleRightH");
            SyrupLoverPunchLeft = Content.Load<Texture2D>("Maple_Syrus");
            SyrupLoverPunchRight = Content.Load<Texture2D>("Maple_Syrus");
            lumberJackLeft = Content.Load<Texture2D>("Right_PunchH2");
            lumberJackRight = Content.Load<Texture2D>("Left_PunchH2");
            lumberJackPunchLeft = Content.Load<Texture2D>("Axe_Left");
            lumberJackPunchRight = Content.Load<Texture2D>("Axe_Right");
            powerup = Content.Load<Texture2D>("Maple_Syrus");
            background = Content.Load<Texture2D>("ground");
            bar = Content.Load<Texture2D>("lifeBar");
            GameOver = Content.Load<Texture2D>("GameOver");
            UnknownSoldier = Content.Load<Texture2D>("punchingCage");

            menu = new SimpleMenu("MainMenu");
            menu.LoadContent(Content);
            menu.CenterElement(750, 800);
            menu.MoveElement(50, 150);
            playerRand = Content.Load<Texture2D>("RandomPlayer");

            playerReady = Content.Load<Texture2D>("Ready");

            MainMenu = Content.Load<Texture2D>("MainMenu");

            Player1BodyLeft = SyrupLoverLeft;
            Player2BodyLeft = SyrupLoverRight;

        }

        /// <summary>
        /// UnloadContent will be called once per game and is the place to unload
        /// game-specific content.
        /// </summary>
        protected override void UnloadContent()
        {
            // TODO: Unload any non ContentManager content here
        }

        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Update(GameTime gameTime)
        {
            switch (state)
            {
                case gamestate.startmenu:
                    startMenuUpdate();
                    break;
                case gamestate.gameplay:
                    gameplayUpdate();
                    break;
                case gamestate.gameover:
                    gameOverUpdate();
                    break;
                default:
                    break;
            }
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            // TODO: Add your update logic here

            base.Update(gameTime);
        }
        bool ispressedP1 = false;
        bool ispressedP2 = false;

        protected void startMenuUpdate()
        {

            KeyboardState keystate = Keyboard.GetState();
            if (keystate.IsKeyDown(Keys.A))
            {
                if (!ispressedP1)
                {
                    ispressedP1 = true;

                    p1++;
                    switch (p1)
                    {
                        case 0:
                            Player1BodyLeft = TreeEntLeft;
                            break;
                        case 1:
                            Player1BodyLeft = lumberJackLeft;
                            break;
                        case 2:
                            Player1BodyLeft = SyrupLoverLeft;
                            break;
                        case 3:
                            Player1BodyLeft = HippieLeft;
                            break;

                        default:
                            p1 = 0;
                            Player1BodyLeft = TreeEntLeft;
                            break;
                    }
                }

            }
            if (keystate.IsKeyDown(Keys.D))
            {
                if (!ispressedP1)
                {
                    ispressedP1 = true;
                    p1--;
                    switch (p1)
                    {
                        case 0:
                            Player1BodyLeft = TreeEntLeft;
                            break;
                        case 1:
                            Player1BodyLeft = lumberJackLeft;
                            break;
                        case 2:
                            Player1BodyLeft = SyrupLoverLeft;
                            break;
                        case 3:
                            Player1BodyLeft = HippieLeft;
                            break;
                        default:
                            p1 = 3;
                            Player1BodyLeft = HippieLeft;
                            break;
                    }
                }

            }
            if (ispressedP1)
            {
                if (keystate.IsKeyUp(Keys.D) && keystate.IsKeyUp(Keys.A))
                {

                    ispressedP1 = false;
                }
            }
            if (keystate.IsKeyDown(Keys.NumPad4))
            {
                if (!ispressedP2)
                {
                    ispressedP2 = true;
                    p2++;
                    switch (p2)
                    {
                        case 0:
                            Player2BodyLeft = TreeEntLeft;
                            break;
                        case 1:
                            Player2BodyLeft = lumberJackLeft;
                            break;
                        case 2:
                            Player2BodyLeft = SyrupLoverLeft;
                            break;
                        case 3:
                            Player2BodyLeft = HippieLeft;
                            break;
                        default:
                            p2 = 0;
                            Player2BodyLeft = TreeEntLeft;
                            break;
                    }
                }
            }
            if (keystate.IsKeyDown(Keys.NumPad6))
            {
                if (!ispressedP2)
                {
                    ispressedP2 = true;
                    p2--;
                    switch (p2)
                    {
                        case 0:
                            Player2BodyLeft = TreeEntLeft;
                            break;
                        case 1:
                            Player2BodyLeft = lumberJackLeft;
                            break;
                        case 2:
                            Player2BodyLeft = SyrupLoverLeft;
                            break;
                        case 3:
                            Player2BodyLeft = HippieLeft;
                            break;
                        default:
                            p2 = 3;
                            Player2BodyLeft = HippieLeft;
                            break;
                    }
                }
            }
            Random rand = new Random();
            if (ispressedP2)
            {
                if (keystate.IsKeyUp(Keys.NumPad6) && keystate.IsKeyUp(Keys.NumPad4))
                {
                    ispressedP2 = false;
                }
            }
            if (player1randRectangle.Contains(new Point(Mouse.GetState().X, Mouse.GetState().Y)) && Mouse.GetState().LeftButton == ButtonState.Pressed)
            {
                p1 = rand.Next(0, 4);
                Player1BodyLeft = UnknownSoldier;

            }
            if (player2randRectangle.Contains(new Point(Mouse.GetState().X, Mouse.GetState().Y)) && Mouse.GetState().LeftButton == ButtonState.Pressed)
            {
                p2 = rand.Next(0, 4);
                Player2BodyLeft = UnknownSoldier;
            }
            if (player1readyRectangle.Contains(new Point(Mouse.GetState().X, Mouse.GetState().Y)) && Mouse.GetState().LeftButton == ButtonState.Pressed)
            {
                player1Ready = true;
            }
            if (player2readyRectangle.Contains(new Point(Mouse.GetState().X, Mouse.GetState().Y)) && Mouse.GetState().LeftButton == ButtonState.Pressed)
            {
                player2Ready = true;
            }
            if (player1Ready && player2Ready)
            {
                switch (p1)
                {
                    case 0:
                        Player1BodyLeft = TreeEntLeft;
                        Player1BodyRight = TreeEntRight;
                        Player1Right = TreeEntPunchRight;
                        Player1Left = TreeEntPunchLeft;
                        players[0] = new TreeEnt(0 + (windowWidth / 4) - 50, ((windowHeight / 5) * 3) - 50, 0, 0, 0, 0, 0, 0);
                        break;
                    case 1:
                        Player1BodyLeft = lumberJackLeft;
                        Player1BodyRight = lumberJackRight;
                        Player1Right = lumberJackPunchRight;
                        Player1Left = lumberJackPunchLeft;
                        players[0] = new LumberJack(0 + (windowWidth / 4) - 50, ((windowHeight / 5) * 3) - 50, 0, 0, 0, 0, 0, 0);
                        break;
                    case 2:
                        Player1BodyLeft = SyrupLoverLeft;
                        Player1BodyRight = SyrupLoverRight;
                        Player1Right = SyrupLoverPunchRight;
                        Player1Left = SyrupLoverPunchLeft;
                        players[0] = new SyrupLover(0 + (windowWidth / 4) - 50, ((windowHeight / 5) * 3) - 50, 0, 0, 0, 0, 0, 0);
                        break;
                    case 3:
                        Player1BodyLeft = HippieLeft;
                        Player1BodyRight = HippieRight;
                        Player1Right = HippiePunchRight;
                        Player1Left = HippiePunchLeft;
                        players[0] = new GreenPeace(0 + (windowWidth / 4) - 50, ((windowHeight / 5) * 3) - 50, 0, 0, 0, 0, 0, 0);
                        break;
                }
                switch (p2)
                {
                    case 0:
                        Player2BodyLeft = TreeEntLeft;
                        Player2BodyRight = TreeEntRight;
                        Player2Right = TreeEntPunchRight;
                        Player2Left = TreeEntPunchLeft;
                        players[1] = new TreeEnt(windowWidth - (windowWidth / 4), ((windowHeight / 5) * 3) - 50, 0, 0, 0, 0, 0, windowWidth);
                        break;
                    case 1:
                        Player2BodyLeft = lumberJackLeft;
                        Player2BodyRight = lumberJackRight;
                        Player2Right = lumberJackPunchRight;
                        Player2Left = lumberJackPunchLeft;
                        players[1] = new LumberJack(windowWidth - (windowWidth / 4), ((windowHeight / 5) * 3) - 50, 0, 0, 0, 0, 0, windowWidth);
                        break;
                    case 2:
                        Player2BodyLeft = SyrupLoverLeft;
                        Player2BodyRight = SyrupLoverRight;
                        Player2Right = SyrupLoverPunchRight;
                        Player2Left = SyrupLoverPunchLeft;
                        players[1] = new SyrupLover(windowWidth - (windowWidth / 4), ((windowHeight / 5) * 3) - 50, 0, 0, 0, 0, 0, windowWidth);
                        break;
                    case 3:
                        Player2BodyLeft = HippieLeft;
                        Player2BodyRight = HippieRight;
                        Player2Right = HippiePunchRight;
                        Player2Left = HippiePunchLeft;
                        players[1] = new GreenPeace(windowWidth - (windowWidth / 4), ((windowHeight / 5) * 3) - 50, 0, 0, 0, 0, 0, windowWidth);
                        break;
                }
                state = gamestate.gameplay;
                theMotherFuckingGround = new Rectangle(0, windowHeight - ((windowHeight / 5) * 2), width: windowWidth, height: (windowHeight / 3));
                player1Avatar = players[0].bodyRectangle;
                player2Avatar = players[1].bodyRectangle;
                player1fist = players[0].fistRectangle;
                player2fist = players[1].fistRectangle;
            }
            //state = gamestate.gameplay;
        }

        private void playerMechanics(KeyboardState keystate, int playerNo)
        {
            players[playerNo].bodyRectangle = players[playerNo].bodyRectangle;
            players[playerNo].fistRectangle = players[playerNo].fistRectangle;

            int PlayerOldPosX = players[playerNo].bodyRectangle.X;
            int PlayerOldPosY = players[playerNo].bodyRectangle.Y;

            AttackPlayer1Timer.Elapsed += resetPlayer1Fist;
            CooldownPlayer1Timer.Elapsed += ResetPlayer1Cooldown;
            AttackPlayer2Timer.Elapsed += resetPlayer2Fist;
            CooldownPlayer2Timer.Elapsed += ResetPlayer2Cooldown;
            HitCooldownTimerP1.Elapsed += HitCooldownTimerP1_Elapsed; ;
            HitCooldownTimerP2.Elapsed += HitCooldownTimerP2_Elapsed; ;

            playermovement(keystate, playerNo);

            players[playerNo].bodyRectangle.X += (int)players[playerNo].XVelocity;
            players[playerNo].bodyRectangle.Y += (int)players[playerNo].YVelocity;
            if (players[playerNo].IsPunching)
            {
                players[playerNo].fistRectangle.X += (int)players[playerNo].XVelocity;
                players[playerNo].fistRectangle.Y += (int)players[playerNo].YVelocity;
            }
            if (!players[playerNo].IsJumping)
            {
                players[playerNo].XVelocity = players[playerNo].XVelocity * coefficientOfRestitution;
            }
            if (players[playerNo].IsJumping)
            {
                players[playerNo].YVelocity = players[playerNo].YVelocity + players[playerNo].Mass * g * 0.015f;
                if (players[playerNo].bodyRectangle.Y >= theMotherFuckingGround.Y - 50)
                {
                    players[playerNo].YVelocity = 0;
                    players[playerNo].bodyRectangle.Y = ((windowHeight / 5) * 3) - 50;

                    players[playerNo].IsJumping = false;
                    players[playerNo].IsSlamming = false;
                    players[playerNo].CanSlam = true;

                }
            }
            if (players[playerNo].bodyRectangle.Left < 1)
            {
                players[playerNo].bodyRectangle.X = PlayerOldPosX;
                players[playerNo].XVelocity = -players[playerNo].XVelocity * coefficientOfRestitution;
            }
            if (players[playerNo].bodyRectangle.Right > Window.ClientBounds.Width)
            {
                players[playerNo].bodyRectangle.X = PlayerOldPosX;
                players[playerNo].XVelocity = -players[playerNo].XVelocity * coefficientOfRestitution;
            }
        }

        private void HitCooldownTimerP2_Elapsed(object sender, ElapsedEventArgs e)
        {
            HitCooldownTimerP2.Enabled = false;
            players[1].CanPunch = true;
            players[1].IsHit = false;
        }

        private void HitCooldownTimerP1_Elapsed(object sender, ElapsedEventArgs e)
        {
            HitCooldownTimerP1.Enabled = false;
            players[0].CanPunch = true;
            players[0].IsHit = false;
        }

        private void playermovement(KeyboardState keystate, int playerNo)
        {
            switch (playerNo)
            {
                case 0:
                    if (keystate.IsKeyDown(Keys.A) && players[playerNo].bodyRectangle.Left > 1 && players[playerNo].XVelocity > -20 && !players[playerNo].IsJumping)
                    {
                        players[playerNo].XVelocity -= players[playerNo].SpeedMovement;
                    }
                    if (keystate.IsKeyDown(Keys.D) && players[playerNo].bodyRectangle.Right < Window.ClientBounds.Width && players[playerNo].XVelocity < 20 && !players[playerNo].IsJumping)
                    {
                        players[playerNo].XVelocity += players[playerNo].SpeedMovement;
                    }
                    if (keystate.IsKeyDown(Keys.W) && !players[playerNo].IsJumping)
                    {
                        players[playerNo].YVelocity = -players[playerNo].SpeedMovement;
                        players[playerNo].IsJumping = true;
                    }
                    if (keystate.IsKeyDown(Keys.E) && players[playerNo].CanPunch)
                    {
                        players[playerNo].IsPunching = true;
                        AttackPlayer1Timer.Interval = 150;
                        AttackPlayer1Timer.Enabled = true;
                        players[playerNo].CanPunch = false;
                        players[playerNo].fistRectangle.X = players[playerNo].bodyRectangle.X + players[playerNo].bodyRectangle.Width;
                        players[playerNo].fistRectangle.Y = players[playerNo].bodyRectangle.Y;
                        players[playerNo].RightPunch = true;
                    }
                    if (keystate.IsKeyDown(Keys.Q) && players[playerNo].CanPunch)
                    {
                        players[playerNo].IsPunching = true;
                        AttackPlayer1Timer.Interval = 150;
                        AttackPlayer1Timer.Enabled = true;
                        players[playerNo].CanPunch = false;
                        players[playerNo].fistRectangle.X = players[playerNo].bodyRectangle.X - players[playerNo].fistRectangle.Width;
                        players[playerNo].fistRectangle.Y = players[playerNo].bodyRectangle.Y;
                        players[playerNo].LeftPunch = true;
                    }
                    if (keystate.IsKeyDown(Keys.S) && players[playerNo].IsJumping && players[playerNo].CanSlam && !players[playerNo].IsHit)
                    {
                        players[playerNo].IsSlamming = true;

                        players[playerNo].CanSlam = false;
                        players[playerNo].YVelocity = 15;
                    }
                    break;

                case 1:
                    if (keystate.IsKeyDown(Keys.NumPad4) && players[playerNo].bodyRectangle.Left > 1 && players[playerNo].XVelocity > -20 && !players[playerNo].IsJumping)
                    {
                        players[playerNo].XVelocity -= players[playerNo].SpeedMovement;
                    }
                    if (keystate.IsKeyDown(Keys.NumPad6) && players[playerNo].bodyRectangle.Right < Window.ClientBounds.Width && players[playerNo].XVelocity < 20 && !players[playerNo].IsJumping)
                    {
                        players[playerNo].XVelocity += players[playerNo].SpeedMovement;
                    }
                    if (keystate.IsKeyDown(Keys.NumPad8) && !players[playerNo].IsJumping)
                    {
                        players[playerNo].YVelocity = -players[playerNo].SpeedMovement;
                        players[playerNo].IsJumping = true;
                    }
                    if (keystate.IsKeyDown(Keys.NumPad9) && players[playerNo].CanPunch)
                    {
                        players[playerNo].IsPunching = true;
                        AttackPlayer2Timer.Interval = 150;
                        AttackPlayer2Timer.Enabled = true;
                        players[playerNo].CanPunch = false;
                        players[playerNo].fistRectangle.X = players[playerNo].bodyRectangle.X + players[playerNo].bodyRectangle.Width;
                        players[playerNo].fistRectangle.Y = players[playerNo].bodyRectangle.Y;
                        players[playerNo].RightPunch = true;
                    }
                    if (keystate.IsKeyDown(Keys.NumPad7) && players[playerNo].CanPunch)
                    {
                        players[playerNo].IsPunching = true;
                        AttackPlayer2Timer.Interval = 150;
                        AttackPlayer2Timer.Enabled = true;
                        players[playerNo].CanPunch = false;
                        players[playerNo].fistRectangle.X = players[playerNo].bodyRectangle.X - players[playerNo].fistRectangle.Width;
                        players[playerNo].fistRectangle.Y = players[playerNo].bodyRectangle.Y;
                        players[playerNo].LeftPunch = true;
                    }
                    if (keystate.IsKeyDown(Keys.NumPad5) && players[playerNo].IsJumping && players[playerNo].CanSlam && !players[playerNo].IsHit)
                    {
                        players[playerNo].IsSlamming = true;

                        players[playerNo].CanSlam = false;
                        players[playerNo].YVelocity = 15;

                    }
                    break;
            }
        }

        private bool isDead(int playerNo)
        {
            if (players[playerNo].Health <= 0)
            {
                return true;
            }
            return false;
        }

        private void hitDetection()
        {

            if (players[0].fistRectangle.Intersects(players[1].bodyRectangle) && players[0].IsPunching && !players[1].IsHit) // and not hit
            {
                players[1].Health -= players[0].Damage;
                if (players[1].bodyRectangle.X < players[0].bodyRectangle.X)
                {
                    players[1].XVelocity = -players[0].Damage;
                    players[1].YVelocity = -players[0].Damage / 2;
                    players[1].IsJumping = true;
                }
                else
                {
                    players[1].XVelocity = players[0].Damage;
                    players[1].YVelocity = -players[0].Damage / 2;
                    players[1].IsJumping = true;
                }
                listOfPowerups.Add(new powerup(windowWidth, true));
                players[1].IsHit = true;
                HitCooldownTimerP2.Interval = 750;
                HitCooldownTimerP2.Enabled = true;
                players[1].CanPunch = false;
            }
            if (players[0].bodyRectangle.Intersects(players[1].bodyRectangle) && players[0].IsSlamming && !players[1].IsHit)
            {
                players[1].Health -= players[0].Damage;
                if (players[1].bodyRectangle.X < players[0].bodyRectangle.X)
                {
                    players[1].XVelocity = -players[0].Damage;
                    players[1].YVelocity = -players[0].Damage / 2;
                    players[1].IsJumping = true;
                }
                else
                {
                    players[1].XVelocity = players[0].Damage;
                    players[1].YVelocity = -players[0].Damage / 2;
                    players[1].IsJumping = true;
                }
                listOfPowerups.Add(new powerup(windowWidth, true));
                players[1].IsHit = true;
                HitCooldownTimerP2.Interval = 750;
                HitCooldownTimerP2.Enabled = true;
                players[1].CanPunch = false;
            }
            foreach (powerup p in listOfPowerups)
            {
                if (p.PowerUpRect.Intersects(players[0].bodyRectangle) && p.ActivePowerUp)
                {
                    players[0].Health += p.Health;
                    p.ActivePowerUp = false;
                }
            }


            if (players[1].fistRectangle.Intersects(players[0].bodyRectangle) && players[1].IsPunching && !players[0].IsHit)
            {
                players[0].Health -= players[1].Damage;

                if (players[0].bodyRectangle.X < players[1].bodyRectangle.X)
                {
                    players[0].XVelocity = -players[1].Damage;
                    players[0].YVelocity = -players[1].Damage / 2;
                    players[0].IsJumping = true;
                }
                else
                {
                    players[0].XVelocity = players[1].Damage;
                    players[0].YVelocity = -players[1].Damage / 2;
                    players[0].IsJumping = true;
                }
                listOfPowerups.Add(new powerup(windowWidth, true));
                players[0].IsHit = true;
                HitCooldownTimerP1.Interval = 750;
                HitCooldownTimerP1.Enabled = true;
                players[0].CanPunch = false;
            }
            if (players[1].bodyRectangle.Intersects(players[0].bodyRectangle) && players[1].IsSlamming && !players[0].IsHit)
            {
                players[0].Health -= players[1].Damage;
                if (players[0].bodyRectangle.X < players[1].bodyRectangle.X)
                {
                    players[0].XVelocity = -players[1].Damage;
                    players[0].YVelocity = -players[1].Damage / 2;
                    players[0].IsJumping = true;
                }
                else
                {
                    players[0].XVelocity = players[1].Damage;
                    players[0].YVelocity = -players[1].Damage / 2;
                    players[0].IsJumping = true;
                }
                listOfPowerups.Add(new powerup(windowWidth, true));
                players[0].IsHit = true;
                HitCooldownTimerP1.Interval = 750;
                HitCooldownTimerP1.Enabled = true;
                players[0].CanPunch = false;
            }
            foreach (powerup p in listOfPowerups)
            {
                if (p.PowerUpRect.Intersects(players[1].bodyRectangle) && p.ActivePowerUp)
                {
                    players[1].Health += p.Health;
                    p.ActivePowerUp = false;
                }
            }
        }

        protected void gameplayUpdate()
        {
            KeyboardState keystate = Keyboard.GetState();
            playerMechanics(keystate, 0);
            playerMechanics(keystate, 1);
            hitDetection();
            if (isDead(0) || isDead(1))
            {
                state = gamestate.gameover;
                //endgame player 2 win
            }
            List<powerup> powerupRemove = new List<powerup>();

            foreach (powerup item in powerupRemove)
            {
                if (item.PowerUpRect.Y >= Window.ClientBounds.Bottom)
                {
                    powerupRemove.Add(item);
                }
            }

            foreach (powerup item in powerupRemove)
            {
                listOfPowerups.Remove(item);
            }
            powerupRemove.Clear();

            player1Avatar.X = players[0].bodyRectangle.X;
            player1Avatar.Y = players[0].bodyRectangle.Y;
            player2Avatar.X = players[1].bodyRectangle.X;
            player2Avatar.Y = players[1].bodyRectangle.Y;
            player1fist.X = players[0].fistRectangle.X;
            player1fist.Y = players[0].fistRectangle.Y;
            player2fist.X = players[1].fistRectangle.X;
            player2fist.Y = players[1].fistRectangle.Y;

            foreach (powerup p in listOfPowerups)
            {
                p.rectangleYPos = p.Velocity;
            }
        }

        private void ResetPlayer2Cooldown(object sender, ElapsedEventArgs e)
        {
            players[1].CanPunch = true;
            CooldownPlayer2Timer.Enabled = false;
        }

        private void resetPlayer2Fist(object sender, ElapsedEventArgs e)
        {
            players[1].IsPunching = false;
            AttackPlayer2Timer.Enabled = false;
            CooldownPlayer2Timer.Enabled = true;
            CooldownPlayer2Timer.Interval = players[1].SpeedAttack;
            players[1].RightPunch = false;
        }

        private void ResetPlayer1Cooldown(object sender, ElapsedEventArgs e)
        {
            players[0].CanPunch = true;
            CooldownPlayer1Timer.Enabled = false;
        }

        private void resetPlayer1Fist(object sender, ElapsedEventArgs e)
        {
            players[0].IsPunching = false;
            AttackPlayer1Timer.Enabled = false;
            CooldownPlayer1Timer.Enabled = true;
            CooldownPlayer1Timer.Interval = players[0].SpeedAttack;
            players[0].RightPunch = false;
        }

        protected void gameOverUpdate()
        {
            players[0].XVelocity = 0;
            players[0].YVelocity = 0;
            players[1].XVelocity = 0;
            players[1].YVelocity = 0;
            players[0].IsHit = false;
            players[0].IsJumping = false;
            players[0].IsPunching = false;
            players[0].IsSlamming = false;
            players[1].IsHit = false;
            players[1].IsJumping = false;
            players[1].IsPunching = false;
            players[1].IsSlamming = false;

            KeyboardState keystate = Keyboard.GetState();
            if (keystate.IsKeyDown(Keys.Enter)) 
            {
                state = gamestate.startmenu;
            }

        }

        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.ForestGreen);
            switch (state)
            {
                case gamestate.startmenu:
                    startMenuDraw();
                    break;
                case gamestate.gameplay:
                    gameplayDraw();
                    break;
                case gamestate.gameover:
                    gameOverDraw();
                    break;
                default:
                    break;
            }

            // TODO: Add your drawing code here

            base.Draw(gameTime);
        }

        protected void startMenuDraw()
        {
            spriteBatch.Begin();
            spriteBatch.Draw(MainMenu, MainMenuRectangle, Color.White);
            spriteBatch.Draw(Player1BodyLeft, player1charRectangle, Color.White);
            spriteBatch.Draw(Player2BodyLeft, player2charRectangle, Color.White);

            spriteBatch.Draw(playerRand, player1randRectangle, Color.White);
            spriteBatch.Draw(playerRand, player2randRectangle, Color.White);

            spriteBatch.Draw(playerReady, player1readyRectangle, Color.White);
            spriteBatch.Draw(playerReady, player2readyRectangle, Color.White);
            spriteBatch.End();
        }
        protected void gameplayDraw()
        {
            spriteBatch.Begin();
            //spriteBatch.Draw(background, backgroundRect, Color.White);

            if (players[0].XVelocity < 0)
            {
                spriteBatch.Draw(Player1BodyLeft, player1Avatar, Color.White);
            }
            else
            {
                spriteBatch.Draw(Player1BodyRight, player1Avatar, Color.White);
            }
            if (players[0].IsPunching)
            {
                if (players[0].LeftPunch)
                {
                    spriteBatch.Draw(Player1Left, player1fist, Color.White);
                }
                if (players[0].RightPunch)
                {
                    spriteBatch.Draw(Player1Right, player1fist, Color.White);
                }
            }
            if (players[1].XVelocity < 0)
            {
                spriteBatch.Draw(Player2BodyLeft, player2Avatar, Color.White);
            }
            else
            {
                spriteBatch.Draw(Player2BodyRight, player2Avatar, Color.White);
            }
            if (players[1].IsPunching)
            {
                if (players[1].LeftPunch)
                {
                    spriteBatch.Draw(Player2Left, player2fist, Color.White);
                }
                if (players[1].RightPunch)
                {
                    spriteBatch.Draw(Player2Right, player2fist, Color.White);
                }
            }
            spriteBatch.Draw(background, theMotherFuckingGround, Color.Beige);
            spriteBatch.Draw(bar, players[0].Healthbar.HealthBarRect1, Color.White);
            spriteBatch.Draw(bar, players[1].Healthbar.HealthBarRect1, Color.White);
            foreach (powerup p in listOfPowerups)
            {
                if (p.ActivePowerUp)
                {
                    spriteBatch.Draw(powerup, p.PowerUpRect, Color.White);
                }
            }
            spriteBatch.End();
        }
        protected void gameOverDraw()
        {
            backgroundRect.Height = 450;
            spriteBatch.Begin();
            spriteBatch.Draw(GameOver, backgroundRect, Color.White);
            spriteBatch.Draw(Player1BodyLeft, player1Avatar, Color.White);
            spriteBatch.Draw(Player2BodyLeft, player2Avatar, Color.White);
            //spriteBatch.Draw(Player1BodyLeft, theMotherFuckingGround, Color.White);
            spriteBatch.Draw(bar, players[0].Healthbar.HealthBarRect1, Color.White);
            spriteBatch.Draw(bar, players[1].Healthbar.HealthBarRect1, Color.White);




            spriteBatch.End();
        }
    }
}
