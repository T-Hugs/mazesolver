using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MazeLogSolver.Rules.Common
{
    static class DirectionRules
    {
        /// <summary>
        /// Rule that allows movement in the horizontal or vertical directions
        /// </summary>
        public static DirectionMovementRule RanksAndFiles
        {
            get
            {
                DirectionMovementRule rankAndFileRule = new DirectionMovementRule();
                rankAndFileRule.SetFromNumber(1, 3, 5, 7);
                return rankAndFileRule;
            }
        }
    }
}
