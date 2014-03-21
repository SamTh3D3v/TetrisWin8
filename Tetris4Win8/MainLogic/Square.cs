using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;

namespace Tetris4Win8.MainLogic
{
    class Square
    {
        #region Properties

        public int width { get; set; }
        public Point Position { get; set; }
        public bool IsAlive { get; set; }
        #endregion
    }
}
