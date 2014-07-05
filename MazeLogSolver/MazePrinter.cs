using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MazeLogSolver
{
    /// <summary>
    /// A class that assists printing a maze to the console
    /// </summary>
    class MazePrinter
    {
        private const char SPACE = ' ';
        private AbstractMaze maze;
        private int hPadding = 1, vPadding = 0, width, height;

        /// <summary>
        /// The maze that will be printed
        /// </summary>
        public AbstractMaze Maze { get { return maze; } }

        /// <summary>
        /// The minimum width of the printed maze
        /// </summary>
        public int MinWidth { get; set; }

        /// <summary>
        /// The minimum height of the printed maze
        /// </summary>
        public int MinHeight { get; set; }

        /// <summary>
        /// Amount of horizontal padding around each token
        /// </summary>
        public int HPadding { get { return hPadding; } set { hPadding = value; } }

        /// <summary>
        /// Amount of vertical padding around each token
        /// </summary>
        public int VPadding { get { return vPadding; } set { vPadding = value; } }

        /// <summary>
        /// Width of a single cell
        /// </summary>
        public int Width { get { return width; } }

        // Height of a single cell
        public int Height { get { return height; } }

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="m"></param>
        public MazePrinter(AbstractMaze m)
        {
            maze = m;
        }

        /// <summary>
        /// Prints the maze to the console
        /// </summary>
        public void Print()
        {
            SetDimensions();
            for (int i = 0; i < maze.Grid.RowCount; ++i)
            {
                Console.Write(rowLine());
                Console.Write(string.Concat(Enumerable.Repeat(rowLine(SPACE, '|'), vPadding)));

                for (int j = 0; j < maze.Grid.ColCount; ++j)
                {
                    string leftPad = string.Concat(Enumerable.Repeat(SPACE, width - maze.TokenPrintMapping.GetOutput(maze.Grid[i, j]).Item1.Length - hPadding));
                    Console.Write("|" + leftPad);
                    Console.ForegroundColor = maze.TokenPrintMapping.GetOutput(maze.Grid[i, j]).Item2;
                    Console.Write(maze.TokenPrintMapping.GetOutput(maze.Grid[i, j]).Item1);
                    Console.ResetColor();
                    Console.Write(string.Concat(Enumerable.Repeat(SPACE, hPadding)));
                }
                Console.Write("|\n");

                Console.Write(string.Concat(Enumerable.Repeat(rowLine(SPACE, '|'), vPadding)));
            }
            Console.Write(rowLine());
            Console.ResetColor();
        }

        private string rowLine(char borderChar = '-', char boundaryChar = '+')
        {
            return string.Concat(Enumerable.Repeat(boundaryChar + string.Concat(Enumerable.Repeat(borderChar, Width)), maze.Grid.ColCount)) + boundaryChar + "\n";
        }

        private void SetDimensions()
        {
            width = Math.Max(maze.TokenPrintMapping.GetMaxWidth() + hPadding * 2, MinWidth);
            height = Math.Max(maze.TokenPrintMapping.GetMaxHeight() + vPadding * 2, MinHeight);
        }
    }
}
