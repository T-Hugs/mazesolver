using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MazeLogSolver
{
    class ApolloMaze : AbstractMaze
    {
        public const string MazeString = @"b 3 -,r 5 -,r 6 -,r 6 -,r 2 o
r 8 -,r 3 -,b 3 -,r 5 -,b 6 o
b 4 -,b 1 -,b 4 -,r 3 -,b 5 -
r 7 o,r 7 -,r 8 -,b 7 -,r 4 -
r 2 -,r 6 -,r 8 -,b 5 -,p 0 -";

        private static TokenPrintMapping printMapping;

        public override TokenPrintMapping TokenPrintMapping
        {
            get { return printMapping; }
        }

        static ApolloMaze()
        {
            printMapping = new TokenPrintMapping();
            printMapping.MapTokenTo(new MazeToken("r", "1", "-"), "^  ", ConsoleColor.Red);
            printMapping.MapTokenTo(new MazeToken("r", "2", "-"), "/' ", ConsoleColor.Red);
            printMapping.MapTokenTo(new MazeToken("r", "3", "-"), "-> ", ConsoleColor.Red);
            printMapping.MapTokenTo(new MazeToken("r", "4", "-"), "\\, ", ConsoleColor.Red);
            printMapping.MapTokenTo(new MazeToken("r", "5", "-"), "v  ", ConsoleColor.Red);
            printMapping.MapTokenTo(new MazeToken("r", "6", "-"), ",/ ", ConsoleColor.Red);
            printMapping.MapTokenTo(new MazeToken("r", "7", "-"), "<- ", ConsoleColor.Red);
            printMapping.MapTokenTo(new MazeToken("r", "8", "-"), "'\\ ", ConsoleColor.Red);
            printMapping.MapTokenTo(new MazeToken("b", "1", "-"), "^  ", ConsoleColor.Cyan);
            printMapping.MapTokenTo(new MazeToken("b", "2", "-"), "/' ", ConsoleColor.Cyan);
            printMapping.MapTokenTo(new MazeToken("b", "3", "-"), "-> ", ConsoleColor.Cyan);
            printMapping.MapTokenTo(new MazeToken("b", "4", "-"), "\\, ", ConsoleColor.Cyan);
            printMapping.MapTokenTo(new MazeToken("b", "5", "-"), "v  ", ConsoleColor.Cyan);
            printMapping.MapTokenTo(new MazeToken("b", "6", "-"), ",/ ", ConsoleColor.Cyan);
            printMapping.MapTokenTo(new MazeToken("b", "7", "-"), "<- ", ConsoleColor.Cyan);
            printMapping.MapTokenTo(new MazeToken("b", "8", "-"), "'\\ ", ConsoleColor.Cyan);

            printMapping.MapTokenTo(new MazeToken("r", "1", "o"), "^ *", ConsoleColor.Red);
            printMapping.MapTokenTo(new MazeToken("r", "2", "o"), "/'*", ConsoleColor.Red);
            printMapping.MapTokenTo(new MazeToken("r", "3", "o"), "->*", ConsoleColor.Red);
            printMapping.MapTokenTo(new MazeToken("r", "4", "o"), "\\,*", ConsoleColor.Red);
            printMapping.MapTokenTo(new MazeToken("r", "5", "o"), "v *", ConsoleColor.Red);
            printMapping.MapTokenTo(new MazeToken("r", "6", "o"), ",/*", ConsoleColor.Red);
            printMapping.MapTokenTo(new MazeToken("r", "7", "o"), "<-*", ConsoleColor.Red);
            printMapping.MapTokenTo(new MazeToken("r", "8", "o"), "'\\*", ConsoleColor.Red);
            printMapping.MapTokenTo(new MazeToken("b", "1", "o"), "^ *", ConsoleColor.Cyan);
            printMapping.MapTokenTo(new MazeToken("b", "2", "o"), "/'*", ConsoleColor.Cyan);
            printMapping.MapTokenTo(new MazeToken("b", "3", "o"), "->*", ConsoleColor.Cyan);
            printMapping.MapTokenTo(new MazeToken("b", "4", "o"), "\\,*", ConsoleColor.Cyan);
            printMapping.MapTokenTo(new MazeToken("b", "5", "o"), "v *", ConsoleColor.Cyan);
            printMapping.MapTokenTo(new MazeToken("b", "6", "o"), ",/*", ConsoleColor.Cyan);
            printMapping.MapTokenTo(new MazeToken("b", "7", "o"), "<-*", ConsoleColor.Cyan);
            printMapping.MapTokenTo(new MazeToken("b", "8", "o"), "'\\*", ConsoleColor.Cyan);
        }

        public ApolloMaze(string maze = null)
            : base(maze == null ? MazeString : maze, new Position(0, 0), new Position(7, 7))
        {
            
        }

        protected override RuleSet GetRules(Stack<GridPosition> history)
        {
            GridPosition current = history.Peek();

            // Reverse the arrow if we have encountered an odd number of circled spots
            bool arrowPointsToNext = history.Count(p => Grid[p.Row, p.Col].Properties[2].Equals("o")) % 2 == 0;
            

            // Must alternate colors (color property does not match)
            TokenRule tr = new TokenRule()
            {
                PropertiesToCheck = new[] { 0 },
                MinPropertiesMatch = 0,
                MaxPropertiesMatch = 0
            };

            // Must move in the direction of the arrow (or tail)
            DirectionMovementRule dmr = new DirectionMovementRule();
            int direction = int.Parse(Grid[current.Row, current.Col].Properties[1]);
            direction += arrowPointsToNext ? 0 : 4;
            direction = ((direction - 1) % 8) + 1;
            dmr.SetFromNumber(direction);

            RuleSet rules = new RuleSet();
            rules.MovementRules.Add(dmr);
            rules.OtherRules.Add(tr);
            return rules;
        }
    }
}
