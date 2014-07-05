using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MazeLogSolver
{
    class May2014Maze : AbstractMaze
    {
        public const string MazeString = @"2,*,2,3,4,3,1
*,4,3,2,3,4,3
5,*,3,4,*,2,4
2,3,3,*,3,3,2
3,2,2,5,*,2,3
4,4,3,3,2,*,4
*,1,3,4,3,2,0";

        public May2014Maze(string maze = null)
            : base(maze == null ? MazeString : maze, new Position(0, 0), new Position(6, 6)) 
        {

        }

        protected override RuleSet GetRules(Stack<GridPosition> history)
        {
            GridPosition current = history.Peek();
            MazeToken token = Grid[current.Row, current.Col];
            RuleSet rules = new RuleSet();
            RelativeMovementRule movementRule = new RelativeMovementRule();
            Stack<GridPosition> popped = new Stack<GridPosition>();
            while (token.Properties[0].Equals("*"))
            {
                GridPosition prev = history.Pop();
                popped.Push(prev);
                token = Grid[prev.Row, prev.Col];
            }
            movementRule.AddMovementOption(new MoveDescriptor() { Down = int.Parse(token.Properties[0]), Right = 0}, MirrorOptions.Horizontal);
            movementRule.AddMovementOption(new MoveDescriptor() { Down = 0, Right = int.Parse(token.Properties[0])}, MirrorOptions.Vertical);
            rules.MovementRules.Add(movementRule);
            while (popped.Count > 0)
            {
                history.Push(popped.Pop());
            }
            return rules;
        }
    }
}
