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
        public float F;
        public float m;
        public float density;
        public float Diameter;
        Color Colour;
        bool displayOrbit;
        List<Vector2> orbitTrail = new List<Vector2>();
        Random rand = new Random();


        public Planet(bool dispOrbit, Vector2 pos, Vector2 vel, float mass, float dens)
        {
            displayOrbit = dispOrbit;
            Position = pos;
            m = mass;
            density = dens;
            Diameter = (mass / dens) * 10;
            Colour = new Color((float)rand.NextDouble(),(float)rand.NextDouble(),(float)rand.NextDouble());
            Velocity = vel;
        }

        public void Update()
        {Acceleration.X = Force.X / m;
            Acceleration.Y = Force.Y / m;

            if (displayOrbit)
                orbitTrail.Add(Position);
            
            Velocity = Vector2.Add(Velocity, Acceleration);
            Position = Vector2.Add(Position, Velocity);
        }

        public void Draw(SpriteBatch spriteBatch, Texture2D Texture)
        {
            Rectangle Rectangle = new Rectangle((int)Position.X - (int)Diameter / 2, (int)Position.Y - (int)Diameter / 2, (int)Diameter, (int)Diameter);

            if (displayOrbit)
                foreach (Vector2 t in orbitTrail)
                    spriteBatch.Draw(Texture, new Rectangle((int)t.X, (int)t.Y, 1, 1), Colour);

            spriteBatch.Draw(Texture, Rectangle, Colour);
        }
    }
}
