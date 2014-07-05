using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MazeLogSolver
{
    /// <summary>
    /// Describes a position-agnostic move
    /// </summary>
    class MoveDescriptor
    {
        /// <summary>
        /// Number of squares to move to the right (negative for left movement)
        /// </summary>
        public int Right { get; set; }

        /// <summary>
        /// Number of squares to move down (negative for upward movement)
        /// </summary>
        public int Down { get; set; }

        /// <summary>
        /// Two move descriptors are equal if the describe the same movement
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public override bool Equals(object obj)
        {
            if (!(obj is MoveDescriptor))
            {
                return false;
            }
            return (obj as MoveDescriptor).Right == Right && (obj as MoveDescriptor).Down == Down;
        }

        /// <summary>
        /// Returns the hash code of the string obtained by concatenating the right and down movement amounts
        /// </summary>
        /// <returns></returns>
        public override int GetHashCode()
        {
            return (Right.ToString() + " " + Down.ToString()).GetHashCode();
        }
    }
}
