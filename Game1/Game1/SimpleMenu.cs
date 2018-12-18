﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;


namespace SyrupIsSlaughter
{
    class SimpleMenu
    {
        private Texture2D GUITexture;

        private Rectangle GUIRect;

        private string assetName;

        public delegate void ElementClicked(string element);

        public event ElementClicked clickEvent;

        public SimpleMenu(string assetName)
        {
            this.assetName = assetName;
        }

        public void LoadContent(ContentManager content)
        {
            GUITexture = content.Load<Texture2D>(assetName);
            GUIRect = new Rectangle(0, 0, GUITexture.Width, GUITexture.Height);

        }

        public void Update()
        {

        }

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(GUITexture, GUIRect, Color.White);
        }

        public void CenterElement(int height, int width)
        {
            GUIRect = new Rectangle((width / 2) - (this.GUITexture.Width / 2), (height / 2) - (this.GUITexture.Height / 2), this.GUITexture.Width, this.GUITexture.Height);
        }

        public void MoveElement(int x, int y)
        {
            GUIRect = new Rectangle(GUIRect.X += x, GUIRect.Y += y, GUIRect.Width, GUIRect.Height);

        }

    }
}