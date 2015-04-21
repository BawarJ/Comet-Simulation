﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;

namespace CometSimulation
{
    class Checkbox
    {
        public bool isChecked;
        bool isHovering;
        public bool Clicked;
        int Width = 160;
        int Y;
        Rectangle mousePos;
        string Message;
        MouseState pms;
        MouseState ms;
        Color Colour;

        public Checkbox(string msg, int y)
        {
            Message = msg;
            Y = y;
            Colour = new Color(200, 200, 200);
        }

        public void Update(int menuX)
        {
            
            ms = Mouse.GetState();
            mousePos = new Rectangle(ms.X, ms.Y, 1, 1);

            if (mousePos.Intersects(new Rectangle(menuX+20, Y, Width, 50)))
                isHovering = true;
            else
                isHovering = false;

            if (isHovering && pms.LeftButton == ButtonState.Released && ms.LeftButton == ButtonState.Pressed)
                Clicked = true;
            else
                Clicked = false;

            if (isHovering)
                Colour.B = 180;
            else
                Colour.B = 200;

            if (Clicked)
            {
                if (isChecked)
                    isChecked = false;
                else if (!isChecked)
                    isChecked = true;
            }

            if (isChecked)
                Colour.G = 255;
            else
                Colour.G = 200;

            pms = ms;

        }

        public void Draw(SpriteBatch spriteBatch, Texture2D Texture, SpriteFont Font, int menuX)
        {
            spriteBatch.Draw(Texture, new Rectangle(menuX + 20, Y+10, 30, 30), Colour);
            spriteBatch.DrawString(Font, Message, new Vector2(menuX + 50, Y + 10), Color.Black);
        }
    }
}