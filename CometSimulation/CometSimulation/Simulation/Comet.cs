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
        #region Variables
        public Vector2 Position;
        public Vector2 Acceleration;
        public Vector2 Velocity;
        public Vector2 Force;
        public float F;
        public float Mass;
        public float Density;
        public float Diameter;
        Color tailColour;
        bool displayOrbit;
        List<Vector2> orbitTrail = new List<Vector2>();
        List<Vector2> gasParticles = new List<Vector2>();
        List<Particle> dustParticles = new List<Particle>();
        List<Particle> dustParticlesToRemove = new List<Particle>();
        Random rand = new Random();
        public Vector2 particleVelocity;
        public Vector2 gasDirection;
        #endregion

        public Comet(bool dispOrbit, Vector2 pos, Vector2 vel, float mass, float dens)
        {
            displayOrbit = dispOrbit;
            Position = pos;
            Velocity = vel;
            Mass = mass;
            Density = dens;
            //Diameter of the comet is calculated from the Mass and Density values
            Diameter = (mass / dens)*10;
            tailColour = new Color(255, 255, 255);
        }

        public void Update()
        {
            //Motion of the comet
            //Calculated using Newton's Second Law of Motion: F = ma
            Acceleration.X = Force.X / Mass;
            Acceleration.Y = Force.Y / Mass;

            //Calculates the velocity and position of the comet
            Velocity = Vector2.Add(Velocity, Acceleration);
            Position = Vector2.Add(Position, Velocity);

            //If the displayOrbit checkbox has been checked, draw the trail
            if (displayOrbit)
                orbitTrail.Add(Position);

            //Create gas ion tail
            for (int i = 0; i <= 99; i++)
            {
                //Uses vector equation to draw line of gas particles in the direction away from the sun
                gasParticles.Insert(i, new Vector2(Position.X + ((float)(rand.NextDouble() - 0.5) / 10 + Vector2.Normalize(gasDirection).X) * i * F/5,
                                                   Position.Y + ((float)(rand.NextDouble() - 0.5) / 10 + Vector2.Normalize(gasDirection).Y) * i * F/5));
                if (gasParticles.Count > 99)
                    //Removes all particles beyond 99 (only stores 100 particles)
                    gasParticles.RemoveRange(99, gasParticles.Count-100);
            }

            //Create dust tail
            dustParticles.Add(new Particle(Position, Color.LightGray, particleVelocity));

            //Update dust tail
            foreach (Particle p in dustParticles)
            {
                p.Update();
                //Remove particle if it is dead
                if (p.Length == 0)
                    dustParticlesToRemove.Add(p);
            }
            //Remove dust tail particles after they are dead
            foreach (Particle r in dustParticlesToRemove)
                dustParticles.Remove(r);
        }
        
        public void Draw(SpriteBatch spriteBatch, Texture2D Texture)
        {
            Rectangle Rectangle = new Rectangle((int)Position.X - (int)Diameter / 2, (int)Position.Y - (int)Diameter / 2, (int)Diameter, (int)Diameter);

            //If the displayOrbit checkbox has been checked, draw the trail
            if (displayOrbit)
                foreach (Vector2 t in orbitTrail)
                    spriteBatch.Draw(Texture, new Rectangle((int)t.X, (int)t.Y, 1, 1), Color.White);

            //Draws the dust tail
            foreach (Particle p in dustParticles)
                spriteBatch.Draw(Texture, new Rectangle((int)p.Position.X, (int)p.Position.Y, 2, 2), p.Colour);

            //Draws the gas ion tail
            for (int i = 0; i <= 99; i++)
            {
                Color gColour = new Color(tailColour.R - i*3, tailColour.G - i*2, tailColour.B - i);
                spriteBatch.Draw(Texture, new Rectangle((int)gasParticles[i].X, (int)gasParticles[i].Y, 2, 2), gColour);
            }

            spriteBatch.Draw(Texture, Rectangle, Color.White);
        }
    }
}
