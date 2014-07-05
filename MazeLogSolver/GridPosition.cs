using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MazeLogSolver
{
    class GridPosition : Position
    {
        private AbstractGrid grid;
        public GridPosition TopLeft { get { return new GridPosition(0, 0, grid); } }
        public GridPosition TopRight { get { return new GridPosition(0, grid.ColCount - 1, grid); } }
        public GridPosition BottomLeft { get { return new GridPosition(grid.RowCount - 1, 0, grid); } }
        public GridPosition BottomRight { get { return new GridPosition(grid.RowCount - 1, grid.ColCount - 1, grid); } }

        public GridPosition(int r, int c, AbstractGrid g)
            : base(r, c)
        {
            grid = g;
        }

        public bool InBounds(int row, int col)
        {
            return grid.RowCount > row && grid.ColCount > col && row >= 0 && col >= 0;
        }
        public bool InBounds(Position pos)
        {
            return InBounds(pos.Row, pos.Col);
        }

        public bool CanMove(int right, int down)
        {
            return InBounds(base.Move(right, down));
        }

        public bool CanMove(MoveDescriptor movement)
        {
            return CanMove(movement.Right, movement.Down);
        }

        public GridPosition ToLeftSide()
        {
            return new GridPosition(Row, 0, grid);
        }
        public GridPosition ToTop()
        {
            return new GridPosition(0, Col, grid);
        }
        public GridPosition ToRightSide()
        { 
            return new GridPosition(Row, grid.ColCount - 1, grid);
        }
        public GridPosition ToBottom()
        {
            return new GridPosition(grid.RowCount - 1, Col, grid);
        }
        public GridPosition Up(int distance = 1, GridWrapOption? wrapOption = null)
        {
            return Move(0, -distance, wrapOption);
        }
        public GridPosition Down(int distance = 1, GridWrapOption? wrapOption = null)
        {
            return Move(0, distance, wrapOption);
        }
        public GridPosition Left(int distance = 1, GridWrapOption? wrapOption = null)
        {
            return Move(-distance, 0, wrapOption);
        }
        public GridPosition Right(int distance = 1, GridWrapOption? wrapOption = null)
        {
            return Move(distance, 0, wrapOption);
        }
        public GridPosition UpRight(int distance = 1, GridWrapOption? wrapOption = null)
        {
            return Move(distance, -distance, wrapOption);
        }
        public GridPosition UpLeft(int distance = 1, GridWrapOption? wrapOption = null)
        {
            return Move(-distance, -distance, wrapOption);
        }
        public GridPosition DownRight(int distance = 1, GridWrapOption? wrapOption = null)
        {
            return Move(distance, distance, wrapOption);
        }
        public GridPosition DownLeft(int distance = 1, GridWrapOption? wrapOption = null)
        {
            return Move(-distance, distance, wrapOption);
        }
        public GridPosition Move(int right, int down, GridWrapOption? wrapOption = null)
        {
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
        public GridPosition Move(MoveDescriptor movement, GridWrapOption? wrapOption = null)
        {
            return Move(movement.Right, movement.Down, wrapOption);
        }
    }

    class OutOfBoundsException : Exception 
    { 
        public OutOfBoundsException(string message, params object[] args)
            :base(string.Format(message, args))
        {

        }
    }
}
