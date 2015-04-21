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
        public Vector2 Velocity;
        public double m;
        public double Diameter;
        Color Colour;
        Color startingColour;
        MouseState ms;
        MouseState pms;
        Rectangle mousePos;
        public Boolean isClicking;
        public Boolean isHovering;

        public Star(Vector2 pos, double mass, double dia, Color col)
        {
            Position = pos;
            Diameter = dia;
            Colour = col;
            startingColour = col;
            m = mass;
            Velocity.X = 0;
        }

        public void Update()
        {
            Position = Vector2.Add(Position, Velocity);

            //mouse code
            ms = Mouse.GetState();
            mousePos = new Rectangle(ms.X, ms.Y, 1, 1);

            if (mousePos.Intersects(new Rectangle((int)(Position.X-Diameter/2), (int)(Position.Y-Diameter/2), (int)Diameter, (int)Diameter)))
                isHovering = true;
            else
                isHovering = false;

            if (isHovering && pms.LeftButton == ButtonState.Pressed && ms.LeftButton == ButtonState.Pressed)
                isClicking = true;
            else
                isClicking = false;

            if (isHovering)
                Colour.R = 100;
            else
                Colour.R = startingColour.R;


            pms = Mouse.GetState();
        }
        
        public void Draw(SpriteBatch spriteBatch, Texture2D Texture)
        {
            Rectangle Rectangle = new Rectangle((int)Position.X - (int)Diameter/2, (int)Position.Y - (int)Diameter/2, (int)Diameter, (int)Diameter);
            spriteBatch.Draw(Texture, Rectangle, Colour);
        }
    }
}
