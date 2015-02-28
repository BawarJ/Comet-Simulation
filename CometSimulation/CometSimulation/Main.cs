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
        Menu menu = new Menu();

        public Main()
        {
            Content.RootDirectory = "Content";
            graphics = new GraphicsDeviceManager(this);
            graphics.PreferredBackBufferWidth = 1360;
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
                manager.createComet();
            if (menu.btnPlanet.isClicking)
                manager.createPlanet();
            if (menu.btnReset.isClicking)
                manager.resetScreen();
            manager.Update();
            menu.Update();
            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.Black);
            spriteBatch.Begin();

            manager.Draw(spriteBatch, texComet, texPlanet, texStar);
            menu.Draw(spriteBatch, font, texMenu);

            spriteBatch.End();
            base.Draw(gameTime);
        }
    }
}
