using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MazeLogSolver
{
    /// <summary>
    /// Describes a rule based on the token properties at the origination and destination
    /// </summary>
    class TokenRule : AbstractRule
    {
        /// <summary>
        /// The minimum number of token properties that must be matched
        /// </summary>
        public int MinPropertiesMatch { get; set; }

        /// <summary>
        /// The maximum number of token properties that must be matched
        /// </summary>
        public int MaxPropertiesMatch { get; set; }

        /// <summary>
        /// The property indices to check
        /// </summary>
        public int[] PropertiesToCheck { get; set; }
        
        /// <summary>
        /// Returns true if the token at from position has [MinPropertiesMatch,MaxPropertiesMatch] matching properties, 
        /// where the properties checked are only those in PropertiesToCheck
        /// </summary>
        /// <param name="from">From position</param>
        /// <param name="to">To position</param>
        /// <param name="maze">The maze</param>
        /// <returns>True if the rule is satisfied</returns>
        public override bool RuleSatisfied(Position from, Position to, AbstractMaze maze)
        {
            MazeToken fromToken = maze.Grid[from.Row, from.Col];
            MazeToken toToken = maze.Grid[to.Row, to.Col];
            int matchingProperties = 0;
            foreach (int p in PropertiesToCheck)
            {
                if (fromToken.Properties[p].Equals(toToken.Properties[p]))
                {
                    matchingProperties++;
                }
            }
            return matchingProperties >= MinPropertiesMatch && matchingProperties <= MaxPropertiesMatch;
        }
    }
}
