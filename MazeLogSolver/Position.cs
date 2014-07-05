using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MazeLogSolver
{
    class Position
    {
        private int row;
        private int col;

        public int Row { get { return row; } }
        public int Col { get { return col; } }

        public Position(int r, int c)
        {
            row = r;
            col = c;
        }

        public GridPosition ToGridPosition(AbstractGrid g)
        {
            return new GridPosition(row, col, g);
        }

        public Position Up(int distance = 1)
        {
            return Move(0, -distance);
        }
        public Position Down(int distance = 1)
        {
            return Move(0, distance);
        }
        public Position Left(int distance = 1)
        {
            return Move(-distance, 0);
        }
        public Position Right(int distance = 1)
        {
            return Move(distance, 0);
        }
        public Position UpRight(int distance = 1)
        {
            return Move(distance, -distance);
        }
        public Position UpLeft(int distance = 1)
        {
            return Move(-distance, -distance);
        }
        public Position DownRight(int distance = 1)
        {
            return Move(distance, distance);
        }
        public Position DownLeft(int distance = 1)
        {
            return Move(-distance, distance);
        }

        public virtual Position Move(int right, int down)
        {
            return new Position(row + down, col + right);
        }

        public virtual Position Move(MoveDescriptor movement)
        {
            return Move(movement.Right, movement.Down);
        }

        public override bool Equals(object obj)
        {
            if (obj is Position)
            {
                Position other = obj as Position;
                return Row == other.Row && Col == other.Col;
            }
            return false;
        }

        public override int GetHashCode()
        {
            return (Row + "," + Col).GetHashCode();
        }

        public override string ToString()
        {
            return string.Format("({0},{1})", row, col);
        }
    }
}
