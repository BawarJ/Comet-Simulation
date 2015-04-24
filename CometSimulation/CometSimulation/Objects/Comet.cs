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
        public float F;
        public float m;
        public float Diameter;
        Color tailColour;
        bool displayOrbit;
        List<Vector2> dots = new List<Vector2>();
        List<Vector2> gasParticles = new List<Vector2>();
        List<Particle> dustParticles = new List<Particle>();
        List<Particle> dustParticlesToRemove = new List<Particle>();
        Random rand = new Random();
        public Vector2 particleVelocity;
        public Vector2 gasDirection;

        public Comet(bool dispOrbit, Vector2 pos, Vector2 vel, float mass, float dens)
        {
            displayOrbit = dispOrbit;
            Position = pos;
            m = mass;
            Diameter = (mass / dens)*10;
            tailColour = new Color(255, 255, 255);
            Velocity = vel;
        }

        public void Update()
        {
            //motion of the comet
            Acceleration.X = Force.X / m;
            Acceleration.Y = Force.Y / m;
            Velocity = Vector2.Add(Velocity, Acceleration);
            Position = Vector2.Add(Position, Velocity);

            if (displayOrbit)
                dots.Add(Position); //orbit line

            //create gas tail
            for (int i = 0; i <= 99; i++)
            {
                gasParticles.Insert(i, new Vector2(Position.X + ((float)(rand.NextDouble() - 0.5) / 10 + Vector2.Normalize(gasDirection).X) * i * F/5,
                                                   Position.Y + ((float)(rand.NextDouble() - 0.5) / 10 + Vector2.Normalize(gasDirection).Y) * i * F/5));
                if (gasParticles.Count > 99)
                    gasParticles.RemoveRange(99, gasParticles.Count-100);
            }

            //create dust tail
            dustParticles.Add(new Particle(Position, Color.Gray, particleVelocity));

            foreach (Particle p in dustParticles)
            {
                p.Update();
                if (p.Length == 0)
                    dustParticlesToRemove.Add(p);
            }

            foreach (Particle r in dustParticlesToRemove)
                dustParticles.Remove(r);
        }
        
        public void Draw(SpriteBatch spriteBatch, Texture2D Texture)
        {
            Rectangle Rectangle = new Rectangle((int)Position.X - (int)Diameter / 2, (int)Position.Y - (int)Diameter / 2, (int)Diameter, (int)Diameter);
            
            if (displayOrbit)
                foreach (Vector2 d in dots)
                    spriteBatch.Draw(Texture, new Rectangle((int)d.X, (int)d.Y, 1, 1), Color.White);

            foreach (Particle p in dustParticles)
                spriteBatch.Draw(Texture, new Rectangle((int)p.Position.X, (int)p.Position.Y, 2, 2), p.Colour);
            for (int i = 0; i <= 99; i++)
            {
                Color gColour = new Color(tailColour.R - i*3, tailColour.G - i*2, tailColour.B - i);
                spriteBatch.Draw(Texture, new Rectangle((int)gasParticles[i].X, (int)gasParticles[i].Y, 2, 2), gColour);
            }

            spriteBatch.Draw(Texture, Rectangle, Color.White);
        }
    }
}
