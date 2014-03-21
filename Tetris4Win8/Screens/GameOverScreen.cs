using System;
using System.Collections.Generic;
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
    public class GameOverScreen : GameStateBase
    {
        #region Field
        Texture2D backgroundImage;
        Song bgSound;
        private float timer;
        private float gameovertimer = 4000;
        #endregion

        public GameOverScreen(Game game, GameStateManager manager)
            : base(game, manager)
        {
        }

        #region Method
        protected override void LoadContent()
        {
            ContentManager Content = GameRef.Content;

            backgroundImage = Content.Load<Texture2D>(@"Bg\GameOverScreen");
            // startVideo = Content.Load<Video>(@"Assets\Videos\Intro");
            //  player.Play(startVideo);
            base.LoadContent();
        }
        public override void Update(GameTime gameTime)
        {
            if (timer > gameovertimer)
            {
                timer = 0f;
                StateManager.PopState();
                StateManager.PopState();
                GameRef.mainScreen = new MainScreen(GameRef, GameRef.stateManager); //Dirty :p
                StateManager.PushState(GameRef.mainScreen);
                StateManager.PopState();
            }
            else
            {
                timer += (float)gameTime.ElapsedGameTime.TotalMilliseconds;
            }
            base.Update(gameTime);
        }
        public override void Draw(GameTime gameTime)
        {
            GameRef.SpriteBatch.Begin();
            GameRef.SpriteBatch.Draw(backgroundImage,
                              new Vector2(GameRef.screenWidth / 2 - backgroundImage.Width / 2, GameRef.screenHeight / 2 - backgroundImage.Height / 2), Color.White);
            GameRef.SpriteBatch.End();
            base.Draw(gameTime);
        }
        #endregion    }
    }
}
