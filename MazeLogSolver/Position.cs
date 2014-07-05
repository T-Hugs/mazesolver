using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MazeLogSolver
{
    /// <summary>
    /// Describes a generic, immutable position
    /// </summary>
    class Position
    {
        private int row;
        private int col;

        /// <summary>
        /// Row of the position
        /// </summary>
        public int Row { get { return row; } }

        /// <summary>
        /// Column of the position
        /// </summary>
        public int Col { get { return col; } }

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="r"></param>
        /// <param name="c"></param>
        public Position(int r, int c)
        {
            row = r;
            col = c;
        }

        /// <summary>
        /// Convert this to a grid position with the given grid
        /// </summary>
        /// <param name="g"></param>
        /// <returns></returns>
        public GridPosition ToGridPosition(AbstractGrid g)
        {
            return new GridPosition(row, col, g);
        }

        /// <summary>
        /// Returns the position obtained by moving up the given distance
        /// </summary>
        /// <param name="distance"></param>
        /// <returns></returns>
        public Position Up(int distance = 1)
        {
            return Move(0, -distance);
        }

        /// <summary>
        /// Returns the position obtained by moving down the given distance
        /// </summary>
        /// <param name="distance"></param>
        /// <returns></returns>
        public Position Down(int distance = 1)
        {
            return Move(0, distance);
        }

        /// <summary>
        /// Returns the position obtained by moving left the given distance
        /// </summary>
        /// <param name="distance"></param>
        /// <returns></returns>
        public Position Left(int distance = 1)
        {
            return Move(-distance, 0);
        }

        /// <summary>
        /// Returns the position obtained by moving right the given distance
        /// </summary>
        /// <param name="distance"></param>
        /// <returns></returns>
        public Position Right(int distance = 1)
        {
            return Move(distance, 0);
        }

        /// <summary>
        /// Returns the position obtained by moving up and to the right the given distance
        /// </summary>
        /// <param name="distance"></param>
        /// <returns></returns>
        public Position UpRight(int distance = 1)
        {
            return Move(distance, -distance);
        }

        /// <summary>
        /// Returns the position obtained by moving up and to the left the given distance
        /// </summary>
        /// <param name="distance"></param>
        /// <returns></returns>
        public Position UpLeft(int distance = 1)
        {
            return Move(-distance, -distance);
        }

        /// <summary>
        /// Returns the position obtained by moving down and to the right the given distance
        /// </summary>
        /// <param name="distance"></param>
        /// <returns></returns>
        public Position DownRight(int distance = 1)
        {
            return Move(distance, distance);
        }

        /// <summary>
        /// Returns the position obtained by moving down and to the left the given distance
        /// </summary>
        /// <param name="distance"></param>
        /// <returns></returns>
        public Position DownLeft(int distance = 1)
        {
            return Move(-distance, distance);
        }

        /// <summary>
        /// Returns the position obtained by moving as described
        /// </summary>
        /// <param name="right"></param>
        /// <param name="down"></param>
        /// <returns></returns>
        public virtual Position Move(int right, int down)
        {
            return new Position(row + down, col + right);
        }

        /// <summary>
        /// Returns the position obtained by moving as described
        /// </summary>
        /// <param name="movement"></param>
        /// <returns></returns>
        public virtual Position Move(MoveDescriptor movement)
        {
            return Move(movement.Right, movement.Down);
        }

        /// <summary>
        /// Two positions are equal if their row and columns are equal
        /// </summary>
        /// <param name="distance"></param>
        /// <returns></returns>
        public override bool Equals(object obj)
        {
            if (obj is Position)
            {
                Position other = obj as Position;
                return Row == other.Row && Col == other.Col;
            }
            return false;
        }

        /// <summary>
        /// Returns the hashcode of the string gotten by concatenating the row and column, separated by a comma
        /// </summary>
        /// <returns></returns>
        public override int GetHashCode()
        {
            return (Row + "," + Col).GetHashCode();
        }

        /// <summary>
        /// Gets a string representation of this position
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            return string.Format("({0},{1})", row, col);
        }
    }
}
