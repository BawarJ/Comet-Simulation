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
        #region Variables

        //Textures and Graphics
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;
        SpriteFont font;
        Texture2D texComet;
        Texture2D texPlanet;
        Texture2D texSun;
        Texture2D texPixel;
        Texture2D texBox;
        Texture2D texTab;
        Texture2D texButton;
        List<Texture2D> texCheckbox = new List<Texture2D>();

            //Other
        Manager manager = new Manager();
        FileHandler fileManager = new FileHandler();
        int X = 0;
        int Inc = 10;
        MouseState ms;
        Rectangle rectMouse;
        Rectangle rectContainer;

            //Menu
        MenuState state;
        Button btnComet = new Button("Comet", 200);
        Button btnPlanet = new Button("Planet", 250);
        Button btnSave = new Button("Save", 350);
        Button btnLoad = new Button("Load", 400);
        Button btnInstructions = new Button("Instructions", 500);
        Button btnReset = new Button("Reset", 600);
        Button btnExit = new Button("Exit", 700);
        Button btnCreate = new Button("Create", 625);
        Button btnBack = new Button("Back", 700);
        Slider sldr_timeDelay = new Slider(0f, 10f, "Time Delay:", 125);
        TextBox txt_startX = new TextBox("X Position (0 - 1024):", 0, 1024, 50);
        TextBox txt_startY = new TextBox("Y Position (0 - 768):", 0, 768, 150);
        Slider sldr_velX = new Slider(-5f, 5f, "X Velocity:", 250);
        Slider sldr_velY = new Slider(-5f, 5f, "Y Velocity:", 330);
        Slider sldr_mass = new Slider(5f, 10f, "Mass:", 410);
        Slider sldr_density = new Slider(5f, 10f, "Density:", 490);
        Checkbox chkbxOrbitTrail = new Checkbox("Display Orbit Trail", 545);
        #endregion

        public Main()
        {
            //Set up the window
            Content.RootDirectory = "Content";
            graphics = new GraphicsDeviceManager(this);
            graphics.PreferredBackBufferWidth = 1024;
            graphics.PreferredBackBufferHeight = 768;
            graphics.IsFullScreen = false;
            IsMouseVisible = true;

            state = MenuState.Main; //Sets the initial MenuState to the Main Menu
        }

        public enum MenuState
        {
            //Creates 4 Menu States
            Main,
            Comet,
            Planet,
            Instructions
        }

        protected override void Initialize()
        {
            //Initialises the simulation
            manager.Initialize();
            base.Initialize();
        }

        protected override void LoadContent()
        {
            spriteBatch = new SpriteBatch(GraphicsDevice);
            #region Load Textures 
                //Loads required textures
            font = Content.Load<SpriteFont>("Font"); 
            texComet = Content.Load<Texture2D>("Comet");
            texPlanet = Content.Load<Texture2D>("Planet");
            texSun = Content.Load<Texture2D>("Sun");
            texPixel = Content.Load<Texture2D>("Pixel");
            texBox = Content.Load<Texture2D>("Box");
            texCheckbox.Add(Content.Load<Texture2D>("Checkbox_Unticked"));
            texCheckbox.Add(Content.Load<Texture2D>("Checkbox_Ticked"));
            texTab = Content.Load<Texture2D>("Tab");
            texButton = Content.Load<Texture2D>("Button");
            #endregion
        }

        protected override void Update(GameTime gameTime)
        {
            #region Menu Slide Reveal

            ms = Mouse.GetState();
            rectMouse = new Rectangle(ms.X, ms.Y, 1, 1);
            rectContainer = new Rectangle(X, 0, 200, 768);

            //When the mouse hovers left the menu is revealed
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
                #region MAIN MENU
                case MenuState.Main:
                    btnComet.Update(X);
                    btnPlanet.Update(X);
                    sldr_timeDelay.Update(gameTime, X);
                    btnSave.Update(X);
                    btnLoad.Update(X);
                    btnInstructions.Update(X);
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
                    if (btnInstructions.Clicked)
                        state = MenuState.Instructions;
                    if (btnReset.Clicked)
                        manager.resetScreen();
                    if (btnExit.Clicked)
                        Exit();
                    break;
                #endregion

                #region COMET MENU
                case MenuState.Comet:
                    btnCreate.Update(X);
                    btnBack.Update(X);

                    if (btnCreate.Clicked)
                    {
                        manager.createComet(chkbxOrbitTrail.isChecked, txt_startX.Value, txt_startY.Value, sldr_velX.Value, sldr_velY.Value, sldr_mass.Value, sldr_density.Value);
                        state = MenuState.Main;
                    }
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
                #endregion

                #region PLANET MENU
                case MenuState.Planet:
                    btnCreate.Update(X);
                    btnBack.Update(X);

                    if (btnCreate.Clicked)
                    {
                        manager.createPlanet(chkbxOrbitTrail.isChecked, txt_startX.Value, txt_startY.Value, sldr_velX.Value, sldr_velY.Value, sldr_mass.Value, sldr_density.Value);
                        state = MenuState.Main;
                    }
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
                #endregion

                #region INSTRUCTIONS PAGE
                case MenuState.Instructions:
                    btnBack.Update(X);

                    if (btnBack.Clicked)
                        state = MenuState.Main;
                    break;
                #endregion
            }

            manager.Update();
            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.Black);
            spriteBatch.Begin();

            //Draws the Simulation
            manager.Draw(spriteBatch, texComet, texPlanet, texSun);

            #region Draw Menu
            spriteBatch.Draw(texPixel, rectContainer, Color.White);
            spriteBatch.Draw(texTab, new Rectangle(rectContainer.X + rectContainer.Width, rectContainer.Height/4, 15, 80), Color.White);
            spriteBatch.DrawString(font, "Menu", new Vector2(rectContainer.X + rectContainer.Width + 18, rectContainer.Height / 4 +18), Color.Black, MathHelper.ToRadians(90), Vector2.Zero, 0.9f, SpriteEffects.None, 0);

            switch (state)
            {
                #region MAIN MENU
                case MenuState.Main:
                    spriteBatch.DrawString(font, "Comet Simulation", new Vector2(X + 20, 10), Color.Black);
                    btnComet.Draw(spriteBatch, texButton, font, X);
                    btnPlanet.Draw(spriteBatch, texButton, font, X);
                    sldr_timeDelay.Draw(spriteBatch, texBox, font, X);
                    btnSave.Draw(spriteBatch, texButton, font, X);
                    btnLoad.Draw(spriteBatch, texButton, font, X);
                    btnInstructions.Draw(spriteBatch, texButton, font, X);
                    btnReset.Draw(spriteBatch, texButton, font, X);
                    btnExit.Draw(spriteBatch, texButton, font, X);
                    break;
                #endregion

                #region COMET MENU
                case MenuState.Comet:
                    btnCreate.Draw(spriteBatch, texButton, font, X);
                    btnBack.Draw(spriteBatch, texButton, font, X);
                    txt_startX.Draw(spriteBatch, texBox, font, X);
                    txt_startY.Draw(spriteBatch, texBox, font, X);
                    sldr_velX.Draw(spriteBatch, texBox, font, X);
                    sldr_velY.Draw(spriteBatch, texBox, font, X);
                    sldr_mass.Draw(spriteBatch, texBox, font, X);
                    sldr_density.Draw(spriteBatch, texBox, font, X);
                    chkbxOrbitTrail.Draw(spriteBatch, texCheckbox, font, X);
                    break;
                #endregion

                #region PLANET MENU
                case MenuState.Planet:
                    btnCreate.Draw(spriteBatch, texButton, font, X);
                    btnBack.Draw(spriteBatch, texButton, font, X);
                    txt_startX.Draw(spriteBatch, texBox, font, X);
                    txt_startY.Draw(spriteBatch, texBox, font, X);
                    sldr_velX.Draw(spriteBatch, texBox, font, X);
                    sldr_velY.Draw(spriteBatch, texBox, font, X);
                    sldr_mass.Draw(spriteBatch, texBox, font, X);
                    sldr_density.Draw(spriteBatch, texBox, font, X);
                    chkbxOrbitTrail.Draw(spriteBatch, texCheckbox, font, X);
                    break;
                #endregion

                #region INSTRUCTIONS PAGE
                case MenuState.Instructions:
                    spriteBatch.DrawString(font, "Instructions Page", new Vector2(X + 20, 10), Color.Black);
                    spriteBatch.DrawString(font, "In order to use this \nsimulation, follow the \nfollowing steps.", new Vector2(X + 10, 100), Color.Black);
                    spriteBatch.DrawString(font, 
                        "1. Choose desired time \ndelay using slider. \n2. Click Comet/Planet \nbutton. \n3. Customise variables. \n4. Click Create button. \n5. Repeat as required. \n\nTo save current state, \nclick Save button. \nTo load state, \nclick Load button. \nTo clear the simulation, \nclick Reset button. \nTo exit simulation, \nclick Exit button. \n\nFor further assistance, \nseek User Manuals."
                        , new Vector2(X + 10, 200), Color.Black);
                    btnBack.Draw(spriteBatch, texButton, font, X);
                    break;
                #endregion
            }
            #endregion

            spriteBatch.End();
            base.Draw(gameTime);
        }
    }
}
