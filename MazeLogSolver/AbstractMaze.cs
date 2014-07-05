using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MazeLogSolver
{
    /// <summary>
    /// Describes a generic maze
    /// </summary>
    abstract class AbstractMaze
    {
        private const int MAX_SOLUTION_LENGTH = 100;
        private int shortestSolution = MAX_SOLUTION_LENGTH;
        private string mazeText;
        private GridPosition startPosition;
        private GridPosition endPosition;
        private HashSet<GridPosition> visited;

        private Grid<MazeToken> grid;

        /// <summary>
        /// The Grid containing the maze tokens for this maze
        /// </summary>
        public Grid<MazeToken> Grid { get { return grid; } }

        /// <summary>
        /// The starting position of the maze
        /// </summary>
        public GridPosition StartPosition { get { return startPosition; } }

        /// <summary>
        /// The end/final position of the maze that designates solution
        /// </summary>
        public GridPosition EndPosition { get { return endPosition; } }
        
        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="maze">Text that describes the maze</param>
        /// <param name="start">Starting position</param>
        /// <param name="end">Ending position</param>
        protected AbstractMaze(string maze, Position start, Position end)
        {
            visited = new HashSet<GridPosition>();
            mazeText = maze;
            InitializeGrid();
            startPosition = start.ToGridPosition(grid);
            visited.Add(startPosition);
            endPosition = end.ToGridPosition(grid);
        }

        /// <summary>
        /// Finds a solution to the maze
        /// </summary>
        /// <returns>List of positions starting at StartPosition, making legal moves until EndPosition</returns>
        public List<GridPosition> FindSolution()
        {
            Stack<GridPosition> history = new Stack<GridPosition>();
            history.Push(startPosition);
            List<GridPosition> solution = FindSolutionHelper(history, new List<GridPosition>());
            solution.Reverse();
            return solution;
        }

        private List<GridPosition> FindSolutionHelper(Stack<GridPosition> history, List<GridPosition> currentBestSolution)
        {
            // If we're at the end position, make note of the solution if it's shorter than the current best solution
            if (history.Peek().Equals(endPosition))
            {
                if (history.Count < shortestSolution) 
                {
                    shortestSolution = history.Count;
                    currentBestSolution = history.ToList();
                    return currentBestSolution;
                }
            }
            // Get the list of valid moves given the current history, and iterate on each to exhaustively search all possibilities
            List<GridPosition> validMoves = GetValidMoves(history);
            foreach (GridPosition move in validMoves)
            {
                // Ignore this move if we've already found a better solution, or if we've already been here
                // Todo: better way to track previous states
                if (history.Count + 1 > shortestSolution || (visited.Contains(move)/* && Grid[move.Row, move.Col].Properties[0] != "*"*/))
                {
                    continue;
                }
                history.Push(move);
                visited.Add(move);

                // Recursive step: find the solution based on this move
                List<GridPosition> solution = FindSolutionHelper(history, currentBestSolution);
                if (solution != null)
                {
                    currentBestSolution = solution;
                }
                visited.Remove(move);
                history.Pop();
            }
            return currentBestSolution;
        }

        /// <summary>
        /// Create the grid based on the string representation of the maze
        /// </summary>
        private void InitializeGrid()
        {
            string[] rows = mazeText.Split(new string[] {"\r\n", "\n"}, StringSplitOptions.None);
            grid = new Grid<MazeToken>(rows.Length, rows[0].Split(',').Length);
            int currentRow = 0;
            foreach (string row in rows)
            {
                string[] cells = row.Split(',');
                int currentCol = 0;
                foreach (string cell in cells)
                {
                    MazeToken token = new MazeToken(cell.Split(' '));
                    grid[currentRow, currentCol] = token;
                    currentCol++;
                }
                currentRow++;
            }
        }

        /// <summary>
        /// Gets all legal moves from the current position (given as the top item in history)
        /// </summary>
        /// <param name="history">Stack containing all moves up to this point</param>
        /// <returns>List of valid next positions</returns>
        private List<GridPosition> GetValidMoves(Stack<GridPosition> history)
        {
            GridPosition fromPosition = history.Peek();
            RuleSet rules = GetRules(history);
            
            HashSet<MoveDescriptor> initialPossibilities = new HashSet<MoveDescriptor>();

            // Add movement rules first because they initially define a subset of valid spaces
            foreach (MovementRule rule in rules.MovementRules)
            {
                initialPossibilities.UnionWith(rule.SatisfyingMoves(this));
            }
            List<GridPosition> moves = new List<GridPosition>();
            foreach (MoveDescriptor possibility in initialPossibilities)
            {
                bool cont = false;

                // Ignore moves that are out of bounds
                if (!fromPosition.CanMove(possibility))
                {
                    continue;
                }
                foreach (AbstractRule rule in rules.OtherRules)
                {
                    if (!rule.RuleSatisfied(fromPosition, fromPosition.Move(possibility), this))
                    {
                        cont = true;
                        break;
                    }
                }
                if (cont)
                {
                    continue;
                }
                moves.Add(fromPosition.Move(possibility));
            }
            return moves;
        }

        /// <summary>
        /// Gets the mapping from tokens to strings/colors for this maze
        /// </summary>
        public virtual TokenPrintMapping TokenPrintMapping { get { return new TokenPrintMapping(); } }

        /// <summary>
        /// Gets the rules for next move (given the history)
        /// </summary>
        /// <param name="history">History of moves up to this point</param>
        /// <returns>RuleSet of rules that describe next legal moves</returns>
        protected abstract RuleSet GetRules(Stack<GridPosition> history);
    }
}
