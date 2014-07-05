using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MazeLogSolver
{
    /// <summary>
    /// A rule that allows movement only in the directions specified
    /// </summary>
    class DirectionMovementRule : MovementRule
    {
        public bool Up { get; set; }
        public bool UpRight { get; set; }
        public bool Right { get; set; }
        public bool DownRight { get; set; }
        public bool Down { get; set; }
        public bool DownLeft { get; set; }
        public bool Left { get; set; }
        public bool UpLeft { get; set; }

        /// <summary>
        ///     1
        ///   8   2
        /// 7       3
        ///   6   4
        ///     5
        /// </summary>
        /// <param name="num"></param>
        public void SetFromNumber(params int[] nums)
        {
            foreach (int num in nums)
            {
                switch (num)
                {
                    case 1 :
                        Up = true;
                        break;
                    case 2:
                        UpRight = true;
                        break;
                    case 3:
                        Right = true;
                        break;
                    case 4:
                        DownRight = true;
                        break;
                    case 5:
                        Down = true;
                        break;
                    case 6:
                        DownLeft = true;
                        break;
                    case 7:
                        Left = true;
                        break;
                    case 8:
                        UpLeft = true;
                        break;
                }
            }
        }

        public override List<MoveDescriptor> SatisfyingMoves(AbstractMaze maze)
        {
            List<MoveDescriptor> moves = new List<MoveDescriptor>();

            if (Up)
            {
                moves.AddRange(Enumerable.Range(1, maze.Grid.RowCount).Select(n => new MoveDescriptor() { Down = -n, Right = 0 }));
            }
            if (UpRight)
            {
                moves.AddRange(Enumerable.Range(1, maze.Grid.RowCount).Select(n => new MoveDescriptor() { Down = -n, Right = n }));
            }
            if (Right)
            {
                moves.AddRange(Enumerable.Range(1, maze.Grid.RowCount).Select(n => new MoveDescriptor() { Down = 0, Right = n }));
            }
            if (DownRight)
            {
                moves.AddRange(Enumerable.Range(1, maze.Grid.RowCount).Select(n => new MoveDescriptor() { Down = n, Right = n }));
            }
            if (Down)
            {
                moves.AddRange(Enumerable.Range(1, maze.Grid.RowCount).Select(n => new MoveDescriptor() { Down = n, Right = 0 }));
            }
            if (DownLeft)
            {
                moves.AddRange(Enumerable.Range(1, maze.Grid.RowCount).Select(n => new MoveDescriptor() { Down = n, Right = -n }));
            }
            if (Left)
            {
                moves.AddRange(Enumerable.Range(1, maze.Grid.RowCount).Select(n => new MoveDescriptor() { Down = 0, Right = -n }));
            }
            if (UpLeft)
            {
                moves.AddRange(Enumerable.Range(1, maze.Grid.RowCount).Select(n => new MoveDescriptor() { Down = -n, Right = -n }));
            }

            return moves;
        }

        public override bool RuleSatisfied(Position from, Position to, AbstractMaze maze)
        {
            bool isDiag = to.Col - from.Col == to.Row - from.Row;
            return
                (Up && to.Row - from.Row < 0) ||
                (UpRight && to.Row - from.Row < 0 && to.Col - from.Col > 0 && isDiag) ||
                (Right && to.Col - from.Col > 0) ||
                (DownRight && to.Row - from.Row > 0 && to.Col - from.Col > 0 && isDiag) ||
                (Down && to.Row - from.Row > 0) ||
                (DownLeft && to.Row - from.Row > 0 && to.Col - from.Col < 0 && isDiag) ||
                (Left && to.Col - from.Col < 0) ||
                (UpLeft && to.Row - from.Row < 0 && to.Col - from.Col < 0 && isDiag);
        }
    }
}
