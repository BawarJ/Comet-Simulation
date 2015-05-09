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
        #region Variables
        public float Value;
        float currentValue;
        bool isHovering;
        public bool isClicking;
        int Width = 160;
        int Y;
        Rectangle mousePos;
        MouseState ms;
        MouseState pms;
        Color Colour;
        string Message;
        float Minimum;
        float Maximum;
        #endregion

        public Slider(float min, float max, string msg, int y)
        {
            #region Variables
            Y = y;
            Colour = new Color(100, 100, 100);
            Message = msg;
            Minimum = min;
            Maximum = max;
            currentValue = Width / 2;
            #endregion
        }

        public void Update(GameTime gameTime, int menuX)
        {
            pms = ms;
            ms = Mouse.GetState();
            mousePos = new Rectangle(ms.X, ms.Y, 1, 1);

            //If the mouse is hovering on the slider, highlight the slider colour
            //Otherwise set to default colour
            if (mousePos.Intersects(new Rectangle(menuX + 20, Y, Width, 50)))
            {
                isHovering = true;
                Colour.B = 200;
                Colour.R = 25;
            }
            else
            {
                isHovering = false;
                Colour.B = 100;
                Colour.R = 100;
            }

            //If the mouse is being clicked, set the new slider value to the current mouse position
            if (isHovering && ms.LeftButton == ButtonState.Pressed && pms.LeftButton == ButtonState.Pressed)
            {
                isClicking = true;
                currentValue = ms.X - 20;
            }
            else
            {
                isClicking = false;
                Colour.B = 100;
                Colour.R = 100;
            }

            //Rounds slider value
            Value = Minimum + (float)Math.Round((currentValue/Width)*Maximum*2);
        }

        public void Draw(SpriteBatch spriteBatch, Texture2D Texture, SpriteFont Font, int menuX)
        {
            spriteBatch.DrawString(Font, Message, new Vector2(menuX + 25, Y - 30), Color.Black);
            spriteBatch.Draw(Texture, new Rectangle(menuX + 20, Y, Width, 30), Color.Snow);
            spriteBatch.Draw(Texture, new Rectangle(menuX + 20, Y, (int)currentValue, 30), Colour);
            if (isHovering)
                //Draws numeric value if mouse is hovering on slider
                spriteBatch.DrawString(Font, Value.ToString(), new Vector2(menuX + 22 + currentValue, Y), Colour);
        }
    }
}
