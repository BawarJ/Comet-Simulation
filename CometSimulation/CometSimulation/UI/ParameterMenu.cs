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
        TextBox txt_startX = new TextBox(200, 200);
        TextBox txt_startY = new TextBox(200, 300);
        TextBox txt_mass = new TextBox(200, 400);
        TextBox txt_diameter = new TextBox(200, 500);
        public float startX;
        public float startY;
        public float mass;
        public float diameter;
        public bool createObject;

        public ParameterMenu()
        {
            
        }

        public void Update()
        {
            ms = Mouse.GetState();
            rectMouse = new Rectangle(ms.X, ms.Y, 1, 1);
            rectContainer = new Rectangle(X, 0, Width, 768);
            
            //update menu items below
            if (txt_startX.kb.tekst != "" && txt_startX.inFocus)
            {
                txt_startX.textInput = txt_startX.kb.tekst;
                startX = float.Parse(txt_startX.kb.tekst);
            }
            if (txt_startY.kb.tekst != "" && txt_startY.inFocus)
            {
                txt_startY.textInput = txt_startY.kb.tekst;
                startY = float.Parse(txt_startY.kb.tekst);
            }
            if (txt_mass.kb.tekst != "" && txt_mass.inFocus)
            {
                txt_mass.textInput = txt_mass.kb.tekst;
                mass = float.Parse(txt_mass.kb.tekst);
            }
            if (txt_diameter.kb.tekst != "" && txt_diameter.inFocus)
            {
                txt_diameter.textInput = txt_diameter.kb.tekst;
                diameter = float.Parse(txt_diameter.kb.tekst);
            }

            txt_startX.Update();
            txt_startY.Update();
            txt_mass.Update();
            txt_diameter.Update();
            btnCreate.Update(0);

            if (btnCreate.isClicking)
            {
                showMenu = false;
                createObject = true;
            }
            else
                createObject = false;
        }

        public void Draw(SpriteBatch spriteBatch, SpriteFont font, Texture2D tex)
        {
            if (showMenu == true)
            {
                spriteBatch.Draw(tex, rectContainer, Color.White);

                //draw menu items below
                btnCreate.Draw(spriteBatch, tex, font, 0);
                spriteBatch.DrawString(font, "X:", new Vector2(20, 170), Color.Black);
                txt_startX.Draw(spriteBatch, tex, font);
                spriteBatch.DrawString(font, "Y:", new Vector2(20, 270), Color.Black);
                txt_startY.Draw(spriteBatch, tex, font);
                spriteBatch.DrawString(font, "Mass:", new Vector2(20, 370), Color.Black);
                txt_mass.Draw(spriteBatch, tex, font);
                spriteBatch.DrawString(font, "Diameter:", new Vector2(20, 470), Color.Black);
                txt_diameter.Draw(spriteBatch, tex, font);
            }
        }
    }
}
