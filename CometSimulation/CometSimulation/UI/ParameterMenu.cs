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
    class ParameterMenu
    {
        public Boolean showMenu;
        int X = 0;
        int Width = 200;
        MouseState ms;
        Rectangle rectMouse;
        Rectangle rectContainer;
        Button btnCreate = new Button("Create", 200, 650);
        Button btnBack = new Button("Back", 200, 700); 
        TextBox txt_startX = new TextBox(200, 200);
        TextBox txt_startY = new TextBox(200, 300);
        TextBox txt_velX = new TextBox(200, 400);
        TextBox txt_velY = new TextBox(200, 500);
        TextBox txt_diameter = new TextBox(200, 600);
        public float startX;
        public float startY;
        public float velX;
        public float velY;
        public float diameter;
        public bool createObject;

        public ParameterMenu()
        {
            
        }

        public void Update(GameTime gameTime)
        {
            ms = Mouse.GetState();
            rectMouse = new Rectangle(ms.X, ms.Y, 1, 1);
            rectContainer = new Rectangle(X, 0, Width, 768);
            
            //update menu items below
            if (txt_startX.kb.text != "" && txt_startX.inFocus)
            {
                txt_startX.textInput =  txt_startX.kb.text;
                startX = txt_startX.kb.minus * float.Parse(txt_startX.kb.text);
            }
            if (txt_startY.kb.text != "" && txt_startY.inFocus)
            {
                txt_startY.textInput = txt_startY.kb.text;
                startY = txt_startY.kb.minus * float.Parse(txt_startY.kb.text);
            }
            if (txt_velX.kb.text != "" && txt_velX.inFocus)
            {
                txt_velX.textInput = txt_velX.kb.text;
                velX = txt_velX.kb.minus * float.Parse(txt_velX.kb.text);
            }
            if (txt_velY.kb.text != "" && txt_velY.inFocus)
            {
                txt_velY.textInput = txt_velY.kb.text;
                velY = txt_velY.kb.minus * float.Parse(txt_velY.kb.text);
            }
            if (txt_diameter.kb.text != "" && txt_diameter.inFocus)
            {
                txt_diameter.textInput = txt_diameter.kb.text;
                diameter = txt_diameter.kb.minus * float.Parse(txt_diameter.kb.text);
            }

            txt_startX.Update(gameTime);
            txt_startY.Update(gameTime);
            txt_velX.Update(gameTime);
            txt_velY.Update(gameTime);
            txt_diameter.Update(gameTime);
            btnCreate.Update(0);
            btnBack.Update(0);
            
            if (btnCreate.Clicked)
                createObject = true;
            else
                createObject = false;

            if (btnBack.Clicked)
                showMenu = false;
        }

        public void Draw(SpriteBatch spriteBatch, SpriteFont font, Texture2D texPixel, Texture2D texBox)
        {
            if (showMenu == true)
            {
                spriteBatch.Draw(texPixel, rectContainer, Color.White);

                //draw menu items below
                btnCreate.Draw(spriteBatch, texPixel, font, 0);
                btnBack.Draw(spriteBatch, texPixel, font, 0);
                spriteBatch.DrawString(font, "X Position:", new Vector2(20, 170), Color.Black);
                txt_startX.Draw(spriteBatch, texBox, font);
                spriteBatch.DrawString(font, "Y Position:", new Vector2(20, 270), Color.Black);
                txt_startY.Draw(spriteBatch, texBox, font);
                spriteBatch.DrawString(font, "X Velocity:", new Vector2(20, 370), Color.Black);
                txt_velX.Draw(spriteBatch, texBox, font);
                spriteBatch.DrawString(font, "Y Velocity:", new Vector2(20, 470), Color.Black);
                txt_velY.Draw(spriteBatch, texBox, font);
                spriteBatch.DrawString(font, "Diameter:", new Vector2(20, 570), Color.Black);
                txt_diameter.Draw(spriteBatch, texBox, font);
            }
        }
    }
}
