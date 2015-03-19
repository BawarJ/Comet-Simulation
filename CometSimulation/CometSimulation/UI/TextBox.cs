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
        byte x;
        bool isHovering;
        public bool isClicking;
        public bool inFocus;
        int Width;
        int Y;
        Rectangle mousePos;
        MouseState pms;
        MouseState ms;
        Color Colour;
        Color cursorColour;
        public KbHandler kb = new KbHandler();
        public string textInput = "";
        int cursorPosition;

        public TextBox(int wid, int y)
        {
            Width = wid - 20;
            Y = y;
            Colour = new Color(255, 255, 255);
            cursorColour = new Color(0, 0, 0);
        }

        public void Update(GameTime gameTime)
        {
            ms = Mouse.GetState();
            mousePos = new Rectangle(ms.X, ms.Y, 1, 1);

            if (mousePos.Intersects(new Rectangle(0, Y, Width, 50)))
            {
                isHovering = true;
                Colour.R = 245;
            }
            else
            {
                isHovering = false;
                Colour.R = 255;
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
                Colour.R = 230;
                x = (byte)(gameTime.TotalGameTime.Milliseconds/4);
                cursorColour.A = x;
                if (kb.text != "")
                    textInput = kb.text;
            }
            else
            {
                kb.text = "";
            }

            cursorPosition = textInput.Length;
            kb.Update();
            pms = Mouse.GetState();
        }

        public void Draw(SpriteBatch spriteBatch, Texture2D Texture, SpriteFont Font)
        {
            spriteBatch.Draw(Texture, new Rectangle(20, Y, Width - 20, 50), Colour);
            
            spriteBatch.DrawString(Font, textInput, new Vector2(25, Y + 10), Color.Black);
            if (inFocus)
            {
                spriteBatch.Draw(Texture, new Rectangle(25 + 10 * cursorPosition, Y + 10, 1, 30), cursorColour); //draws cursor

                if (kb.minus == -1)
                    spriteBatch.DrawString(Font, "-", new Vector2(7, Y + 10), Color.Black);
            }
        }
    }
}
