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
        public float Value;
        bool isHovering;
        public bool isClicking;
        public bool inFocus;
        int Width = 160;
        int Y;
        Rectangle mousePos;
        MouseState pms;
        MouseState ms;
        Color Colour;
        Color cursorColour;
        public KbHandler kb = new KbHandler();
        public string textInput = "0";
        string Message;
        float Minimum;
        float Maximum;
        Color textColour;

        public TextBox(string msg, float min, float max, int y)
        {
            Y = y;
            Colour = new Color(255, 255, 255);
            cursorColour = new Color(0, 0, 0);
            textColour = Color.Black;
            Message = msg;
            Minimum = min;
            Maximum = max;
        }

        public void Update(GameTime gameTime, int menuX)
        {
            kb.Update();
            Console.WriteLine(kb.text);
            #region Mouse Stuff
            pms = ms;
            ms = Mouse.GetState();
            mousePos = new Rectangle(ms.X, ms.Y, 1, 1);

            if (mousePos.Intersects(new Rectangle(menuX+20, Y, Width, 50)))
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
                textInput = Value.ToString();
                kb.text = textInput;
            }
            else
                isClicking = false;

            if (!isHovering && ms.LeftButton == ButtonState.Pressed && pms.LeftButton == ButtonState.Released)
                inFocus = false;
            #endregion

            if (inFocus)
            {

                textInput = kb.text;

                Colour.R = 230;
                cursorColour.A = (byte)(gameTime.TotalGameTime.Milliseconds/4);
                if (kb.text != "")
                {
                    if (Value >= Minimum && Value <= Maximum)
                    {
                        textColour = Color.Black;
                        Value = float.Parse(kb.text);
                    }
                    else
                    {
                        textColour = Color.Red;
                        Value = float.Parse(kb.text);
                    }
                }
            }
        }

        public void Draw(SpriteBatch spriteBatch, Texture2D Texture, SpriteFont Font, int menuX)
        {
            spriteBatch.Draw(Texture, new Rectangle(menuX + 20, Y, Width, 50), Colour);
            spriteBatch.DrawString(Font, Message, new Vector2(menuX + 20, Y - 30), Color.Black);
            spriteBatch.DrawString(Font, textInput, new Vector2(menuX + 25, Y + 10), textColour);
            if (inFocus)
            {
                spriteBatch.Draw(Texture, new Rectangle(menuX + 25 + (int)Font.MeasureString(textInput).X, Y + 10, 1, 30), cursorColour); //draws cursor
            }
        }
    }
}
