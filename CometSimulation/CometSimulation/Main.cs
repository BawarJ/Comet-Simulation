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
        string obj = "";
        Menu menu = new Menu();
        ParameterMenu popup = new ParameterMenu();

        public Main()
        {
            Content.RootDirectory = "Content";
            graphics = new GraphicsDeviceManager(this);
            graphics.PreferredBackBufferWidth = 1360;
            graphics.PreferredBackBufferHeight = 700;
            //graphics.IsFullScreen = true;
            IsMouseVisible = true;
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
            if (menu.btnComet.Clicked && !popup.showMenu)
            {
                popup.showMenu = true;
                obj = "comet";
            }
            if (menu.btnPlanet.Clicked && !popup.showMenu)
            {
                popup.showMenu = true;
                obj = "planet";
            }
            if (menu.btnReset.Clicked && !popup.showMenu)
            {
                manager.resetScreen();
                menu.showMenu = false;
            }
            if (menu.btnExit.Clicked && !popup.showMenu)
                Exit();

            if (popup.createObject)
            {
                if (obj == "comet")
                    manager.createComet(popup.startX, popup.startY, popup.velX, popup.velY, popup.diameter);
                if (obj == "planet")
                    manager.createPlanet(popup.startX, popup.startY, popup.velX, popup.velY, popup.diameter);
            }

            manager.Update();
            menu.Update();
            popup.Update(gameTime);
            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.Black);
            spriteBatch.Begin();

            manager.Draw(spriteBatch, texComet, texPlanet, texStar);
            menu.Draw(spriteBatch, font, texPixel, texBox);
            popup.Draw(spriteBatch, font, texPixel, texBox);
            spriteBatch.End();
            base.Draw(gameTime);
        }
    }
}
