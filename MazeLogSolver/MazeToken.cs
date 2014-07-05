using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MazeLogSolver
{
    class MazeToken
    {
        private List<string> properties;

        public List<string> Properties { get { return properties; } }

        public MazeToken(params string[] props)
        {
            properties = props.ToList();
        }

        public override bool Equals(object obj)
        {
            if (!(obj is MazeToken))
            {
                return false;
            }
            for (int i = 0; i < properties.Count; ++i)
            {
                if (!properties[i].Equals((obj as MazeToken).Properties[i]))
                {
                    return false;
                }
            }
            return true;
        }
        public override int GetHashCode()
        {
            return string.Concat(Properties).GetHashCode();
        }
    }
}
