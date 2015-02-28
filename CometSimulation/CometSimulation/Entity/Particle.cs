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
        const float PI = MathHelper.Pi;
        public Vector2 Position;
        public Vector2 Velocity;
        public int Quadrant;
        public float Angle;
        public float Length;
        public Color Colour;
        private Random random = new Random();

        public Particle(Vector2 pos, float ang, Color col, int quad)
        {
            Position = pos;
            Angle = ang;
            Colour = col;
            Length = 50;
            Quadrant = quad;
        }

        public void Update()
        {
            if (Quadrant == 1) //TOP RIGHT QUADRANT
            {
                Velocity.X = (float)Math.Sin(Angle);
                Velocity.Y = (float)Math.Cos(Angle);
            }
            if (Quadrant == 2) //TOP LEFT QUADRANT
            {
                Velocity.X = (float)Math.Sin(Angle);
                Velocity.Y = (float)Math.Cos(Angle);
            }
            if (Quadrant == 3) //BOTTOM LEFT QUADRANT
            {
                Velocity.X = (float)Math.Sin(2*PI-Angle);
                Velocity.Y = (float)Math.Cos(2*PI-Angle);
            }
            if (Quadrant == 4) //BOTTOM RIGHT QUADRANT
            {
                Velocity.X = (float)Math.Sin(Angle);
                Velocity.Y = (float)Math.Cos(Angle);
            }

            Position = Vector2.Add(Position, Velocity);
            Length--;
            Colour.R = (byte)(Length/50*255);
            Colour.G = (byte)(Length/50*255);
            Colour.B = (byte)(Length/50*255);
        }
    }
}
