using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MazeLogSolver.Rules.Common
{
    static class DirectionRules
    {
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
