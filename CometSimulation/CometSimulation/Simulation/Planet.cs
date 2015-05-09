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
        #region Variables
        public Vector2 Position;
        public Vector2 Acceleration;
        public Vector2 Velocity;
        public Vector2 Force;
        public float F;
        public float Mass;
        public float Density;
        public float Diameter;
        Color Colour;
        bool displayOrbit;
        List<Vector2> orbitTrail = new List<Vector2>();
        Random rand = new Random();
        #endregion

        public Planet(bool dispOrbit, Vector2 pos, Vector2 vel, float mass, float dens)
        {
            displayOrbit = dispOrbit;
            Position = pos;
            Velocity = vel;
            Mass = mass;
            Density = dens;
            //Diameter of the planet is calculated from the Mass and Density values
            Diameter = (mass / dens) * 10;
            //Generates a random colour used to draw the planet
            Colour = new Color((float)rand.NextDouble(),(float)rand.NextDouble(),(float)rand.NextDouble());
        }

        public void Update()
        {
            //Newton's Second Law of Motion: F = ma
            Acceleration.X = Force.X / Mass;
            Acceleration.Y = Force.Y / Mass;

            //If the displayOrbit checkbox has been checked, draw the trail
            if (displayOrbit)
                orbitTrail.Add(Position);
            
            //Calculates the velocity and position of the planet
            Velocity = Vector2.Add(Velocity, Acceleration);
            Position = Vector2.Add(Position, Velocity);
        }

        public void Draw(SpriteBatch spriteBatch, Texture2D Texture)
        {
            Rectangle Rectangle = new Rectangle((int)Position.X - (int)Diameter / 2, (int)Position.Y - (int)Diameter / 2, (int)Diameter, (int)Diameter);

            //If the displayOrbit checkbox has been checked, draw the trail
            if (displayOrbit)
                foreach (Vector2 t in orbitTrail)
                    spriteBatch.Draw(Texture, new Rectangle((int)t.X, (int)t.Y, 1, 1), Colour);

            spriteBatch.Draw(Texture, Rectangle, Colour);
        }
    }
}
