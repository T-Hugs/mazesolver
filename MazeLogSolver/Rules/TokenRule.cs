using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MazeLogSolver
{
    class TokenRule : AbstractRule
    {
        public int MinPropertiesMatch { get; set; }
        public int MaxPropertiesMatch { get; set; }
        public int[] PropertiesToCheck { get; set; }
        
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
