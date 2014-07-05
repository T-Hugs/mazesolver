using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MazeLogSolver
{
    /// <summary>
    /// A rule that allows movement along a certain number of cells
    /// </summary>
    class DistanceMovementRule : MovementRule
    {
        /// <summary>
        /// The minimum number of spaces allowed to move in the upward direction
        /// </summary>
        public int UpLeast { get; set; }

        /// <summary>
        /// The maximum number of spaces allowed to move in the upward direction
        /// </summary>
        public int UpMost { get; set; }

        /// <summary>
        /// When moving in the upward direction, the multiple of spaces required to move
        /// </summary>
        public int UpMultiple { get; set; }
        public int DownLeast { get; set; }
        public int DownMost { get; set; }
        public int DownMultiple { get; set; }
        public int LeftLeast { get; set; }
        public int LeftMost { get; set; }
        public int LeftMultiple { get; set; }
        public int RightLeast { get; set; }
        public int RightMost { get; set; }
        public int RightMultiple { get; set; }
                
        /// <summary>
        /// Set the minimum number of squares to move in all directions
        /// </summary>
        /// <param name="val">The minimum number of squares</param>
        public void SetLeast(int val)
        {
            UpLeast = val;
            DownLeast = val;
            LeftLeast = val;
            RightLeast = val;
        }

        /// <summary>
        /// Set the maximum number of squares to move in all directions
        /// </summary>
        /// <param name="val">The maximum number of squares</param>
        public void SetMost(int val)
        {
            UpMost = val;
            DownMost = val;
            LeftMost = val;
            RightMost = val;
        }

        /// <summary>
        /// Set the multiple for the number of squares to move in all directions
        /// </summary>
        /// <param name="val">The multiple for the number of squares</param>
        public void SetMultiple(int val)
        {
            UpMultiple = val;
            DownMultiple = val;
            LeftMultiple = val;
            RightMultiple = val;
        }

        /// <summary>
        /// Set all properties given a string in the format described below:
        /// uleast,umost,umultiple|dleast,dmost,dmultiple|lleast,lmost,lmultiple|rleast,rmost,rmultiple
        /// </summary>
        /// <param name="s"></param>
        public void SetFromString(string s)
        {
            string[] udlr = s.Split('|');
            string[] u = udlr[0].Split(',');
            string[] d = udlr[1].Split(',');
            string[] l = udlr[2].Split(',');
            string[] r = udlr[3].Split(',');
            UpLeast = int.Parse(u[0]);
            UpMost = int.Parse(u[1]);
            UpMultiple = int.Parse(u[2]);
            DownLeast = int.Parse(d[0]);
            DownMost = int.Parse(d[1]);
            DownMultiple = int.Parse(d[2]);
            LeftLeast = int.Parse(l[0]);
            LeftMost = int.Parse(l[1]);
            LeftMultiple = int.Parse(l[2]);
            RightLeast = int.Parse(r[0]);
            RightMost = int.Parse(r[1]);
            RightMultiple = int.Parse(r[2]);
        }

        /// <summary>
        /// Determines if this rule is satisfied for the given move.
        /// </summary>
        /// <param name="from">From position</param>
        /// <param name="to">To position</param>
        /// <param name="maze">The maze</param>
        /// <returns>Returns true if the rule was satisfied.</returns>
        public override bool RuleSatisfied(Position from, Position to, AbstractMaze maze)
        {
            int downDist = to.Row - from.Row;
            int rightDist = to.Col - from.Col;
            if (downDist < 0)
            {
                int upDist = -downDist;
                if (! (upDist >= UpLeast && upDist <= UpMost && upDist < maze.Grid.RowCount && upDist % UpMultiple == 0))
                {
                    return false;
                }
            }
            else
            {
                if (!(downDist >= DownLeast && downDist <= DownMost && downDist <= maze.Grid.RowCount && downDist % DownMultiple == 0))
                {
                    return false;
                }
            }
            if (rightDist < 0)
            {
                int leftDist = -rightDist;
                if (!(leftDist >= LeftLeast && leftDist <= LeftMost && leftDist <= maze.Grid.ColCount && leftDist % LeftMultiple == 0))
                {
                    return false;
                }
            }
            else
            {
                if (!(rightDist >= RightLeast && rightDist <= RightMost && rightDist <= maze.Grid.ColCount && rightDist % RightMultiple == 0))
                {
                    return false;
                }
            }
            return true;
        }

        /// <summary>
        /// Returns a list of all potential moves given the set distance properties (based on the given maze)
        /// Note: Many of the moves may not be legal, i.e., out of bounds.
        /// Todo: make this more linq-y
        /// </summary>
        /// <param name="maze">The maze</param>
        /// <returns></returns>
        public override List<MoveDescriptor> SatisfyingMoves(AbstractMaze maze)
        {
            List<MoveDescriptor> moves = new List<MoveDescriptor>();
            IEnumerable<int> upValues, downValues, leftValues, rightValues;
            upValues = Enumerable.Range(UpLeast, Math.Max(UpMost, maze.Grid.RowCount)).Where(n => n % UpMultiple == 0);
            downValues = Enumerable.Range(DownLeast, Math.Max(DownMost, maze.Grid.RowCount)).Where(n => n % DownMultiple == 0);
            leftValues = Enumerable.Range(LeftLeast, Math.Max(LeftMost, maze.Grid.ColCount)).Where(n => n % LeftMultiple == 0);
            rightValues = Enumerable.Range(RightLeast, Math.Max(RightMost, maze.Grid.ColCount)).Where(n => n % RightMultiple == 0);

            foreach (int v in upValues)
            {
                foreach (int w in leftValues)
                {
                    moves.Add(new MoveDescriptor()
                    {
                        Down = -v,
                        Right = -w
                    });
                }
                foreach (int w in rightValues)
                {
                    moves.Add(new MoveDescriptor()
                    {
                        Down = -v,
                        Right = w
                    });
                }
            }
            foreach (int v in downValues)
            {
                foreach (int w in leftValues)
                {
                    moves.Add(new MoveDescriptor()
                    {
                        Down = v,
                        Right = -w
                    });
                }
                foreach (int w in rightValues)
                {
                    moves.Add(new MoveDescriptor()
                    {
                        Down = v,
                        Right = w
                    });
                }
            }
            return moves;
        }   
    }
}
