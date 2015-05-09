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
        //Declare variables
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
            //Sun is created when the program is initialised
            sun.Add(new Sun(new Vector2(600, 300), 80, Color.White));
        }

        public void createComet(bool displayOrbit, float startX, float startY, float velX, float velY, float m, float density)
        {
            //Adds comet to list
            comets.Add(new Comet(displayOrbit, new Vector2(startX, startY), new Vector2(velX, velY), m, density));
        }
        public void createPlanet(bool displayOrbit, float startX, float startY, float velX, float velY, float m, float density)
        {
            //Adds planet to list
            planets.Add(new Planet(displayOrbit, new Vector2(startX, startY), new Vector2(velX, velY), m, density));
        }
        public void resetScreen()
        {
            //Clears contents of both lists
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
            //Simulation is slowed down based upon the timeDelay slider
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

            //If the simulation is not paused do the following
            if (!isPaused)
            {
                #region COMETS
                //Iterates through the loop for each comet in the list
                foreach (Comet c in comets)
                {
                    //Reset the comet Force value
                    c.Force = Vector2.Zero;

                    //Iterates through the loop for each sun in the list
                    //(There is only one sun but there is room for expansion)
                    foreach (Sun s in sun)
                    {
                        dSq = Vector2.DistanceSquared(s.Position, c.Position);
                        if (dSq != 0)
                        {
                            //Distance between sun and comet
                            d.X = s.Position.X - c.Position.X;
                            d.Y = s.Position.Y - c.Position.Y;
                            //Newton's Law of Universal Gravitation
                            c.F = (float)((G * c.Mass * s.Mass)/ dSq);
                            //Angle between sun and comet
                            theta = (float)Math.Atan2(d.X, d.Y);
                            //Force to be applied to the comet
                            c.Force.X += (float)Math.Sin(theta) * (float)c.F;
                            c.Force.Y += (float)Math.Cos(theta) * (float)c.F;

                            //Direction to draw gas tail (away from sun)
                            c.gasDirection = -d * 0.001f;
                            //Initial velocity to emit dust tail particles
                            c.particleVelocity = -d * 0.0001f + c.Velocity;
                        }
                    }
                    c.Update();
                }
                #endregion
                #region PLANETS
                //Iterates through the loop for each planet in the list
                foreach (Planet p in planets)
                {
                    //Reset the planet Force value
                    p.Force = Vector2.Zero;
                    //Iterates through the loop for each sun in the list
                    foreach (Sun s in sun)
                    {
                        dSq = Vector2.DistanceSquared(s.Position, p.Position);
                        if (dSq != 0)
                        {
                            //Distance between sun and planet
                            d.X = s.Position.X - p.Position.X;
                            d.Y = s.Position.Y - p.Position.Y;
                            //Newton's Law of Universal Gravitation
                            p.F = (float)(G * p.Mass * s.Mass / dSq);
                            //Angle between sun and planet
                            theta = (float)Math.Atan2(d.X, d.Y);
                            //Force to be applied to the planet
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
                //Allows the user to click and drag sun
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
            //Iterates through each list and draws each object
            foreach (Comet c in comets)
                c.Draw(spriteBatch, texComet);
            foreach (Planet p in planets)
                p.Draw(spriteBatch, texPlanet);
            foreach (Sun s in sun)
                s.Draw(spriteBatch, texStar);
        }
    }
}
