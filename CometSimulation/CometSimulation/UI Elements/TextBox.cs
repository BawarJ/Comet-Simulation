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
    class Textbox
    {
        #region Variables
        public float Value;
        bool isHovering;
        public bool isClicking;
        public bool inFocus;
        int Width = 160;
        int Y;
        Rectangle mousePos;
        MouseState ms;
        MouseState pms;
        Color Colour;
        Color cursorColour;
        public kbHandler kb = new kbHandler();
        public string textInput = "0";
        string Message;
        float Minimum;
        float Maximum;
        #endregion

        public Textbox(string msg, float min, float max, int y)
        {
            Y = y;
            Colour = new Color(255, 255, 255);
            cursorColour = new Color(0, 0, 0);
            Message = msg;
            Minimum = min;
            Maximum = max;
        }

        public void Update(GameTime gameTime, int menuX)
        {
            //private variable
            bool Valid = true;

            //Updates the kbHandler object
            kb.Update();

            pms = ms;
            ms = Mouse.GetState();
            mousePos = new Rectangle(ms.X, ms.Y, 1, 1);

            //Checks if the mouse is hovering on the textbox
            //Highlights the textbox if it is hovering
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
            
            //Checks if the mouse is clicking on the textbox
            if (isHovering && ms.LeftButton == ButtonState.Pressed && pms.LeftButton == ButtonState.Released)
            {
                isClicking = true;
                inFocus = true;

                //If the input is valid, set the contents of the textbox equal to its numeric value
                //Otherwise, set the textbox to be empty, ready for user input
                if (Valid)
                {
                    textInput = Value.ToString();
                    kb.text = textInput;
                }
                else
                    textInput = kb.text = "";
            }
            else
                isClicking = false;

            if (!isHovering && ms.LeftButton == ButtonState.Pressed && pms.LeftButton == ButtonState.Released)
                inFocus = false;

            if (inFocus)
            {
                Colour.R = 230; //sets the textbox colour to a darker blue to indicate that it is in focus
                cursorColour.A = (byte)(gameTime.TotalGameTime.Milliseconds / 4); //cursor blinking

                //Parses the string input into a float value to be used by to set the object variables
                Valid = float.TryParse(textInput, out Value);

                //If the Value is valid and within the acceptable range, output its text equivalent
                //Otherwise display error message
                if (Valid && Value >= Minimum && Value <= Maximum)
                    textInput = kb.text;
                else
                    textInput = "INVALID INPUT!\nClick here to reset";
            }
        }

        public void Draw(SpriteBatch spriteBatch, Texture2D Texture, SpriteFont Font, int menuX)
        {
            spriteBatch.Draw(Texture, new Rectangle(menuX + 20, Y, Width, 50), Colour);
            spriteBatch.DrawString(Font, Message, new Vector2(menuX + 20, Y - 30), Color.Black);
            spriteBatch.DrawString(Font, textInput, new Vector2(menuX + 25, Y + 5), Color.Black);
            if (inFocus)
            {
                //draws cursor
                spriteBatch.Draw(Texture, new Rectangle(menuX + 25 + (int)Font.MeasureString(textInput).X, Y + 10, 1, 30), cursorColour);
            }
        }
    }
}
