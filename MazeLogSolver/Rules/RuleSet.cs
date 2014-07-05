using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MazeLogSolver
{
    class RuleSet
    {
        public List<MovementRule> MovementRules { get; set; }
        public List<AbstractRule> OtherRules { get; set; }

        public RuleSet()
        {
            MovementRules = new List<MovementRule>();
            OtherRules = new List<AbstractRule>();
        }
    }
}
