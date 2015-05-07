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
        public float Length;
        public Color Colour;
        public Color startingColour;
        private Random random = new Random();

        public Particle(Vector2 pos, Color col, Vector2 vel)
        {
            Position = pos;
            Colour = col;
            startingColour = col;
            Length = 40;
            Velocity = vel;
        }

        public void Update()
        {
            Position = Vector2.Add(Position, Velocity);
            Position = Vector2.Add(Position, new Vector2((float)random.NextDouble() - 0.5f, (float)random.NextDouble() - 0.5f));
            Length--;

            //colour fades out towards end of the particles life
            Colour.R = (byte)(Length / 40 * startingColour.R);
            Colour.G = (byte)(Length / 40 * startingColour.G);
            Colour.B = (byte)(Length / 40 * startingColour.B);
        }
    }
}
