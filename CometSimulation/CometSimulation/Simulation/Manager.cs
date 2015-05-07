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
        #region Variables
        //declare variables
        public List<Comet> comets = new List<Comet>();
        public List<Planet> planets = new List<Planet>();
        public List<Sun> sun = new List<Sun>();
        public Boolean isPaused = false;
        Random rand = new Random();
        public float timeDelay;
        public float tempTimeDelay;
        MouseState ms;
        Vector2 mousePos;
        float G = (float)Math.Exp(-11)*7;
        float theta;
        double dSq;
        Vector2 d;
        #endregion

        public void Initialize()
        {
            //sun is created when the program is initialised
            sun.Add(new Sun(new Vector2(600, 300), 80, Color.White));
        }

        public void createComet(bool displayOrbit, float startX, float startY, float velX, float velY, float m, float density)
        {
            //adds comet to list
            comets.Add(new Comet(displayOrbit, new Vector2(startX, startY), new Vector2(velX, velY), m, density));
        }
        public void createPlanet(bool displayOrbit, float startX, float startY, float velX, float velY, float m, float density)
        {
            //adds planet to list
            planets.Add(new Planet(displayOrbit, new Vector2(startX, startY), new Vector2(velX, velY), m, density));
        }
        public void resetScreen()
        {
            //clears contents of both lists
            comets.RemoveRange(0, comets.Count);
            planets.RemoveRange(0, planets.Count);
        }

        public void Update()
        {
            #region Mouse
            ms = Mouse.GetState();
            mousePos = new Vector2(ms.X, ms.Y);
            #endregion

            #region Time Delay
            //simulation is slowed down based upon the timeDelay slider
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
            #endregion

            if (!isPaused)
            {
                #region COMETS
                foreach (Comet c in comets)
                {
                    c.Force = Vector2.Zero;
                    foreach (Sun s in sun)
                    {
                        dSq = Vector2.DistanceSquared(s.Position, c.Position);
                        if (dSq != 0)
                        {
                            d.X = s.Position.X - c.Position.X;
                            d.Y = s.Position.Y - c.Position.Y;
                            c.F = (float)((G * c.m * s.m)/ dSq); //Formula
                            theta = (float)Math.Atan2(d.X, d.Y);
                            c.Force.X += (float)Math.Sin(theta) * (float)c.F;
                            c.Force.Y += (float)Math.Cos(theta) * (float)c.F;

                            c.gasDirection = -d * 0.001f;
                            c.particleVelocity = -d * 0.0001f + c.Velocity;
                        }
                    }
                    c.Update();
                }
                #endregion
                #region PLANETS
                foreach (Planet p in planets)
                {
                    p.Force = Vector2.Zero;
                    foreach (Sun s in sun)
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
                    p.Update();
                }
                #endregion
            }

            #region SUN
            foreach (Sun s in sun)
            {
                //allows the user to click and drag sun
                if (s.isClicking)
                {
                    s.Position = mousePos;
                }
                s.Update();
            }
            #endregion
        }

        public void Draw(SpriteBatch spriteBatch, Texture2D texComet, Texture2D texPlanet, Texture2D texStar)
        {
            foreach (Comet c in comets)
                c.Draw(spriteBatch, texComet);
            foreach (Planet p in planets)
                p.Draw(spriteBatch, texPlanet);
            foreach (Sun s in sun)
                s.Draw(spriteBatch, texStar);
        }
    }
}
