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
        public Vector2 Acceleration;
        public Vector2 Velocity;
        public Vector2 Force;
        public double F;
        public double m;
        public double Diameter;
        Color Colour;
        List<Vector2> dots = new List<Vector2>();
        Random rand = new Random();


        public Planet(Vector2 pos, double mass, double dia, Color col)
        {
            Position = pos;
            Diameter = dia;
            Colour = col;
            m = mass;
            Velocity.Y = 1;
        }

        public void Update()
        {
            Acceleration.X = Force.X / (float)m;
            Acceleration.Y = Force.Y / (float)m;

            dots.Add(Position);

            Velocity = Vector2.Add(Velocity, Acceleration);
            Position = Vector2.Add(Position, Velocity);
        }

        public void Draw(SpriteBatch spriteBatch, Texture2D Texture)
        {
            Rectangle Rectangle = new Rectangle((int)Position.X - (int)Diameter / 2, (int)Position.Y - (int)Diameter / 2, (int)Diameter, (int)Diameter);

            foreach (Vector2 d in dots)
                spriteBatch.Draw(Texture, new Rectangle((int)d.X, (int)d.Y, 1, 1), Colour);

            spriteBatch.Draw(Texture, Rectangle, Colour);
        }
    }
}
