using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MazeLogSolver
{
    class Program
    {
        /// <summary>
        /// Entry point. Constructs a maze, solves it, and prints it.
        /// </summary>
        /// <param name="args"></param>
        static void Main(string[] args)
        {
            //string mazeStr = GetMaze();
            AbstractMaze maze = new Jul2014Maze();
            MazePrinter mp = new MazePrinter(maze);
            mp.HPadding = 2;
            mp.VPadding = 1;
            mp.Print();
            Stopwatch sw = Stopwatch.StartNew();
            List<GridPosition> solution = maze.FindSolution();
            sw.Stop();
            Console.WriteLine("Found solution with {0} moves in {1} seconds.", solution.Count, (sw.ElapsedMilliseconds) / 1000.0);
            foreach (GridPosition pos in solution)
            {
                Console.Write(pos + " ");
            }
            Console.ReadKey();
        }

        /// <summary>
        /// Gets a maze from the user. Obsolete. Consider deleting.
        /// </summary>
        /// <returns></returns>
        static string GetMaze()
        {
            StringBuilder sb = new StringBuilder();
            int ch;
            while ((ch = Console.Read()) != -1)
            {
                sb.Append(Convert.ToChar(ch));
            }
            return sb.ToString();
        }
    }
}
