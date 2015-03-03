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
        Texture2D texSolid;
        Texture2D texMenu;
        string obj = "";
        Menu menu = new Menu();
        ParameterMenu popup = new ParameterMenu();

        public Main()
        {
            Content.RootDirectory = "Content";
            graphics = new GraphicsDeviceManager(this);
            graphics.PreferredBackBufferWidth = 1366;
            graphics.PreferredBackBufferHeight = 768;
            graphics.IsFullScreen = true;
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
            texSolid = Content.Load<Texture2D>("Pixel");
            texMenu = Content.Load<Texture2D>("Menu");
        }

        protected override void Update(GameTime gameTime)
        {
            if (Keyboard.GetState().IsKeyDown(Keys.Escape) == true)
                Exit();
            if (menu.btnComet.isClicking)
            {
                popup.showMenu = true;
                obj = "comet";
            }
            if (menu.btnPlanet.isClicking)
            {
                popup.showMenu = true;
                obj = "planet";
            }
            if (menu.btnReset.isClicking)
                manager.resetScreen();

            if (popup.createObject)
            {
                if (obj == "comet")
                    manager.createComet(popup.startX, popup.startY, popup.mass, popup.diameter);
                if (obj == "planet")
                    manager.createPlanet(popup.startX, popup.startY, popup.mass, popup.diameter);
            }

            manager.Update();
            menu.Update();
            popup.Update();
            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.Black);
            spriteBatch.Begin();

            manager.Draw(spriteBatch, texComet, texPlanet, texStar);
            menu.Draw(spriteBatch, font, texMenu);
            popup.Draw(spriteBatch, font, texMenu);
            spriteBatch.End();
            base.Draw(gameTime);
        }
    }
}
