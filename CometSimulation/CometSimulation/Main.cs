using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;

namespace CometSimulation
{
    public class Main : Microsoft.Xna.Framework.Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;
        SpriteFont font;
        Manager manager = new Manager();
        Texture2D texComet;
        Texture2D texPlanet;
        Texture2D texStar;
        Texture2D texPixel;
        Texture2D texBox;

        MenuState state;
        int X = 0;
        int Inc = 10;
        MouseState ms;
        Rectangle rectMouse;
        Rectangle rectContainer;
        Button btnComet = new Button("Comet", 200, 200);
        Button btnPlanet = new Button("Planet", 200, 400);
        Button btnReset = new Button("Reset", 200, 650);
        Button btnExit = new Button("Exit", 200, 700);
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

        public Main()
        {
            Content.RootDirectory = "Content";
            graphics = new GraphicsDeviceManager(this);
            graphics.PreferredBackBufferWidth = 1360;
            graphics.PreferredBackBufferHeight = 700;
            //graphics.IsFullScreen = true;
            IsMouseVisible = true;

            state = MenuState.Main;
        }

        public enum MenuState
        {
            Main,
            Comet,
            Planet
        }

        protected override void Initialize()
        {
            manager.Initialize();
            base.Initialize();
        }

        protected override void LoadContent()
        {
            spriteBatch = new SpriteBatch(GraphicsDevice);
            font = Content.Load<SpriteFont>("Font");
            texComet = Content.Load<Texture2D>("Comet");
            texPlanet = Content.Load<Texture2D>("Planet");
            texStar = Content.Load<Texture2D>("Star");
            texPixel = Content.Load<Texture2D>("Pixel");
            texBox = Content.Load<Texture2D>("Box");
        }

        protected override void Update(GameTime gameTime)
        {
            #region Mouse Stuff
            ms = Mouse.GetState();
            rectMouse = new Rectangle(ms.X, ms.Y, 1, 1);
            rectContainer = new Rectangle(X, 0, 200, 768);

            if (ms.X < 10 || rectMouse.Intersects(rectContainer))
            {
                if (X < 0)
                    X += Inc;
            }
            else
            {
                if (X > 0 - 200)
                    X -= Inc;
                if (X <= 0 - 200)
                {
                    X = -200;
                }
            }
            #endregion

            switch (state)
            {
                case MenuState.Main:
                    btnComet.Update(X);
                    btnPlanet.Update(X);
                    btnReset.Update(X);
                    btnExit.Update(X);
                    if (btnComet.Clicked)
                        state = MenuState.Comet;
                    if (btnPlanet.Clicked)
                        state = MenuState.Planet;
                    if (btnReset.Clicked)
                        manager.resetScreen();
                    if (btnExit.Clicked)
                        Exit();
                    break;

                case MenuState.Comet:
                    btnCreate.Update(X);
                    btnBack.Update(X);

                    if (btnCreate.Clicked)
                        manager.createComet(startX, startY, velX, velY, diameter);

                    if (btnBack.Clicked)
                        state = MenuState.Main;

                    txt_startX.Update(gameTime, X);
                    txt_startY.Update(gameTime, X);
                    txt_velX.Update(gameTime, X);
                    txt_velY.Update(gameTime, X);
                    txt_diameter.Update(gameTime, X);

                    #region Textboxes
                    if (txt_startX.kb.text != "" && txt_startX.inFocus)
                    {
                        txt_startX.textInput = txt_startX.kb.text;
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
                    #endregion

                    break;

                case MenuState.Planet:
                    //do stuff
                    break;
            }

            manager.Update();
            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.Black);
            spriteBatch.Begin();

            manager.Draw(spriteBatch, texComet, texPlanet, texStar);

            #region menu
            spriteBatch.Draw(texPixel, rectContainer, Color.White);

            switch (state)
            {
                case MenuState.Main:
                    spriteBatch.DrawString(font, "Comet Simulation", new Vector2(X + 20, 10), Color.Black);
                    btnComet.Draw(spriteBatch, texPixel, font, X);
                    btnPlanet.Draw(spriteBatch, texPixel, font, X);
                    btnReset.Draw(spriteBatch, texPixel, font, X);
                    btnExit.Draw(spriteBatch, texPixel, font, X);
                    break;

                case MenuState.Comet:
                    btnCreate.Draw(spriteBatch, texPixel, font, X);
                    btnBack.Draw(spriteBatch, texPixel, font, X);
                    spriteBatch.DrawString(font, "X Position:", new Vector2(X + 20, 170), Color.Black);
                    txt_startX.Draw(spriteBatch, texBox, font, X);
                    spriteBatch.DrawString(font, "Y Position:", new Vector2(X + 20, 270), Color.Black);
                    txt_startY.Draw(spriteBatch, texBox, font, X);
                    spriteBatch.DrawString(font, "X Velocity:", new Vector2(X + 20, 370), Color.Black);
                    txt_velX.Draw(spriteBatch, texBox, font, X);
                    spriteBatch.DrawString(font, "Y Velocity:", new Vector2(X + 20, 470), Color.Black);
                    txt_velY.Draw(spriteBatch, texBox, font, X);
                    spriteBatch.DrawString(font, "Diameter:", new Vector2(X + 20, 570), Color.Black);
                    txt_diameter.Draw(spriteBatch, texBox, font, X);
                    break;

                case MenuState.Planet:
                    //bla
                    break;
            }
            #endregion

            spriteBatch.End();
            base.Draw(gameTime);
        }
    }
}
