using System.Runtime.InteropServices.ComTypes;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Tetris4Win8.Screens;

namespace Tetris4Win8
{
    /// <summary>
    /// This is the main type for your game
    /// </summary>
    public class Tetris : Microsoft.Xna.Framework.Game
    {
        public SpriteBatch SpriteBatch;

                
        #region Game State Region
        public GameStateManager stateManager;
        public PauseScreen pauseScreen;
        public MainScreen mainScreen;
        public MenuScreen menuScreen;
        public AboutScreen aboutScreen;
        public SettingScreen settingScreen;
        public GameOverScreen gameOverScreen;
        private Texture2D backgroundImage;
        #endregion
        #region Screen Field Region
        public int screenWidth =1366;
        public int screenHeight =768;
        public readonly Rectangle ScreenRectangle;
        #endregion
        public InputHundler inputeManager;
        GraphicsDeviceManager graphics;
        public Tetris()
        {
            // = GraphicsAdapter.DefaultAdapter.CurrentDisplayMode.Height;
           //screenWidth = GraphicsAdapter.DefaultAdapter.CurrentDisplayMode.Width;
            graphics = new GraphicsDeviceManager(this);            
            graphics.PreferredBackBufferWidth = screenWidth;
            graphics.PreferredBackBufferHeight = screenHeight;
            graphics.IsFullScreen = true;
            ScreenRectangle = new Rectangle(0, 0, screenWidth, screenHeight);
            Content.RootDirectory = "Content";
            inputeManager = new InputHundler(this);
            stateManager = new GameStateManager(this);
            pauseScreen = new PauseScreen(this, stateManager);
            mainScreen = new MainScreen(this, stateManager);
            menuScreen = new MenuScreen(this, stateManager);
            aboutScreen = new AboutScreen(this, stateManager);
            settingScreen = new SettingScreen(this, stateManager);
            gameOverScreen = new GameOverScreen(this, stateManager);
            Components.Add(inputeManager);
            Components.Add(stateManager);                      
            IsMouseVisible = true;

        }


        /// <summary>
        /// Allows the game to perform any initialization it needs to before starting to run.
        /// This is where it can query for any required services and load any non-graphic
        /// related content.  Calling base.Initialize will enumerate through any components
        /// and initialize them as well.
        /// </summary>
        protected override void Initialize()
        {
            // TODO: Add your initialization logic here
            SpriteBatch = new SpriteBatch(GraphicsDevice);
            stateManager.ChangeState(menuScreen);
            base.Initialize();
        }

        /// <summary>
        /// LoadContent will be called once per game and is the place to load
        /// all of your content.
        /// </summary>
        protected override void LoadContent()
        {
            // Create a new SpriteBatch, which can be used to draw textures.
            
            backgroundImage = Content.Load<Texture2D>(@"Bg\MenuScreen");         
            
           // TODO: use this.Content to load your game content here
        }

        /// <summary>
        /// UnloadContent will be called once per game and is the place to unload
        /// all content.
        /// </summary>
        protected override void UnloadContent()
        {         
            // TODO: Unload any non ContentManager content here
        }

        protected override void Update(GameTime gameTime)
        {
            // TODO: Add your update logic here

            base.Update(gameTime);
        }
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.LightGray);
            base.Draw(gameTime);
        }
    }
}
