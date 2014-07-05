using MazeLogSolver.Rules.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MazeLogSolver
{
    class Jul2014Maze : AbstractMaze
    {
        public const string MazeString = @"g X,o →,o #,k #,b X,o O
p +,b #,b =,o =,o →,b X
b #,p =,y X,y +,g #,p =
p #,k =,b X,g X,g →,k X
b X,p →,o +,k +,g X,p O
k +,g #,g →,o O,k O,b #";

        private static TokenPrintMapping printMapping;

        public override TokenPrintMapping TokenPrintMapping
        {
            get { return printMapping; }
        }

        static Jul2014Maze()
        {
            printMapping = new TokenPrintMapping()
            {
                ColorPropertyIndex = 0
            };
        }

        public Jul2014Maze(string maze = null)
            : base(maze == null ? MazeString : maze, new Position(0, 0), new Position(5, 5))
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

            int propertyToMatchIndex = history.Count % 2;

            // Must alternate between matching color and matching shape
            TokenRule tr = new TokenRule()
            {
                PropertiesToCheck = new[] { propertyToMatchIndex },
                MinPropertiesMatch = 1,
                MaxPropertiesMatch = 1
            };

            RuleSet rules = new RuleSet();
            rules.MovementRules.Add(DirectionRules.RanksAndFiles);
            rules.OtherRules.Add(tr);
            return rules;
        }
    }
}
