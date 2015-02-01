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
    class Star
    {
        public Vector2 Position;
        public double m;
        public double Diameter;
        //List<Particle> particles = new List<Particle>();
        //List<Particle> particlesToRemove = new List<Particle>();
        Color Colour;

        public Star(Vector2 pos, double mass, double dia, Color col)
        {
            Position = pos;
            Diameter = dia;
            Colour = col;
            m = mass;
        }

        public void Update()
        {
            /*
            particles.Add(new Particle(Position, 0, Colour));

            foreach (Particle p in particles)
            {
                p.Update();
                p.Angle+= 0.1f;
                if (p.ttl == 0)
                    particlesToRemove.Add(p);
            }

            foreach (Particle r in particlesToRemove)
                particles.Remove(r);
             */
        }
        
        public void Draw(SpriteBatch spriteBatch, Texture2D Texture)
        {
            Rectangle Rectangle = new Rectangle((int)Position.X - (int)Diameter/2, (int)Position.Y - (int)Diameter/2, (int)Diameter, (int)Diameter);

            /*
            foreach (Particle p in particles)
                spriteBatch.Draw(Texture, new Rectangle((int)p.Position.X, (int)p.Position.Y, 2, 2), p.Colour);
            */

            spriteBatch.Draw(Texture, Rectangle, Colour);
        }
    }
}
