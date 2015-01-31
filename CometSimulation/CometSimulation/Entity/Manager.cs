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
    class Manager
    {
        List<Comet> comets = new List<Comet>();
        List<Planet> planets = new List<Planet>();
        List<Star> stars = new List<Star>();

        Random rand = new Random();

        double G = 0.001;
        float theta;
        double dSq;
        Vector2 d;

        public void Initialize()
        {
            stars.Add(new Star(new Vector2(500, 200), 50000, 80, Color.White));
            //stars.Add(new Star(new Vector2(700, 200), 10000, 80, Color.Red));

            //for (int i = 0; i <= 100; i++)
            comets.Add(new Comet(new Vector2(rand.Next(900), 100 + rand.Next(300)), 3, 30, 0, Color.White));
            comets.Add(new Comet(new Vector2(rand.Next(900), 100 + rand.Next(300)), 2, 20, 0, Color.Gray));
            comets.Add(new Comet(new Vector2(rand.Next(900), 100 + rand.Next(300)), 2, 20, 0, Color.LightYellow));
            comets.Add(new Comet(new Vector2(rand.Next(900), 100 + rand.Next(300)), 1, 10, 0, Color.DarkGray));
            //comets.Add(new Comet(new Vector2(600, 200), 1, 30, Color.White));
        }

        public void Update()
        {
            
                foreach (Comet c in comets)
                {
                    c.Force = Vector2.Zero;
                    foreach (Star s in stars)
                    {
                    dSq = Vector2.DistanceSquared(s.Position, c.Position);
                    if (dSq != 0)
                    {
                        d.X = s.Position.X - c.Position.X;
                        d.Y = s.Position.Y - c.Position.Y;
                        c.F = G * c.m * s.m / dSq;
                        theta = (float)Math.Atan2(d.Y, d.X);
                        c.Force.X += (float)Math.Cos(theta) * (float)c.F;
                        c.Force.Y += (float)Math.Sin(theta) * (float)c.F;
                    }
                    c.Angle = theta;
                    Console.WriteLine(theta);
                    }
                }

            foreach (Comet c in comets)
                c.Update();
            foreach (Planet p in planets)
                p.Update();
            foreach (Star s in stars)
                s.Update();
        }

        public void Draw(SpriteBatch spriteBatch, Texture2D texComet, Texture2D texPlanet, Texture2D texStar)
        {
            foreach (Comet c in comets)
                c.Draw(spriteBatch, texComet);
            foreach (Planet p in planets)
                p.Draw(spriteBatch, texPlanet);
            foreach (Star s in stars)
                s.Draw(spriteBatch, texStar);
        }
    }
}
