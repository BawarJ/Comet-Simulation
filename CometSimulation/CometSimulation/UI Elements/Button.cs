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
    class Button
    {
        #region Variables
        bool isHovering;
        public bool Clicked;
        int Width = 160;
        int Y;
        Rectangle mousePos;
        MouseState ms;
        MouseState pms;
        string Message;
        Color Colour;
        #endregion

        public Button(string msg, int y)
        {
            Message = msg;
            Y = y;
            Colour = new Color(200, 200, 200);
        }

        public void Update(int menuX)
        {
            ms = Mouse.GetState();
            mousePos = new Rectangle(ms.X, ms.Y, 1, 1);

            //Checks if mouse is hovering on button
            if (mousePos.Intersects(new Rectangle(menuX+20, Y, Width, 50)))
                isHovering = true;
            else
                isHovering = false;

            //Checks if button has been clicked
            if (isHovering && pms.LeftButton == ButtonState.Released && ms.LeftButton == ButtonState.Pressed)
                Clicked = true;
            else
                Clicked = false;

            //Highlight button if it is being hovered over
            if (isHovering)
            {
                Colour.R = 150;
                Colour.G = 150;
                Colour.B = 150;
            }
            else
            {
                Colour.R = 200;
                Colour.G = 200;
                Colour.B = 200;
            }

            pms = ms;

        }

        public void Draw(SpriteBatch spriteBatch, Texture2D Texture, SpriteFont Font, int menuX)
        {
            //Draws the button and accompanying text
            spriteBatch.Draw(Texture, new Rectangle(menuX + 20, Y, Width, 50), Colour);
            spriteBatch.DrawString(Font, Message, new Vector2(menuX + 15 + Width/2 - Font.MeasureString(Message).X/2, Y + 15), Color.Black);
        }
    }
}
