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
    class Comet
    {
        public Vector2 Position;
        public Vector2 Acceleration;
        public Vector2 Velocity;
        public Vector2 Force;
        public double F;
        public double m;
        public double Diameter;
        public float Angle;
        Color Colour;
        List<Vector2> dots = new List<Vector2>();
        List<Particle> particles = new List<Particle>();
        List<Particle> particlesToRemove = new List<Particle>();
        Random rand = new Random();
        

        public Comet(Vector2 pos, double mass, double dia, float ang, Color col)
        {
            Position = pos;
            Diameter = dia;
            Colour = col;
            Angle = ang;
            m = mass;
            Velocity.Y = (float)rand.NextDouble();
        }

        public void Update()
        {
            Acceleration.X = Force.X / (float)m;
            Acceleration.Y = Force.Y / (float)m;

            dots.Add(Position);

            particles.Add(new Particle(Position, Angle));

            foreach (Particle p in particles)
            {
                p.Update();
                p.Angle = Angle;
                if (p.ttl == 0)
                    particlesToRemove.Add(p);
            }

            foreach (Particle r in particlesToRemove)
                particles.Remove(r);

            Velocity = Vector2.Add(Velocity, Acceleration);
            Position = Vector2.Add(Position, Velocity);
        }
        
        public void Draw(SpriteBatch spriteBatch, Texture2D Texture)
        {
            Rectangle Rectangle = new Rectangle((int)Position.X - (int)Diameter / 2, (int)Position.Y - (int)Diameter / 2, (int)Diameter, (int)Diameter);

            foreach (Vector2 d in dots)
                spriteBatch.Draw(Texture, new Rectangle((int)d.X, (int)d.Y, 1, 1), Colour);
            foreach (Particle p in particles)
                spriteBatch.Draw(Texture, new Rectangle((int)p.Position.X, (int)p.Position.Y, 2, 2), Color.DarkSlateBlue);

            spriteBatch.Draw(Texture, Rectangle, Colour);
        }
    }
}
