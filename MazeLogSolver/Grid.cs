using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MazeLogSolver
{
    /// <summary>
    /// Describes an abstract grid.
    /// Todo: consider making this an interface
    /// </summary>
    abstract class AbstractGrid
    {
        private int rowCount;
        private int colCount;
        private GridWrapOption gridWrapOption;

        /// <summary>
        /// Number of rows in the grid
        /// </summary>
        public int RowCount { get { return rowCount; } }

        /// <summary>
        /// Number of columns in the grid
        /// </summary>
        public int ColCount { get { return colCount; } }

        /// <summary>
        /// Describes how the grid reacts to out of bounds movements
        /// </summary>
        public GridWrapOption GridWrapOption { get { return gridWrapOption; } }

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="numRows"></param>
        /// <param name="numCols"></param>
        /// <param name="wrapOption"></param>
        protected AbstractGrid(int numRows, int numCols, GridWrapOption wrapOption = GridWrapOption.Throw)
        {
            rowCount = numRows;
            colCount = numCols;
            gridWrapOption = wrapOption;
        }
    }

    /// <summary>
    /// Describes a concrete grid, where each cell contains the genericized type
    /// </summary>
    /// <typeparam name="T">Type of objects contained in this grid</typeparam>
    class Grid<T> : AbstractGrid, IEnumerable<T>
    {

        private T[][] grid;

        /// <summary>
        /// Constructor - initializes the entire grid in memory
        /// </summary>
        /// <param name="numRows"></param>
        /// <param name="numCols"></param>
        /// <param name="wrapOption"></param>
        public Grid(int numRows, int numCols, GridWrapOption wrapOption = GridWrapOption.Throw)
            : base(numRows, numCols, wrapOption)
        {
            grid = new T[numRows][];
            for (int i = 0; i < numRows; ++i)
            {
                grid[i] = new T[numCols];
            }
        }

        /// <summary>
        /// Extracts the item at the given row, column
        /// </summary>
        /// <param name="row"></param>
        /// <param name="col"></param>
        /// <returns>The object at the given row, column</returns>
        public T this[int row, int col]
        {
            get
            {
                return grid[row][col];
            }
            set
            {
                grid[row][col] = value;
            }
        }

        /// <summary>
        /// Gets an enumerator that enumerates items in the grid, first left to right, then top to bottom
        /// </summary>
        /// <returns></returns>
        public IEnumerator<T> GetEnumerator()
        {
            foreach (T[] row in grid)
            {
                foreach (T cell in row)
                {
                    yield return cell;
                }
            }
        }

        /// <summary>
        /// Gets an enumerator that enumerates items in the grid, first left to right, then top to bottom
        /// </summary>
        /// <returns></returns>
        System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }

    /// <summary>
    /// Describes the behavior of moving out of bounds
    /// </summary>
    enum GridWrapOption
    {
        /// <summary>
        /// Wrap around (moving off the right edge takes you back to the left edge)
        /// </summary>
        Wrap,

        /// <summary>
        /// Stay at the edge (ignore extra movement past the edge)
        /// </summary>
        Stay,

        /// <summary>
        /// Raise an exception if moving out of bounds happens
        /// </summary>
        Throw
    }
}
