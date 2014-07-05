using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MazeLogSolver
{
    /// <summary>
    /// Describes a class of rules that are based on movement
    /// e.g. move direction, number of squares
    /// </summary>
    abstract class MovementRule : AbstractRule
    {
        /// <summary>
        /// Gets a list of moves that can potentially satisfy this rule. MoveDescriptor is position-agnostic.
        /// </summary>
        /// <param name="maze">The maze</param>
        /// <returns>List of MoveDescriptor</returns>
        public abstract List<MoveDescriptor> SatisfyingMoves(AbstractMaze maze);
    }
}
