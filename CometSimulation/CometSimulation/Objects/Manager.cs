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
        public List<Comet> comets = new List<Comet>();
        public List<Planet> planets = new List<Planet>();
        public List<Star> stars = new List<Star>();
        public Boolean isPaused = false;

        Random rand = new Random();

        float G = (float)Math.Exp(-11)*7;
        float theta;
        double dSq;
        Vector2 d;
        public float timeDelay;
        public float tempTimeDelay;
        MouseState ms;
        Vector2 mousePos;

        public void Initialize()
        {
            stars.Add(new Star(new Vector2(600, 300), 80, Color.White));
        }

        public void createComet(bool displayOrbit, float startX, float startY, float velX, float velY, float m, float density)
        {
            comets.Add(new Comet(displayOrbit, new Vector2(startX, startY), new Vector2(velX, velY), m, density));
        }
        public void createPlanet(bool displayOrbit, float startX, float startY, float velX, float velY, float m, float density)
        {
            planets.Add(new Planet(displayOrbit, new Vector2(startX, startY), new Vector2(velX, velY), m, density));
        }
        public void resetScreen()
        {
            comets.RemoveRange(0, comets.Count);
            planets.RemoveRange(0, planets.Count);
        }

        public void Update()
        {
            ms = Mouse.GetState();
            mousePos = new Vector2(ms.X, ms.Y);
            
            if (tempTimeDelay > 0)
            {
                isPaused = true;
                tempTimeDelay--;
            }
            else if (tempTimeDelay == 0)
            {
                isPaused = false;
                tempTimeDelay = timeDelay;
            }

            if (!isPaused)
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

                            c.gasDirection = -d * 0.001f;
                            c.particleVelocity = -d * 0.0001f + c.Velocity;
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
            }
            foreach (Star s in stars)
            {
                if (s.isClicking)
                {
                    s.Position = mousePos;
                }
                s.Update();
            }
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
