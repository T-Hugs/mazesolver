using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MazeLogSolver
{
    class Program
    {
        static void Main(string[] args)
        {
            //string mazeStr = GetMaze();
            AbstractMaze maze = new Jul2014Maze();
            MazePrinter mp = new MazePrinter(maze);
            mp.HPadding = 2;
            mp.VPadding = 1;
            mp.Print();
            List<GridPosition> solution = maze.FindSolution();
            foreach (GridPosition pos in solution)
            {
                Console.Write(pos + " ");
            }
            Console.ReadKey();
        }

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
