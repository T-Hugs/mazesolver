using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MazeLogSolver
{
    abstract class AbstractGrid
    {
        private int rowCount;
        private int colCount;
        private GridWrapOption gridWrapOption;

        public int RowCount { get { return rowCount; } }
        public int ColCount { get { return colCount; } }
        public GridWrapOption GridWrapOption { get { return gridWrapOption; } }

        public AbstractGrid(int numRows, int numCols, GridWrapOption wrapOption = GridWrapOption.Throw)
        {
            rowCount = numRows;
            colCount = numCols;
            gridWrapOption = wrapOption;
        }
    }
    class Grid<T> : AbstractGrid, IEnumerable<T>
    {

        private T[][] grid;

        public Grid(int numRows, int numCols, GridWrapOption wrapOption = GridWrapOption.Throw)
            : base(numRows, numCols, wrapOption)
        {
            grid = new T[numRows][];
            for (int i = 0; i < numRows; ++i)
            {
                grid[i] = new T[numCols];
            }
        }

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

        System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }
    enum GridWrapOption
    {
        Wrap,
        Stay,
        Throw
    }
}
