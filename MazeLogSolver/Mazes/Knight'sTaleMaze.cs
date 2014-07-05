using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MazeLogSolver
{
    class KnightsTaleMaze : AbstractMaze
    {
        public const string MazeString = @".,.,.,.,.,.,.,.
.,.,0,.,.,.,0,.
0,.,0,.,.,0,.,.
.,.,0,0,0,0,.,.
.,.,.,0,0,0,.,.
.,0,0,.,0,.,.,.
0,0,.,.,.,0,.,.
.,.,.,.,0,0,0,.";

        public KnightsTaleMaze(string maze = null)
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

        private TokenRule passableRule;
        private AbstractRule PassableRule
        {
            get
            {
                if (passableRule == null)
                {
                    passableRule = new TokenRule();
                    passableRule.MinPropertiesMatch = 1;
                    passableRule.MaxPropertiesMatch = 1;
                    passableRule.PropertiesToCheck = new int[] { 0 };
                }
                return passableRule;
            }
        }

        protected override RuleSet GetRules(Stack<GridPosition> history)
        {
            RuleSet rules = new RuleSet();
            rules.MovementRules.Add(KnightRule);
            rules.OtherRules.Add(PassableRule);
            return rules;
        }
    }
}
