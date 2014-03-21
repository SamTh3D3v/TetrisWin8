using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Tetris4Win8;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;

namespace Tetris4Win8.Screens
{
    public class GameStateBase : GameState
    {
        #region Fields
        protected Tetris GameRef;
        #endregion
        #region Properties
        #endregion

        public GameStateBase(Game game, GameStateManager manager)
            : base(game, manager)
        {
            GameRef = (Tetris)game;  
        }

    }
}

