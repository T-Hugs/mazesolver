using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MazeLogSolver
{
    /// <summary>
    /// Describes a position on a grid
    /// </summary>
    class GridPosition : Position
    {
        private AbstractGrid grid;
        public GridPosition TopLeft { get { return new GridPosition(0, 0, grid); } }
        public GridPosition TopRight { get { return new GridPosition(0, grid.ColCount - 1, grid); } }
        public GridPosition BottomLeft { get { return new GridPosition(grid.RowCount - 1, 0, grid); } }
        public GridPosition BottomRight { get { return new GridPosition(grid.RowCount - 1, grid.ColCount - 1, grid); } }

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="r">Position's row</param>
        /// <param name="c">Position's column</param>
        /// <param name="g">Grid we're on</param>
        public GridPosition(int r, int c, AbstractGrid g)
            : base(r, c)
        {
            grid = g;
        }

        /// <summary>
        /// Returns true if the given row and column is in bounds of the grid
        /// </summary>
        /// <param name="row"></param>
        /// <param name="col"></param>
        /// <returns></returns>
        public bool InBounds(int row, int col)
        {
            return grid.RowCount > row && grid.ColCount > col && row >= 0 && col >= 0;
        }

        /// <summary>
        /// Returns true if the given position is in bounds of the grid
        /// </summary>
        /// <param name="pos"></param>
        /// <returns></returns>
        public bool InBounds(Position pos)
        {
            return InBounds(pos.Row, pos.Col);
        }

        /// <summary>
        /// Returns true if it is legal to move from the current position as described
        /// </summary>
        /// <param name="right"></param>
        /// <param name="down"></param>
        /// <returns></returns>
        public bool CanMove(int right, int down)
        {
            return InBounds(base.Move(right, down));
        }

        /// <summary>
        /// Returns true if it is legal to move from the current position as described
        /// </summary>
        /// <param name="movement"></param>
        /// <returns></returns>
        public bool CanMove(MoveDescriptor movement)
        {
            return CanMove(movement.Right, movement.Down);
        }

        /// <summary>
        /// Returns the grid position on the left side of the grid, relative to the current position
        /// </summary>
        /// <returns></returns>
        public GridPosition ToLeftSide()
        {
            return new GridPosition(Row, 0, grid);
        }

        /// <summary>
        /// Returns the grid position on the top side of the grid, relative to the current position
        /// </summary>
        /// <returns></returns>
        public GridPosition ToTop()
        {
            return new GridPosition(0, Col, grid);
        }

        /// <summary>
        /// Returns the grid position on the right side of the grid, relative to the current position
        /// </summary>
        /// <returns></returns>
        public GridPosition ToRightSide()
        { 
            return new GridPosition(Row, grid.ColCount - 1, grid);
        }

        /// <summary>
        /// Returns the grid position on the bottom side of the grid, relative to the current position
        /// </summary>
        /// <returns></returns>
        public GridPosition ToBottom()
        {
            return new GridPosition(grid.RowCount - 1, Col, grid);
        }

        /// <summary>
        /// Returns a new GridPosition by moving up the given distance
        /// </summary>
        /// <param name="distance">Distance to move</param>
        /// <param name="wrapOption">How to treat wrapping. Leave null to use the grid's default</param>
        /// <returns>GridPosition after moving</returns>
        public GridPosition Up(int distance = 1, GridWrapOption? wrapOption = null)
        {
            return Move(0, -distance, wrapOption);
        }

        /// <summary>
        /// Returns a new GridPosition by moving down the given distance
        /// </summary>
        /// <param name="distance">Distance to move</param>
        /// <param name="wrapOption">How to treat wrapping. Leave null to use the grid's default</param>
        /// <returns>GridPosition after moving</returns>
        public GridPosition Down(int distance = 1, GridWrapOption? wrapOption = null)
        {
            return Move(0, distance, wrapOption);
        }

        /// <summary>
        /// Returns a new GridPosition by moving left the given distance
        /// </summary>
        /// <param name="distance">Distance to move</param>
        /// <param name="wrapOption">How to treat wrapping. Leave null to use the grid's default</param>
        /// <returns>GridPosition after moving</returns>
        public GridPosition Left(int distance = 1, GridWrapOption? wrapOption = null)
        {
            return Move(-distance, 0, wrapOption);
        }

        /// <summary>
        /// Returns a new GridPosition by moving right the given distance
        /// </summary>
        /// <param name="distance">Distance to move</param>
        /// <param name="wrapOption">How to treat wrapping. Leave null to use the grid's default</param>
        /// <returns>GridPosition after moving</returns>
        public GridPosition Right(int distance = 1, GridWrapOption? wrapOption = null)
        {
            return Move(distance, 0, wrapOption);
        }

        /// <summary>
        /// Returns a new GridPosition by moving up and to the right the given distance
        /// </summary>
        /// <param name="distance">Distance to move</param>
        /// <param name="wrapOption">How to treat wrapping. Leave null to use the grid's default</param>
        /// <returns>GridPosition after moving</returns>
        public GridPosition UpRight(int distance = 1, GridWrapOption? wrapOption = null)
        {
            return Move(distance, -distance, wrapOption);
        }

        /// <summary>
        /// Returns a new GridPosition by moving up and to the left the given distance
        /// </summary>
        /// <param name="distance">Distance to move</param>
        /// <param name="wrapOption">How to treat wrapping. Leave null to use the grid's default</param>
        /// <returns>GridPosition after moving</returns>
        public GridPosition UpLeft(int distance = 1, GridWrapOption? wrapOption = null)
        {
            return Move(-distance, -distance, wrapOption);
        }

        /// <summary>
        /// Returns a new GridPosition by moving down and to the right the given distance
        /// </summary>
        /// <param name="distance">Distance to move</param>
        /// <param name="wrapOption">How to treat wrapping. Leave null to use the grid's default</param>
        /// <returns>GridPosition after moving</returns>
        public GridPosition DownRight(int distance = 1, GridWrapOption? wrapOption = null)
        {
            return Move(distance, distance, wrapOption);
        }

        /// <summary>
        /// Returns a new GridPosition by moving down and to the left the given distance
        /// </summary>
        /// <param name="distance">Distance to move</param>
        /// <param name="wrapOption">How to treat wrapping. Leave null to use the grid's default</param>
        /// <returns>GridPosition after moving</returns>
        public GridPosition DownLeft(int distance = 1, GridWrapOption? wrapOption = null)
        {
            return Move(-distance, distance, wrapOption);
        }

        /// <summary>
        /// Returns a new GridPosition by moving the given number of squares right and down.
        /// Negative values may be used to move left and up.
        /// </summary>
        /// <param name="right">Number of squares to move right</param>
        /// <param name="down">Number of squares to move left</param>
        /// <param name="wrapOption">Determines how to handle out of bounds movements</param>
        /// <returns>The new GridPosition</returns>
        public GridPosition Move(int right, int down, GridWrapOption? wrapOption = null)
        {
            // If the wrap option is null, use the default option set on the grid
            GridWrapOption opt;
            if (wrapOption == null)
            {
                opt = grid.GridWrapOption;
            }
            else
            {
                opt = wrapOption.Value;
            }
            Position newPosition = base.Move(right, down);
            if (opt == GridWrapOption.Stay)
            {
                if (newPosition.Row >= grid.RowCount)
                {
                    newPosition = new Position(grid.RowCount - 1, newPosition.Col);
                }
                if (newPosition.Row < 0)
                {
                    newPosition = new Position(0, newPosition.Col);
                }
                if (newPosition.Col >= grid.ColCount)
                {
                    newPosition = new Position(newPosition.Row, grid.ColCount - 1);
                }
                if (newPosition.Col < 0)
                {
                    newPosition = new Position(newPosition.Row, 0);
                }
            } 
            else if (opt == GridWrapOption.Throw)
            {
                if (!InBounds(newPosition))
                {
                    throw new OutOfBoundsException("Can't move to Row: {0}, Col: {0}.", newPosition.Row, newPosition.Col);
                }
            }
            else if (opt == GridWrapOption.Wrap)
            {
                newPosition = new Position(newPosition.Row % grid.RowCount, newPosition.Col % grid.ColCount);
            }

            return new GridPosition(newPosition.Row, newPosition.Col, grid);
        }

        /// <summary>
        /// Returns a new GridPosition by moving as described.
        /// </summary>
        /// <param name="movement">Describes the move</param>
        /// <param name="wrapOption">Determines how to handle out of bounds movements</param>
        /// <returns>The new GridPosition</returns>
        public GridPosition Move(MoveDescriptor movement, GridWrapOption? wrapOption = null)
        {
            return Move(movement.Right, movement.Down, wrapOption);
        }
    }

    /// <summary>
    /// An exception thrown if a movement goes out of bounds, and the wrap option is set to throw
    /// </summary>
    class OutOfBoundsException : Exception 
    { 
        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="message"></param>
        /// <param name="args"></param>
        public OutOfBoundsException(string message, params object[] args)
            :base(string.Format(message, args))
        {

        }
    }
}
