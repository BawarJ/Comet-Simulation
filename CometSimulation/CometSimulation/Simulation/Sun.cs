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
    class Sun
    {
        #region Variables
        public Vector2 Position;
        public float Mass;
        public float Diameter;
        Color Colour;
        Color startingColour;
        MouseState ms;
        MouseState pms;
        Rectangle mousePos;
        public Boolean isClicking;
        public Boolean isHovering;
        int Frame = 0;
        #endregion

        public Sun(Vector2 pos, float dia, Color col)
        {
            Position = pos;
            Diameter = dia;
            Colour = col;
            startingColour = col;
            Mass = 20000000f; //Rounded constant
        }

        public void Update()
        {
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

            //If mouse is hovering over the sun highlight its colour
            if (isHovering)
                Colour.R = 100;
            else
                Colour.R = startingColour.R;

            pms = Mouse.GetState();
        }
        
        public void Draw(SpriteBatch spriteBatch, Texture2D Texture)
        {
            //Extracts frames from spritesheet texture to create an animation
            if (Frame >= 15)
                Frame = 0;
            else
                Frame++;

            //The location in the texture spritesheet to draw from
            Rectangle sourceRectangle = new Rectangle(0, Frame*100, 104, 100);
            //The location in the simulation to draw the sun to
            Rectangle destinationRectangle = new Rectangle((int)Position.X - (int)Diameter / 2, (int)Position.Y - (int)Diameter / 2, (int)Diameter, (int)Diameter);

            spriteBatch.Draw(Texture, destinationRectangle, sourceRectangle, Colour);
        }
    }
}
