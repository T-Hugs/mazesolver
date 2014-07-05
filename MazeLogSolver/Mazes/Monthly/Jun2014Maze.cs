using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MazeLogSolver
{
    class Jun2014Maze : AbstractMaze
    {
        public const string MazeString = @"r 3,b 4,r 4,b 5,b 5,b 6,b 6,b 6
b 3,b 4,b 4,r 6,b 7,r 4,r 5,r 7
b 1,r 3,b 2,r 7,b 5,b 3,r 6,r 5
r 2,b 7,r 4,b 3,r 1,r 7,r 4,r 6
r 5,b 3,b 7,r 6,b 2,r 4,b 5,b 6
b 4,r 4,b 8,b 4,b 3,r 3,b 8,b 8
b 3,r 3,r 2,r 8,r 5,r 8,r 3,b 8
b 2,b 1,r 1,b 1,r 8,r 3,r 2,p 0";

        private static TokenPrintMapping printMapping;

        public override TokenPrintMapping TokenPrintMapping
        {
            get { return printMapping; }
        }

        static Jun2014Maze()
        {
            printMapping = new TokenPrintMapping();
            printMapping.MapTokenTo(new MazeToken("r", "1"), "^ ", ConsoleColor.Red);
            printMapping.MapTokenTo(new MazeToken("r", "2"), "/'", ConsoleColor.Red);
            printMapping.MapTokenTo(new MazeToken("r", "3"), "->", ConsoleColor.Red);
            printMapping.MapTokenTo(new MazeToken("r", "4"), "\\,", ConsoleColor.Red);
            printMapping.MapTokenTo(new MazeToken("r", "5"), "v ", ConsoleColor.Red);
            printMapping.MapTokenTo(new MazeToken("r", "6"), ",/", ConsoleColor.Red);
            printMapping.MapTokenTo(new MazeToken("r", "7"), "<-", ConsoleColor.Red);
            printMapping.MapTokenTo(new MazeToken("r", "8"), "'\\", ConsoleColor.Red);
            printMapping.MapTokenTo(new MazeToken("b", "1"), "^ ", ConsoleColor.Cyan);
            printMapping.MapTokenTo(new MazeToken("b", "2"), "/'", ConsoleColor.Cyan);
            printMapping.MapTokenTo(new MazeToken("b", "3"), "->", ConsoleColor.Cyan);
            printMapping.MapTokenTo(new MazeToken("b", "4"), "\\,", ConsoleColor.Cyan);
            printMapping.MapTokenTo(new MazeToken("b", "5"), "v ", ConsoleColor.Cyan);
            printMapping.MapTokenTo(new MazeToken("b", "6"), ",/", ConsoleColor.Cyan);
            printMapping.MapTokenTo(new MazeToken("b", "7"), "<-", ConsoleColor.Cyan);
            printMapping.MapTokenTo(new MazeToken("b", "8"), "'\\", ConsoleColor.Cyan);
            printMapping.MapTokenTo(new MazeToken("p", "0"), "0 ", ConsoleColor.DarkMagenta);
        }

        public Jun2014Maze(string maze = null)
            : base(maze == null ? MazeString : maze, new Position(0, 0), new Position(7, 7))
        {
            
        }

        /// <summary>
        /// Called once per movement
        /// </summary>
        /// <param name="history"></param>
        /// <returns></returns>
        protected override RuleSet GetRules(Stack<GridPosition> history)
        {
            GridPosition current = history.Peek();

            // Must alternate colors (color property does not match)
            TokenRule tr = new TokenRule()
            {
                PropertiesToCheck = new[] { 0 },
                MinPropertiesMatch = 0,
                MaxPropertiesMatch = 0
            };

            // Must move in the direction of the arrow
            DirectionMovementRule dmr = new DirectionMovementRule();
            dmr.SetFromNumber(int.Parse(Grid[current.Row, current.Col].Properties[1]));

            RuleSet rules = new RuleSet();
            rules.MovementRules.Add(dmr);
            rules.OtherRules.Add(tr);
            return rules;
        }
    }
}
