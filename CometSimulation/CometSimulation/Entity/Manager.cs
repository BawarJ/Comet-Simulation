﻿using System;
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

        float G = 0.001f;
        float mass = 5000000;
        float theta;
        double dSq;
        Vector2 d;

        public void Initialize()
        {
            stars.Add(new Star(new Vector2(600, 300), mass, 80, Color.White));
        }

        public void createComet(float startX, float startY, float velX, float velY, float diameter)
        {
            comets.Add(new Comet(new Vector2(startX, startY), new Vector2(velX, velY), diameter, Color.White));
        }
        public void createPlanet(float startX, float startY, float velX, float velY, float diameter)
        {
            planets.Add(new Planet(new Vector2(startX, startY), new Vector2(velX, velY), diameter));
        }
        public void resetScreen()
        {
            comets.RemoveRange(0, comets.Count);
            planets.RemoveRange(0, planets.Count);
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
                            c.F = (float)(G * c.m * s.m / dSq);
                            theta = (float)Math.Atan2(d.X, d.Y);
                            c.Force.X += (float)Math.Sin(theta) * (float)c.F;
                            c.Force.Y += (float)Math.Cos(theta) * (float)c.F;


                            c.particleVelocity = -d*0.001f + c.Velocity;
                        }
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
                            p.F = (float)(G * p.m * s.m / dSq);
                            theta = (float)Math.Atan2(d.X, d.Y);
                            p.Force.X += (float)Math.Sin(theta) * (float)p.F;
                            p.Force.Y += (float)Math.Cos(theta) * (float)p.F;
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
