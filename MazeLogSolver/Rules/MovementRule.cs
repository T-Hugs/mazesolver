using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MazeLogSolver
{
    abstract class MovementRule : AbstractRule
    {
        public abstract List<MoveDescriptor> SatisfyingMoves(AbstractMaze maze);
    }
}
