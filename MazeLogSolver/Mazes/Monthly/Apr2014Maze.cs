using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MazeLogSolver
{
    class Apr2014Maze : AbstractMaze
    {
        public const string MazeString = @"b k,w k,w b,w k,b k,w k,b b,w k
w k,b k,w k,b k,w k,w b,b k,w k
b k,w k,b k,b k,w k,b k,b k,w b
b k,b k,b k,b k,b k,w b,w k,b k
w k,w k,b k,w k,b k,w k,b k,w k
b k,b k,b k,w k,b k,b k,b k,w k
b k,w k,b k,b k,w k,w k,b k,b k
w k,b k,w k,b k,w k,w b,w k,w k";

        public Apr2014Maze(string maze = null)
            : base(maze == null ? MazeString : maze, new Position(0, 0), new Position(7, 7)) 
        { 

        }

        private RelativeMovementRule knightRule;
        private MovementRule KnightRule
        {
            get
            {
                if (knightRule == null)
                {
                    knightRule = new RelativeMovementRule();
                    knightRule.AddMovementOption(new MoveDescriptor() { Down = 1, Right = 2 }, MirrorOptions.All);
                    knightRule.AddMovementOption(new MoveDescriptor() { Down = 2, Right = 1 }, MirrorOptions.All);
                }
                return knightRule;
            }
        }

        private RelativeMovementRule bishopRule;
        private MovementRule BishopRule
        {
            get
            {
                if (bishopRule == null)
                {
                    bishopRule = new RelativeMovementRule();
                    bishopRule.AddMovementOption(new MoveDescriptor() { Down = 1, Right = 1 }, MirrorOptions.All);
                }
                return bishopRule;
            }
        }

        private TokenRule colorRule;
        private AbstractRule ColorRule
        {
            get
            {
                if (colorRule == null)
                {
                    colorRule = new TokenRule();
                    colorRule.PropertiesToCheck = new int[] { 0 };
                    colorRule.MinPropertiesMatch = 0;
                    colorRule.MaxPropertiesMatch = 0;
                }
                return colorRule;
            }
        }

        protected override RuleSet GetRules(Stack<GridPosition> history)
        {
            GridPosition currentPosition = history.Peek();
            MazeToken token = Grid[currentPosition.Row, currentPosition.Col];
            RuleSet rules = new RuleSet();
            if (token.Properties[1].Equals("k"))
            {
                rules.MovementRules.Add(KnightRule);
            }
            else
            {
                rules.MovementRules.Add(BishopRule);
            }
            rules.OtherRules.Add(ColorRule);
            return rules;
        }
    }
}
