using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MazeLogSolver
{
    /// <summary>
    /// Describes a rule that allows the player to move from one square to another
    /// </summary>
    abstract class AbstractRule
    {
        /// <summary>
        /// Determines if the given move is valid in the maze
        /// </summary>
        /// <param name="from">Starting position</param>
        /// <param name="to">Position after the proposed move</param>
        /// <param name="maze">The maze</param>
        /// <returns>True if the move from [from] to [to] is legal.</returns>
        public abstract bool RuleSatisfied(Position from, Position to, AbstractMaze maze);

        /// <summary>
        /// Determines if the given move is valid in the maze
        /// </summary>
        /// <param name="from">Starting position</param>
        /// <param name="move">MoveDescriptor that describes the move</param>
        /// <param name="maze">The maze</param>
        /// <returns>True if the move from [from] to the position described by [move] is legal.</returns>
        public sealed bool RuleSatisfied(Position from, MoveDescriptor move, AbstractMaze maze)
        {
            return RuleSatisfied(from, from.Move(move), maze);
        }
    }
}
