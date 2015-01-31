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
    class Planet
    {
        public Vector2 Position;
        public double F;
        public double m;
        public double Diameter;
        Color Colour;

        public Planet(Vector2 pos, double dia, Color col)
        {
            Position = pos;
            Diameter = dia;
            Colour = col;
        }

        public void Update()
        {
        }
        
        public void Draw(SpriteBatch spriteBatch, Texture2D Texture)
        {
            Rectangle Rectangle = new Rectangle((int)Position.X - (int)Diameter / 2, (int)Position.Y - (int)Diameter / 2, (int)Diameter, (int)Diameter); ;
            
            spriteBatch.Draw(Texture, Rectangle, Colour);
        }
    }
}
