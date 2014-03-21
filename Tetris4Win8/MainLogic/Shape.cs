using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Xaml.Documents;
using Windows.UI.Xaml.Media.Animation;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using SharpDX.DirectWrite;
using System.Linq;
using Tetris4Win8.Screens;

namespace Tetris4Win8.MainLogic
{
    public enum ShapeType
    {
        Square,
        SShape,
        LShape,
        IShape
    }

    public enum Direction
    {
        Left,
        Right,
        None
    }

    public enum BlockDirection
    {
        Up1,
        Down1,
        Up2,
        Down2
    }
    class Shape
    {
        #region Prooperties
        public List<Point> ListSqures { get; set; }
        public ShapeType TypeShape { get; set; }
        public bool UpdateShape = true;
        public BlockDirection blockDirection;
        public bool GameOver = false;

        #endregion
        #region Methods
        public Shape(int shapeType)
        {

            switch (shapeType)
            {
                case 0:
                    TypeShape = ShapeType.LShape;
                    ListSqures = new List<Point>()
                    {
                        new Point(5,-1),
                        new Point(6,-1),
                        new Point(5,-2),
                        new Point(5,-3)                       
                    };
                    break;
                case 1:
                    TypeShape = ShapeType.IShape;
                    ListSqures = new List<Point>()
                    {
                        new Point(5,-1),                      
                        new Point(5,-2),
                        new Point(5,-3),
                        new Point(5,-4)
                                            
                    };
                    break;
                case 2:
                    TypeShape = ShapeType.SShape;
                    ListSqures = new List<Point>()
                    {
                        new Point(4,-1),
                        new Point(5,-1),
                        new Point(5,-2),
                        new Point(6,-2)                       
                    };
                    break;
                case 3:
                    TypeShape = ShapeType.Square;
                    ListSqures = new List<Point>()
                    {
                        new Point(5,-1),
                        new Point(6,-1),
                        new Point(5,-2),
                        new Point(6,-2)                       
                    };
                    break;
            }
            blockDirection = BlockDirection.Up1;
            //The Default Value Of The Direction  Is Up1
        }

        public void Update(int[,] board, Direction direction)
        {
            bool update = true;
            switch (direction)
            {
                case Direction.Left:
                    foreach (Point point in ListSqures)
                    {
                        if (point.X - 1 < 0)
                        {
                            update = false;
                            break;
                        }
                        else if (point.X - 1 >= 0 && point.Y >= 0)
                        {
                            if (board[point.X - 1, point.Y] == 1)
                            {
                                update = false;
                                break;
                            }
                        }
                    }
                    if (update)
                    {
                        for (int i = 0; i < ListSqures.Count; i++)
                        {

                            ListSqures[i] = new Point(ListSqures[i].X - 1, ListSqures[i].Y);
                        }
                    }
                    break;
                case Direction.Right:
                    foreach (Point point in ListSqures)
                    {
                        if (point.X + 1 >= MainScreen.Colomns)
                        {
                            update = false;
                            break;
                        }
                        else if (point.X + 1 >= 0 && point.Y >= 0)
                        {
                            if (board[point.X + 1, point.Y] == 1)
                            {
                                update = false;
                                break;
                            }
                        }
                    }
                    if (update)
                    {
                        for (int i = 0; i < ListSqures.Count; i++)
                        {

                            ListSqures[i] = new Point(ListSqures[i].X + 1, ListSqures[i].Y);
                        }
                    }
                    break;
                case Direction.None:
                    foreach (Point point in ListSqures)
                    {
                        if (point.Y + 1 >= MainScreen.Rows)
                        {
                            UpdateShape = false;
                            break;
                        }
                        else if (point.Y + 1 >= 0)
                        {
                            if (board[point.X, point.Y + 1] == 1)
                            {
                                UpdateShape = false;
                                break;
                            }
                        }
                    }
                    if (UpdateShape)
                    {
                        for (int i = 0; i < ListSqures.Count; i++)
                        {

                            ListSqures[i] = new Point(ListSqures[i].X, ListSqures[i].Y + 1);
                        }
                    }
                    else
                    {
                        for (int i = 0; i < ListSqures.Count; i++)
                        {
                            if (ListSqures[i].Y >= 0)
                                board[ListSqures[i].X, ListSqures[i].Y] = 1;
                            else
                            {
                                GameOver = true;
                                break;                                
                            }                         
                        }                 
                    }
                    break;
            }



        }
        public void UpdateBlockPosition(int[,] board)  //Up1 ->Down1 ->Up2 ->Down2
        {
            switch (blockDirection)
            {
                case BlockDirection.Up1:

                    switch (TypeShape)
                    {
                        case ShapeType.IShape:
                            //4
                            //1 2//3 4     
                            //2
                            //1
                            if ((ListSqures[0].X - 2 >= 0) && (ListSqures[3].X + 1 < MainScreen.Colomns) &&(ListSqures[3].Y>=0))
                            {
                                  if (board[ListSqures[0].X - 2, ListSqures[0].Y - 2] +
                                                               board[ListSqures[1].X - 1, ListSqures[1].Y - 1] +
                                                               board[ListSqures[3].X + 1, ListSqures[3].Y + 1] == 0)
                                    {
                                        blockDirection = BlockDirection.Down1;
                                        ListSqures[0] += new Point(-2, -2);
                                        ListSqures[1] += new Point(-1, -1);
                                        ListSqures[3] += new Point(1, 1);
                                    } 
                                
                            }
                            break;
                        case ShapeType.LShape:
                            //4
                            //1//3 4
                            //2//1 2
                            if (ListSqures[0].X - 1 >= 0 && ListSqures[3].X + 1 < MainScreen.Colomns && ListSqures[3].Y>=0)
                            {
                                if (board[ListSqures[0].X - 1, ListSqures[0].Y - 1] +
                                    board[ListSqures[1].X - 2, ListSqures[1].Y] +
                                    board[ListSqures[3].X + 1, ListSqures[3].Y + 1] == 0)
                                {
                                    blockDirection = BlockDirection.Down1;
                                    ListSqures[0] += new Point(-1, -1);
                                    ListSqures[1] += new Point(-2, 0);
                                    ListSqures[3] += new Point(1, 1);
                                }
                            }
                            break;
                        case ShapeType.SShape:
                              //3 4      1
                            //1 2    =>  2 3
                                         //4
                            if (ListSqures[3].Y + 2 < MainScreen.Rows && ListSqures[3].Y >= 0)
                            {
                                if (board[ListSqures[0].X + 1, ListSqures[0].Y - 1] +
                                    board[ListSqures[2].X + 1, ListSqures[2].Y + 1] +
                                    board[ListSqures[3].X, ListSqures[3].Y + 2] == 0)
                                {
                                    blockDirection = BlockDirection.Down1;
                                    ListSqures[0] += new Point(1, -1);
                                    ListSqures[2] += new Point(1, 1);
                                    ListSqures[3] += new Point(0, 2);
                                }
                            }
                            break;
                    }
                    break;
                case BlockDirection.Down1:
                    //4
                    //1 2//3 4     
                    //2
                    //1                    
                    switch (TypeShape)
                    {
                        case ShapeType.IShape:
                            if (ListSqures[0].X + 2 < MainScreen.Rows && ListSqures[3].Y >= 0)
                            {
                                if (board[ListSqures[0].X + 2, ListSqures[0].Y + 2] +
                                    board[ListSqures[1].X + 1, ListSqures[1].Y + 1] +
                                    board[ListSqures[3].X - 1, ListSqures[3].Y - 1] == 0)
                                {
                                    blockDirection = BlockDirection.Up2;
                                    ListSqures[0] += new Point(2, 2);
                                    ListSqures[1] += new Point(1, 1);
                                    ListSqures[3] += new Point(-1, -1);
                                }
                            }
                            break;
                        case ShapeType.LShape:
                            //2 1
                            //1//3 4
                            //2 4
                            if (ListSqures[3].Y + 1 < MainScreen.Rows && ListSqures[0].Y >= 0)
                            {
                                if (board[ListSqures[0].X + 1, ListSqures[0].Y - 1] +
                                    board[ListSqures[1].X, ListSqures[1].Y - 2] +
                                    board[ListSqures[3].X - 1, ListSqures[3].Y + 1] == 0)
                                {
                                    blockDirection = BlockDirection.Up2;
                                    ListSqures[0] += new Point(1, -1);
                                    ListSqures[1] += new Point(0, -2);
                                    ListSqures[3] += new Point(-1, 1);
                                }
                            }
                            break;
                        case ShapeType.SShape:
                              //3 4      1
                            //1 2    <=  2 3       
                                         //4
                            if (ListSqures[0].X - 1 >= 0 )
                            {
                                if (board[ListSqures[0].X - 1, ListSqures[0].Y + 1] +
                                    board[ListSqures[2].X - 1, ListSqures[2].Y - 1] +
                                    board[ListSqures[3].X, ListSqures[3].Y - 2] == 0)
                                {
                                    blockDirection = BlockDirection.Up2;
                                    ListSqures[0] += new Point(-1, 1);
                                    ListSqures[2] += new Point(-1, -1);
                                    ListSqures[3] += new Point(0, -2);
                                }
                            }
                            break;
                    }
                    break;
                case BlockDirection.Up2:

                    switch (TypeShape)
                    {
                        case ShapeType.IShape:
                            if (ListSqures[0].X - 2 >= 0 && ListSqures[3].X + 1 < MainScreen.Colomns)
                            {
                                if (board[ListSqures[0].X - 2, ListSqures[0].Y - 2] +
                                    board[ListSqures[1].X - 1, ListSqures[1].Y - 1] +
                                    board[ListSqures[3].X + 1, ListSqures[3].Y + 1] == 0)
                                {
                                    blockDirection = BlockDirection.Down2;
                                    ListSqures[0] += new Point(-2, -2);
                                    ListSqures[1] += new Point(-1, -1);
                                    ListSqures[3] += new Point(1, 1);
                                }
                            }
                            break;
                        case ShapeType.LShape:
                            //2 1 2
                            //4//3 1 
                            //4 
                            if (ListSqures[0].X + 1 < MainScreen.Colomns)
                            {
                                if (board[ListSqures[0].X + 1, ListSqures[0].Y + 1] +
                                    board[ListSqures[1].X + 2, ListSqures[1].Y] +
                                    board[ListSqures[3].X - 1, ListSqures[3].Y - 1] == 0)
                                {
                                    blockDirection = BlockDirection.Down2;
                                    ListSqures[0] += new Point(1, 1);
                                    ListSqures[1] += new Point(2, 0);
                                    ListSqures[3] += new Point(-1, -1);
                                }
                            }
                            break;
                        case ShapeType.SShape:
                            if (ListSqures[3].Y + 1 < MainScreen.Rows)
                            {
                                if (board[ListSqures[0].X + 1, ListSqures[0].Y - 1] +
                                    board[ListSqures[2].X + 1, ListSqures[2].Y + 1] +
                                    board[ListSqures[3].X, ListSqures[3].Y + 2] == 0)
                                {
                                    blockDirection = BlockDirection.Down2;
                                    ListSqures[0] += new Point(1, -1);
                                    ListSqures[2] += new Point(1, 1);
                                    ListSqures[3] += new Point(0, 2);
                                }
                            }
                            break;
                    }
                    break;
                case BlockDirection.Down2:

                    switch (TypeShape)
                    {
                        case ShapeType.IShape:
                            if (ListSqures[0].X + 2 < MainScreen.Rows)
                            {
                                if (board[ListSqures[0].X + 2, ListSqures[0].Y + 2] +
                                    board[ListSqures[1].X + 1, ListSqures[1].Y + 1] +
                                    board[ListSqures[3].X - 1, ListSqures[3].Y - 1] == 0)
                                {
                                    blockDirection = BlockDirection.Up1;
                                    ListSqures[0] += new Point(2, 2);
                                    ListSqures[1] += new Point(1, 1);
                                    ListSqures[3] += new Point(-1, -1);
                                }
                            }
                            break;
                        case ShapeType.LShape:
                            //4 2
                            //4 3 1 
                            //1 2  

                            if (ListSqures[0].Y + 1 < MainScreen.Rows)
                            {
                                if (board[ListSqures[0].X - 1, ListSqures[0].Y + 1] +
                                    board[ListSqures[1].X, ListSqures[1].Y + 2] +
                                    board[ListSqures[3].X + 1, ListSqures[3].Y - 1] == 0)
                                {
                                    blockDirection = BlockDirection.Up1;
                                    ListSqures[0] += new Point(-1, 1);
                                    ListSqures[1] += new Point(0, 2);
                                    ListSqures[3] += new Point(1, -1);
                                }
                            }
                            break;
                        case ShapeType.SShape:
                            if (ListSqures[0].X - 1 >= 0 )
                            {
                                if (board[ListSqures[0].X - 1, ListSqures[0].Y + 1] +
                                    board[ListSqures[2].X - 1, ListSqures[2].Y - 1] +
                                    board[ListSqures[3].X, ListSqures[3].Y - 2] == 0)
                                {
                                    blockDirection = BlockDirection.Up1;
                                    ListSqures[0] += new Point(-1, 1);
                                    ListSqures[2] += new Point(-1, -1);
                                    ListSqures[3] += new Point(0, -2);
                                }
                            }
                            break;
                    }
                    break;

            }


        }

        #endregion
    }
}
