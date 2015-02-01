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
    class Menu
    {
        Boolean showMenu;
        int X = 0;
        int Inc = 8;
        int Width = 200;
        MouseState ms;
        Rectangle mouseRectangle;
        Rectangle contRectangle;

        public Menu()
        {
        }

        public void Update()
        {
            ms = Mouse.GetState();
            mouseRectangle = new Rectangle(ms.X, ms.Y, 1, 1);
            contRectangle = new Rectangle(X, 0, Width, 768);

            if (ms.X < 10 || mouseRectangle.Intersects(contRectangle))
            {
                showMenu = true;
                if (X < 0)
                    X += Inc;
            }
            else
            {
                if (X > 0-Width)
                    X -= Inc;
                if (X <= 0-Width)
                {
                    X = -Width;
                    showMenu = false;
                }
            } 
        }

        public void Draw(SpriteBatch spriteBatch, SpriteFont font, Texture2D tex)
        {
            if (showMenu == true)
            {
                spriteBatch.Draw(tex, contRectangle, Color.White);

                //draw menu items below
                spriteBatch.DrawString(font, "Comet Simulation", new Vector2(X + 20 , 10), Color.Black);
            }
        }
    }
}
