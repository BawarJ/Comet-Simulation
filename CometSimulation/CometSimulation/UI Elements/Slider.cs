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
    class Slider
    {
        public float Value;
        float Size;
        bool isHovering;
        public bool isClicking;
        int Width = 160;
        int Y;
        Rectangle mousePos;
        MouseState pms;
        MouseState ms;
        Color Colour;
        string Message;
        float Minimum;
        float Maximum;

        public Slider(float min, float max, string msg, int y)
        {
            Y = y;
            Colour = new Color(100, 100, 100);
            Message = msg;
            Minimum = min;
            Maximum = max;
            Size = Width / 2;
        }

        public void Update(GameTime gameTime, int menuX)
        {
            pms = ms;
            ms = Mouse.GetState();
            mousePos = new Rectangle(ms.X, ms.Y, 1, 1);

            if (mousePos.Intersects(new Rectangle(menuX + 20, Y, Width, 50)))
            {
                isHovering = true;
                Colour.B = 140;
            }
            else
            {
                isHovering = false;
                Colour.B = 100;
            }
            
            if (isHovering && ms.LeftButton == ButtonState.Pressed && pms.LeftButton == ButtonState.Pressed)
            {
                isClicking = true;
                Colour.B = 180;
                Size = ms.X - 20;
            }
            else
                isClicking = false;

            Value = Minimum + (float)Math.Round((Size/Width)*Maximum*2);
        }

        public void Draw(SpriteBatch spriteBatch, Texture2D Texture, SpriteFont Font, int menuX)
        {
            spriteBatch.DrawString(Font, Message, new Vector2(menuX + 25, Y - 30), Color.Black);
            spriteBatch.Draw(Texture, new Rectangle(menuX + 20, Y, Width, 30), Color.Snow);
            spriteBatch.Draw(Texture, new Rectangle(menuX + 20, Y, (int)Size, 30), Colour);
            spriteBatch.DrawString(Font, Value.ToString(), new Vector2(menuX + 25, Y), Color.White);
        }
    }
}
