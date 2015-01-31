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
        Boolean showMenu = false;
        int X = 0;

        public Menu()
        {
        }

        public void Update()
        {
            if (Mouse.GetState().X < 10)
            {
                showMenu = true;
                if (X <= 200)
                    X += 8;
            }
            else
            {
                if (X > 0)
                    X -= 8;
                if (X <= 0)
                {
                    X = 0;
                    showMenu = false;
                }
            }
        }
        
        public void Draw(SpriteBatch spriteBatch, Texture2D tex)
        {
            Rectangle Rectangle = new Rectangle(0, 0, X, 768);
            if (showMenu == true)
                spriteBatch.Draw(tex ,Rectangle, Color.White);
        }
    }
}
