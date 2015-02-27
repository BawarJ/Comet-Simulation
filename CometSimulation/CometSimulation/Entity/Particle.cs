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
    class Particle
    {
        public Vector2 Position;
        public Vector2 Velocity;
        public float Angle;
        public float Length;
        public Color Colour;
        private Random random = new Random();

        public Particle(Vector2 pos, float ang, Color col, int Quadrant)
        {
            Position = pos;
            Angle = ang;
            Colour = col;
            Length = 200;

            if (Quadrant == 2) //TOP RIGHT QUADRANT
            {
                Velocity.X = (float)Math.Cos(Angle);
                Velocity.Y = (float)Math.Sin(Angle);
            }
            if (Quadrant == 1) //TOP LEFT QUADRANT
            {
                Velocity.X = (float)Math.Cos(MathHelper.ToRadians(360) - Angle);
                Velocity.Y = (float)Math.Sin(MathHelper.ToRadians(360) - Angle);
            }
            if (Quadrant == 4) //BOTTOM RIGHT QUADRANT
            {
                Velocity.X = (float)Math.Cos(MathHelper.ToRadians(180) - Angle);
                Velocity.Y = (float)Math.Sin(MathHelper.ToRadians(180) - Angle);
            }
            if (Quadrant == 3) //BOTTOM LEFT QUADRANT
            {
                Velocity.X = (float)Math.Cos(MathHelper.ToRadians(180) + Angle);
                Velocity.Y = (float)Math.Sin(MathHelper.ToRadians(180) + Angle);
            }
        }

        public void Update()
        {
            Position = Vector2.Add(Position, Velocity);
            Length--;
            Colour.R = (byte)(Length/200*255);
            Colour.G = (byte)(Length/200*255);
            Colour.B = (byte)(Length/20*255);
        }
    }
}
