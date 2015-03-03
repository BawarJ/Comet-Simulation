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
        Rectangle rectMouse;
        Rectangle rectContainer;
        public Button btnComet;
        public Button btnPlanet;
        public Button btnReset;

        public Menu()
        {
            btnComet = new Button("Comet", Width, 100);
            btnPlanet = new Button("Planet", Width, 200);
            btnReset = new Button("Reset", Width, 500);
        }

        public void Update()
        {
            ms = Mouse.GetState();
            rectMouse = new Rectangle(ms.X, ms.Y, 1, 1);
            rectContainer = new Rectangle(X, 0, Width, 768);

            if (ms.X < 10 || rectMouse.Intersects(rectContainer))
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

            //update menu items below
            btnComet.Update(X);
            btnPlanet.Update(X);
            btnReset.Update(X);
        }

        public void Draw(SpriteBatch spriteBatch, SpriteFont font, Texture2D tex)
        {
            if (showMenu == true)
            {
                spriteBatch.Draw(tex, rectContainer, Color.White);

                //draw menu items below
                spriteBatch.DrawString(font, "Comet Simulation", new Vector2(X + 20 , 10), Color.Black);
                btnComet.Draw(spriteBatch, tex, font, X);
                btnPlanet.Draw(spriteBatch, tex, font, X);
                btnReset.Draw(spriteBatch, tex, font, X);
            }
        }
    }
}
