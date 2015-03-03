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
    class TextBox
    {
        bool isHovering;
        public bool isClicking;
        public bool inFocus;
        int Width;
        int Y;
        Rectangle mousePos;
        MouseState pms;
        MouseState ms;
        Color Colour;
        public KbHandler kb = new KbHandler();
        public string textInput = "";

        public TextBox(int wid, int y)
        {
            Width = wid - 20;
            Y = y;
            Colour = new Color(200, 200, 200);
        }

        public void Update()
        {
            ms = Mouse.GetState();
            mousePos = new Rectangle(ms.X, ms.Y, 1, 1);

            if (mousePos.Intersects(new Rectangle(0, Y, Width, 50)))
            {
                isHovering = true;
                Colour.R = 100;
            }
            else
            {
                isHovering = false;
                Colour.R = 200;
            }
            
            if (isHovering && ms.LeftButton == ButtonState.Pressed && pms.LeftButton == ButtonState.Released)
            {
                isClicking = true;
                inFocus = true;
            }
            else
                isClicking = false;

            if (!isHovering && ms.LeftButton == ButtonState.Pressed && pms.LeftButton == ButtonState.Released)
                inFocus = false;

            if (inFocus)
            {
                Colour.G = 100;
                Colour.R = 200;
            }
            else
            {
                Colour.G = 200;
                kb.tekst = "";
            }
            
            pms = Mouse.GetState();
            kb.Update();
        }

        public void Draw(SpriteBatch spriteBatch, Texture2D Texture, SpriteFont Font)
        {
            spriteBatch.Draw(Texture, new Rectangle(20, Y, Width - 20, 50), Colour);
            spriteBatch.DrawString(Font, textInput, new Vector2(50, Y + 10), Color.Black);
        }
    }
}
