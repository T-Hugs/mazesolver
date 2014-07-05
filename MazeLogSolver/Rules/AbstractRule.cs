using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MazeLogSolver
{
    abstract class AbstractRule
    {
        public abstract bool RuleSatisfied(Position from, Position to, AbstractMaze maze);
    }
}
