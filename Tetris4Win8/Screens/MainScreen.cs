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
using Tetris4Win8.MainLogic;


namespace Tetris4Win8.Screens
{
    public class MainScreen : GameStateBase
    {
        private const int SpeedUpInterval = 100;
        public Vector2 BordOffset;
        public static int Rows = 15;
        public static int Colomns = 10;
        public const int SquareSize = 40;
        public Vector2 ScorePosition;
        public Vector2 NextShapePosition;
        private Texture2D squareTexture;
        float timer = 0f;
        float interval = 400;
        private float _defaultInterval = 400;        
        private Texture2D _mainGrid;
        private int[,] _mainGridMatrix;
        private Shape _nextShape;
        private Shape _currentShape;
        private int _score;
        private int _level;
        private Random _rand;
        private Rectangle _mainGridRect;
        Texture2D backgroundImage;
        SpriteFont font;
        public MainScreen(Game game, GameStateManager manager)
            : base(game, manager)
        {

        }
        protected override void LoadContent()
        {
            ContentManager Content = GameRef.Content;
            font = Content.Load<SpriteFont>(@"Font\BaseFont");
            backgroundImage = Content.Load<Texture2D>(@"Bg\MainScreen");
            _mainGrid = Content.Load<Texture2D>(@"Bg\MainGrd");
            squareTexture = Content.Load<Texture2D>(@"Bg\blacksquare");
            _rand = new Random();
            Initialise();
            base.LoadContent();
        }

        void Initialise()
        {
            _mainGridMatrix = new int[Colomns, Rows];
            //The Default Value In Each Cell Is 0
            int rShape = _rand.Next(4);
            _nextShape = new Shape(rShape);
            _currentShape = _nextShape;
            rShape = _rand.Next(4);
            _nextShape = new Shape(rShape);
            _level = 1;
            _score = 0;
            //Init Position
            BordOffset = new Vector2(700, 80);
            ScorePosition = new Vector2(700,600);
            NextShapePosition = new Vector2(GameRef.ScreenRectangle.Width / 2 - _mainGrid.Width / 2 + BordOffset.X, GameRef.ScreenRectangle.Height / 2 - _mainGrid.Height / 2 + BordOffset.Y);
        }
        public override void Update(GameTime gameTime)
        {
            timer += (float)gameTime.ElapsedGameTime.TotalMilliseconds;
            if (_currentShape.UpdateShape)
            {
                if (InputHundler.KeyPressed(Keys.Escape))
                {
                    StateManager.PopState();
                }
                if (InputHundler.KeyPressed(Keys.P))
                {
                    StateManager.PushState(GameRef.pauseScreen);
                }
                if (InputHundler.KeyPressed(Keys.M))
                {
                    StateManager.ChangeState(GameRef.menuScreen);
                }
                if (InputHundler.KeyPressed(Keys.Left))
                {
                    _currentShape.Update(_mainGridMatrix, Direction.Left);
                }
                if (InputHundler.KeyPressed(Keys.Right))
                {
                    _currentShape.Update(_mainGridMatrix, Direction.Right);
                }
                if (InputHundler.KeyPressed(Keys.Space))
                {
                    _currentShape.UpdateBlockPosition(_mainGridMatrix);
                }
                if (InputHundler.KeyDown(Keys.Down) )
                {
                    if (interval > SpeedUpInterval)
                        interval = SpeedUpInterval;
                }
                else
                {
                    interval = _defaultInterval;
                }
                if (timer > interval)
                {
                    _currentShape.Update(_mainGridMatrix, Direction.None);                    
                    timer = 0f;
                }
            }
            else
            {
                CheckBoard();
                if (_currentShape.GameOver)
                {
                    //Move To Game Over Screen For A While
                    StateManager.PushState(GameRef.gameOverScreen);
                }
                   
                _currentShape = _nextShape;
                //Create a New Shape Randomly 
                int rShape = _rand.Next(4);
                _nextShape = new Shape(rShape);

            }
            base.Update(gameTime);
        }
        public override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.LightGray);
            GameRef.SpriteBatch.Begin();
            DrawBoard();
            DrawMainBoard();
            foreach (Point squarePos in _currentShape.ListSqures)
            {
                GameRef.SpriteBatch.Draw(squareTexture, new Rectangle(GameRef.ScreenRectangle.Width / 2 - _mainGrid.Width / 2 + 232 + squarePos.X * (SquareSize + 1), GameRef.ScreenRectangle.Height / 2 - _mainGrid.Height / 2 + 14 + squarePos.Y *(SquareSize + 1), SquareSize , SquareSize ), Color.White);
            }
            DrawNextShape();
            DrawScore();
            GameRef.SpriteBatch.End();
            base.Draw(gameTime);
        }
        public void DrawMainBoard()
        {
            GameRef.SpriteBatch.Draw(_mainGrid, new Vector2(GameRef.ScreenRectangle.Width / 2 - _mainGrid.Width / 2, GameRef.ScreenRectangle.Height/2 - _mainGrid.Height/2 ), Color.Black);
        }
        public void DrawNextShape()
        {
            foreach (Point squarePos in _nextShape.ListSqures)
            {
                GameRef.SpriteBatch.Draw(squareTexture, new Rectangle((int)(NextShapePosition.X + (squarePos.X - 4) * (SquareSize / 2) + 1), (int)(NextShapePosition.Y + (squarePos.Y + 2) * (SquareSize / 2) + 1), SquareSize / 2 - 2, SquareSize / 2 - 2), Color.White);
            }
        }

        public void DrawScore()
        {
            GameRef.SpriteBatch.DrawString(font, _score.ToString(), new Vector2(GameRef.screenWidth / 2 - _mainGrid.Width / 2+ ScorePosition.X, GameRef.screenHeight / 2 - _mainGrid.Height / 2+ ScorePosition.Y), Color.Black);
        }
        //this function needs reconsideration
        public void CheckBoard()
        {
            for (int j = Rows-1; j >=0 ; j--)
            {
                int line = 1;
                for (int i = 0; i < Colomns; i++)
                {
                    line *= _mainGridMatrix[i, j];                    
                }
                if (line==1)
                {
                    //for (int ii = 0; ii < Colomns; ii++)
                    //{
                    //    _mainGridMatrix[ii, j]=0;
                    //}
                    for (int jj = j; jj-1>=0; jj--)
                    {
                        for (int ii = 0; ii < Colomns; ii++)
                        {
                            _mainGridMatrix[ii, jj] = _mainGridMatrix[ii, jj-1];
                        }
                    }
                    j++;
                    _score += 10;
                    if (_score> _level*1000)
                    {
                        _defaultInterval -= 30;
                    }
                }

            }
        }
        public void DrawBoard()
        {
            for (int i = 0; i < Colomns; i++)
            {
                for (int j = 0; j < Rows; j++)
                {
                    if (_mainGridMatrix[i, j] == 1)
                    {
                        GameRef.SpriteBatch.Draw(squareTexture, new Rectangle(GameRef.ScreenRectangle.Width / 2 - _mainGrid.Width / 2 + 232 + i *(SquareSize + 1), GameRef.ScreenRectangle.Height / 2 - _mainGrid.Height / 2 + 14 + j * (SquareSize + 1), SquareSize , SquareSize ), Color.White);
                    }
                }
            }
        }
    }
}
