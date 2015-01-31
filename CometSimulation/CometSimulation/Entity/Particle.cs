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
        public float Angle;
        public float ttl;
        private Random random = new Random();

        public Particle(Vector2 pos, float ang)
        {
            Position = pos;
            Angle = ang;
            ttl = 100;
        }

        public void Update()
        {
            Position.X += (float)Math.Cos(Angle) + (float)random.NextDouble();
            Position.Y += (float)Math.Sin(Angle) + (float)random.NextDouble();
            ttl--;
        }
    }
}
