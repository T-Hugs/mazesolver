using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MazeLogSolver.Rules.Common
{
    static class ChessRules
    {
        /// <summary>
        /// Rule that models standard knight movement
        /// </summary>
        public static MovementRule KnightRule
        {
            get
            {
                RelativeMovementRule knightRule = new RelativeMovementRule();
                knightRule.AddMovementOption(new MoveDescriptor() { Down = 1, Right = 2 }, MirrorOptions.All);
                knightRule.AddMovementOption(new MoveDescriptor() { Down = 2, Right = 1 }, MirrorOptions.All);
                return knightRule;
            }
        }
    }
}
