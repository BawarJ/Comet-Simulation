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
            stars.Add(new Star(new Vector2(600, 300), 500000, 80, Color.White));
            //stars.Add(new Star(new Vector2(700, 200), 10000, 80, Color.Red));

            //for (int i = 0; i <= 100; i++)
            comets.Add(new Comet (new Vector2(1200 + rand.Next(100), 250 + rand.Next(100)), 1, 10, 0, Color.White));

            planets.Add(new Planet(new Vector2(      rand.Next(100), 300 + rand.Next(100)), 1, 40, Color.LightBlue));
            planets.Add(new Planet(new Vector2(100 + rand.Next(100), 300 + rand.Next(100)), 1, 20, Color.Blue));
            planets.Add(new Planet(new Vector2(200 + rand.Next(100), 300 + rand.Next(100)), 1, 30, Color.Green));
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

                    theta = (float)Math.Atan2(d.X, d.Y);

                    if (c.Position.X > s.Position.X && c.Position.Y < s.Position.Y) //TOP RIGHT QUADRANT
                        c.Quadrant = 2;
                    if (c.Position.X < s.Position.X && c.Position.Y < s.Position.Y) //TOP LEFT QUADRANT
                        c.Quadrant = 1;
                    if (c.Position.X > s.Position.X && c.Position.Y > s.Position.Y) //BOTTOM RIGHT QUADRANT
                        c.Quadrant = 4;
                    if (c.Position.X < s.Position.X && c.Position.Y > s.Position.Y) //BOTTOM LEFT QUADRANT
                        c.Quadrant = 3;
                    }
                }

                foreach (Planet p in planets)
                {
                    p.Force = Vector2.Zero;
                    foreach (Star s in stars)
                    {
                        dSq = Vector2.DistanceSquared(s.Position, p.Position);
                        if (dSq != 0)
                        {
                            d.X = s.Position.X - p.Position.X;
                            d.Y = s.Position.Y - p.Position.Y;
                            p.F = G * p.m * s.m / dSq;
                            theta = (float)Math.Atan2(d.Y, d.X);
                            p.Force.X += (float)Math.Cos(theta) * (float)p.F;
                            p.Force.Y += (float)Math.Sin(theta) * (float)p.F;
                        }
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
