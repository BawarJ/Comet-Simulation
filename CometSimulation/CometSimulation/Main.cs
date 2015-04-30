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
        Texture2D texSun;
        List<Texture2D> texCheckbox = new List<Texture2D>();
        Texture2D texPixel;
        Texture2D texBox;

        MenuState state;
        FileManager fileManager = new FileManager();
        int X = 0;
        int Inc = 10;
        MouseState ms;
        Rectangle rectMouse;
        Rectangle rectContainer;
        Button btnComet = new Button("Comet", 150);
        Button btnPlanet = new Button("Planet", 225);
        Button btnPause = new Button("Play/Pause", 400);
        Button btnSave = new Button("Save", 475);
        Button btnLoad = new Button("Load", 550);
        Button btnReset = new Button("Reset", 625);
        Button btnExit = new Button("Exit", 700);
        Button btnCreate = new Button("Create", 625);
        Button btnBack = new Button("Back", 700);
        TextBox txt_startX = new TextBox("X Position (0 - 1366):", 0, 1366, 50);
        TextBox txt_startY = new TextBox("Y Position (0 - 768):", 0, 768, 150);
        Slider sldr_timeDelay = new Slider(0f, 10f, "Time Delay:", 350);
        Slider sldr_velX = new Slider(-5f, 5f, "X Velocity:", 250);
        Slider sldr_velY = new Slider(-5f, 5f, "Y Velocity:", 330);
        Slider sldr_mass = new Slider(5f, 10f, "Mass:", 410);
        Slider sldr_density = new Slider(5f, 10f, "Density:", 490);
        Checkbox chkbxOrbitTrail = new Checkbox("Display Orbit Tail", 545);

        public Main()
        {
            Content.RootDirectory = "Content";
            graphics = new GraphicsDeviceManager(this);
            graphics.PreferredBackBufferWidth = 1024;
            graphics.PreferredBackBufferHeight = 768;
            graphics.IsFullScreen = false;
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
            texSun = Content.Load<Texture2D>("Sun");
            texPixel = Content.Load<Texture2D>("Pixel");
            texBox = Content.Load<Texture2D>("Box");
            texCheckbox.Add(Content.Load<Texture2D>("Checkbox_Unticked"));
            texCheckbox.Add(Content.Load<Texture2D>("Checkbox_Ticked"));


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
                    sldr_timeDelay.Update(gameTime, X);
                    btnSave.Update(X);
                    btnLoad.Update(X);
                    btnPause.Update(X);
                    btnReset.Update(X);
                    btnExit.Update(X);
                    if (sldr_timeDelay.isClicking)
                    {
                        manager.timeDelay = sldr_timeDelay.Value;
                        manager.tempTimeDelay = manager.timeDelay;
                    }
                    if (btnComet.Clicked)
                        state = MenuState.Comet;
                    if (btnPlanet.Clicked)
                        state = MenuState.Planet;
                    if (btnSave.Clicked)
                        fileManager.Save(manager);
                    if (btnLoad.Clicked)
                    {
                        manager.resetScreen();
                        fileManager.Load(manager);
                    }
                    if (btnPause.Clicked)
                    {
                        if (manager.isPaused)
                            manager.isPaused = false;
                        else if (!manager.isPaused)
                            manager.isPaused = true;

                    }
                    if (btnReset.Clicked)
                        manager.resetScreen();
                    if (btnExit.Clicked)
                        Exit();
                    break;

                case MenuState.Comet:
                    btnCreate.Update(X);
                    btnBack.Update(X);

                    if (btnCreate.Clicked)
                        manager.createComet(chkbxOrbitTrail.isChecked, txt_startX.Value, txt_startY.Value, sldr_velX.Value, sldr_velY.Value, sldr_mass.Value, sldr_density.Value);

                    if (btnBack.Clicked)
                        state = MenuState.Main;

                    txt_startX.Update(gameTime, X);
                    txt_startY.Update(gameTime, X);
                    sldr_velX.Update(gameTime, X);
                    sldr_velY.Update(gameTime, X);
                    sldr_mass.Update(gameTime, X);
                    sldr_density.Update(gameTime, X);
                    chkbxOrbitTrail.Update(X);
                    break;

                case MenuState.Planet:
                    btnCreate.Update(X);
                    btnBack.Update(X);

                    if (btnCreate.Clicked)
                        manager.createPlanet(chkbxOrbitTrail.isChecked, txt_startX.Value, txt_startY.Value, sldr_velX.Value, sldr_velY.Value, sldr_mass.Value, sldr_density.Value);

                    if (btnBack.Clicked)
                        state = MenuState.Main;

                    txt_startX.Update(gameTime, X);
                    txt_startY.Update(gameTime, X);
                    sldr_velX.Update(gameTime, X);
                    sldr_velY.Update(gameTime, X);
                    sldr_mass.Update(gameTime, X);
                    sldr_density.Update(gameTime, X);
                    chkbxOrbitTrail.Update(X);
                    break;
            }

            manager.Update();
            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.Black);
            spriteBatch.Begin();

            manager.Draw(spriteBatch, texComet, texPlanet, texSun);

            #region menu
            spriteBatch.Draw(texPixel, rectContainer, Color.White);

            switch (state)
            {
                case MenuState.Main:
                    spriteBatch.DrawString(font, "Comet Simulation", new Vector2(X + 20, 10), Color.Black);
                    btnComet.Draw(spriteBatch, texPixel, font, X);
                    btnPlanet.Draw(spriteBatch, texPixel, font, X);
                    sldr_timeDelay.Draw(spriteBatch, texBox, font, X);
                    btnSave.Draw(spriteBatch, texPixel, font, X);
                    btnLoad.Draw(spriteBatch, texPixel, font, X);
                    btnPause.Draw(spriteBatch, texPixel, font, X);
                    btnReset.Draw(spriteBatch, texPixel, font, X);
                    btnExit.Draw(spriteBatch, texPixel, font, X);
                    break;

                case MenuState.Comet:
                    btnCreate.Draw(spriteBatch, texPixel, font, X);
                    btnBack.Draw(spriteBatch, texPixel, font, X);
                    txt_startX.Draw(spriteBatch, texBox, font, X);
                    txt_startY.Draw(spriteBatch, texBox, font, X);
                    sldr_velX.Draw(spriteBatch, texBox, font, X);
                    sldr_velY.Draw(spriteBatch, texBox, font, X);
                    sldr_mass.Draw(spriteBatch, texBox, font, X);
                    sldr_density.Draw(spriteBatch, texBox, font, X);
                    chkbxOrbitTrail.Draw(spriteBatch, texCheckbox, font, X);
                    break;

                case MenuState.Planet:
                    btnCreate.Draw(spriteBatch, texPixel, font, X);
                    btnBack.Draw(spriteBatch, texPixel, font, X);
                    txt_startX.Draw(spriteBatch, texBox, font, X);
                    txt_startY.Draw(spriteBatch, texBox, font, X);
                    sldr_velX.Draw(spriteBatch, texBox, font, X);
                    sldr_velY.Draw(spriteBatch, texBox, font, X);
                    sldr_mass.Draw(spriteBatch, texBox, font, X);
                    sldr_density.Draw(spriteBatch, texBox, font, X);
                    chkbxOrbitTrail.Draw(spriteBatch, texCheckbox, font, X);
                    break;
            }
            #endregion

            spriteBatch.End();
            base.Draw(gameTime);
        }
    }
}
