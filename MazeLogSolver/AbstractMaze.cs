using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MazeLogSolver
{
    abstract class AbstractMaze
    {
        private const int MAX_SOLUTION_LENGTH = 100;
        private int shortestSolution = MAX_SOLUTION_LENGTH;
        private string mazeText;
        private GridPosition startPosition;
        private GridPosition endPosition;
        private HashSet<GridPosition> visited;

        private Grid<MazeToken> grid;

        public Grid<MazeToken> Grid { get { return grid; } }
        public GridPosition StartPosition { get { return startPosition; } }
        public GridPosition EndPosition { get { return endPosition; } }
        

        public AbstractMaze(string maze, Position start, Position end)
        {
            visited = new HashSet<GridPosition>();
            mazeText = maze;
            InitializeGrid();
            startPosition = start.ToGridPosition(grid);
            visited.Add(startPosition);
            endPosition = end.ToGridPosition(grid);
        }

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
            if (history.Peek().Equals(endPosition))
            {
                if (history.Count < shortestSolution) 
                {
                    shortestSolution = history.Count;
                    currentBestSolution = history.ToList();
                    return currentBestSolution;
                }
            }
            List<GridPosition> validMoves = GetValidMoves(history);
            foreach (GridPosition move in validMoves)
            {
                if (history.Count + 1 > shortestSolution || (visited.Contains(move)/* && Grid[move.Row, move.Col].Properties[0] != "*"*/))
                {
                    continue;
                }
                history.Push(move);
                visited.Add(move);
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

        private List<GridPosition> GetValidMoves(Stack<GridPosition> history)
        {
            GridPosition fromPosition = history.Peek();
            MazeToken currentToken = grid[fromPosition.Row, fromPosition.Col];
            RuleSet rules = GetRules(history);
            
            HashSet<MoveDescriptor> initialPossibilities = new HashSet<MoveDescriptor>();
            foreach (MovementRule rule in rules.MovementRules)
            {
                initialPossibilities.UnionWith(rule.SatisfyingMoves(this));
            }
            List<GridPosition> moves = new List<GridPosition>();
            foreach (MoveDescriptor possibility in initialPossibilities)
            {
                bool cont = false;
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

        public virtual TokenPrintMapping TokenPrintMapping { get { return new TokenPrintMapping(); } }

        protected abstract RuleSet GetRules(Stack<GridPosition> history);
    }
}
