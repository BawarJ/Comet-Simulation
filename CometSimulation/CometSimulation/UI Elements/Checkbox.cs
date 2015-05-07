using System;
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
        MouseState ms;
        MouseState pms;
        string Message;
        Color Colour;

        public Checkbox(string msg, int y)
        {
            Message = msg;
            Y = y;
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
                Colour = new Color(150,150,150);
            else
                Colour = Color.White;

            if (Clicked)
            {
                if (isChecked)
                    isChecked = false;
                else if (!isChecked)
                    isChecked = true;
            }
            
            pms = ms;

        }

        public void Draw(SpriteBatch spriteBatch, List<Texture2D> Texture, SpriteFont Font, int menuX)
        {
            if (isChecked)
                spriteBatch.Draw(Texture[1], new Rectangle(menuX + 20, Y + 10, 30, 30), Colour);
            else
                spriteBatch.Draw(Texture[0], new Rectangle(menuX + 20, Y+10, 30, 30), Colour);

            spriteBatch.DrawString(Font, Message, new Vector2(menuX + 50, Y + 10), Color.Black);
        }
    }
}
