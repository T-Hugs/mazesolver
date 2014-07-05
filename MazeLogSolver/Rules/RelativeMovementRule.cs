using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MazeLogSolver
{
    class RelativeMovementRule : MovementRule
    {
        private List<MoveDescriptor> RelativeMovementOptions { get; set; }

        public RelativeMovementRule() 
            : base()
        {
            RelativeMovementOptions = new List<MoveDescriptor>();
        }

        public void AddMovementOption(MoveDescriptor movement, MirrorOptions mirror = MirrorOptions.None)
        {
            RelativeMovementOptions.Add(movement);
            if ((mirror & MirrorOptions.Horizontal) == MirrorOptions.Horizontal)
            {
                RelativeMovementOptions.Add(new MoveDescriptor() { Down = -movement.Down, Right = movement.Right });
            }
            if ((mirror & MirrorOptions.Vertical) == MirrorOptions.Vertical)
            {
                RelativeMovementOptions.Add(new MoveDescriptor() { Down = movement.Down, Right = -movement.Right });
            }
            if ((mirror & MirrorOptions.Origin) == MirrorOptions.Origin)
            {
                RelativeMovementOptions.Add(new MoveDescriptor() { Down = -movement.Down, Right = -movement.Right });
            }
        }

        public override bool RuleSatisfied(Position from, Position to, AbstractMaze maze = null)
        {
            foreach (MoveDescriptor option in RelativeMovementOptions)
            {
                if (from.Move(option).Equals(to))
                {
                    return true;
                }
            }
            return false;
        }

        public override List<MoveDescriptor> SatisfyingMoves(AbstractMaze maze)
        {
            return RelativeMovementOptions;
        }
    }

    [Flags]
    enum MirrorOptions
    {
        None = 0,
        Vertical = 1, // axis
        Horizontal = 2, // axis
        Origin = 4,
        All = 7
    }
}
