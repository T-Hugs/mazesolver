using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MazeLogSolver
{
    /// <summary>
    /// Describes a set of rules
    /// </summary>
    class RuleSet
    {
        /// <summary>
        /// Movement rules 
        /// </summary>
        public List<MovementRule> MovementRules { get; set; }

        /// <summary>
        /// Non-movement rules
        /// </summary>
        public List<AbstractRule> OtherRules { get; set; }

        /// <summary>
        /// Constructor
        /// </summary>
        public RuleSet()
        {
            MovementRules = new List<MovementRule>();
            OtherRules = new List<AbstractRule>();
        }
    }
}
