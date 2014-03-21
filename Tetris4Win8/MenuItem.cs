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

namespace Tetris4Win8
{
    public class MenuItem
    {
        #region consts
        const float MinScale = 0.8f;
        const float MaxScale = 1;
        #endregion
        #region Fields
        private SpriteBatch _spriteBatch;
        private Texture2D _texture;
        private Vector2 _position;
        private Vector2 _origin;
        private float _timer = 0;
        private float _scale = 0.8f;
        #endregion
        #region Properties
        public bool Tap { get; set; }
        public int Index { get; set; }
        public Rectangle Bound
        {
            get
            {
                return new Rectangle((int)(_position.X - _origin.X * _scale),
                  (int)(_position.Y - _origin.Y * _scale),
                  (int)(_texture.Width * _scale),
                  (int)(_texture.Height * _scale));
            }
        }
        #endregion
        #region Methods
        public MenuItem(SpriteBatch _spritrBatch, Vector2 Location, Texture2D Texture)
        {
            _position = Location;
            _texture = Texture;
            if (_texture != null) _origin = new Vector2(_texture.Width / 2, _texture.Height / 2);
            _spriteBatch = _spritrBatch;
        }
        public void Update(GameTime gameTime, Vector2 tapPosition)
        {
            Tap = Bound.Contains((int)tapPosition.X,
                (int)tapPosition.Y);
            _timer += (float)gameTime.ElapsedGameTime.TotalMilliseconds;
        }
        public void Draw(GameTime gameTime)
        {
            if (Tap)
            {
                if (_scale <= MaxScale && _timer > 200)
                {
                    _scale += 0.1f;
                }
                _spriteBatch.Draw(_texture, _position, null, Color.Red,
                    0f, _origin, _scale, SpriteEffects.None, 0f);
            }
            else
            {
                if (_scale > MinScale && _timer > 200)
                {
                    _scale -= 0.1f;
                }
                _spriteBatch.Draw(_texture, _position, null, Color.White,
                    0f, _origin, _scale, SpriteEffects.None, 0f);
            }
        }
        #endregion      
    }
}
