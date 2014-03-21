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
using Tetris4Win8;

namespace Tetris4Win8.Screens
{
    public class MenuScreen : GameStateBase
    {
        #region consts
        private const int TotalMenuItems = 4;
        #endregion
        #region Fields
        //Used To Debug :p
        SpriteFont _font;
        Texture2D _texImageMenuItem;
        List<MenuItem> _menu;
        int _index = 0;
        int _currentIndex = -1;
        #endregion
        #region Methods
        public MenuScreen(Game game, GameStateManager manager)
            : base(game, manager)
        {
            // TODO: Construct any child components here
            _menu = new List<MenuItem>();
        }
        protected override void LoadContent()
        {
            ContentManager Content = GameRef.Content;
            _texImageMenuItem = Content.Load<Texture2D>(@"Menu\MenuItem0");
            _font = Content.Load<SpriteFont>(@"Font\BaseFont");
            int X = GameRef.GraphicsDevice.Viewport.Width / 2;
            int Y = GameRef.GraphicsDevice.Viewport.Height / 2 - 170;   //170 : Each Menu Item Is 70Px Height + The Offset Of 20Px
            for (int i = 0; i < TotalMenuItems; i++)
            {
                MenuItem item = new MenuItem(GameRef.SpriteBatch,
                    new Vector2(
                        X, Y + i * (_texImageMenuItem.Height + 20)), Content.Load<Texture2D>(@"Menu\MenuItem" + i));
                item.Index = _index++;
                _menu.Add(item);
            }
            base.LoadContent();
        }
        public override void Update(GameTime gameTime)
        {
            _currentIndex = -1;
            if (InputHundler.KeyPressed(Keys.Enter))
            {
                StateManager.PushState(GameRef.mainScreen);
            }
            Vector2 tapPosition = new Vector2();
            tapPosition.X = InputHundler.MouseState.X;
            tapPosition.Y = InputHundler.MouseState.Y;
            foreach (MenuItem item in _menu)
            {
                item.Update(gameTime, tapPosition);
                if (item.Tap)
                {
                    _currentIndex = item.Index;
                }
            }
            if (InputHundler.MouseState.LeftButton ==
                ButtonState.Pressed)
            {

                switch (_currentIndex)
                {
                    case 0:
                        //Main Screen  //New Game
                        GameRef.mainScreen = new MainScreen(GameRef, GameRef.stateManager); //Dirty :p
                        StateManager.PushState(GameRef.mainScreen);
                        break;
                    case 1:
                        //Continue a Game
                        StateManager.PushState(GameRef.mainScreen);
                        break;
                    case 2:
                        //The Setting Screen
                        StateManager.PushState(GameRef.settingScreen);
                        break;
                    case 3:
                        //The Credit Screen 
                        StateManager.PushState(GameRef.aboutScreen);
                        break;
                }
            }

            base.Update(gameTime);
        }
        public override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.LightGray);
            GameRef.SpriteBatch.Begin();
            base.Draw(gameTime);
            foreach (MenuItem item in _menu)
            {
                item.Draw(gameTime);
            }
            GameRef.SpriteBatch.End();
        }
        #endregion
        
    }
}
