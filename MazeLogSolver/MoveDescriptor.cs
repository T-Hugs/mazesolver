using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MazeLogSolver
{
    class MoveDescriptor
    {
        public int Right { get; set; }

        public int Down { get; set; }

        public override bool Equals(object obj)
        {
            if (!(obj is MoveDescriptor))
            {
                return false;
            }
            return (obj as MoveDescriptor).Right == Right && (obj as MoveDescriptor).Down == Down;
        }

        public override int GetHashCode()
        {
            return (Right.ToString() + " " + Down.ToString()).GetHashCode();
        }
    }
}
