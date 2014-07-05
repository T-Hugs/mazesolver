using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MazeLogSolver
{
    /// <summary>
    /// Describes a token in the maze grid. I.e., each grid position contains a token.
    /// </summary>
    class MazeToken
    {
        private List<string> properties;

        /// <summary>
        /// Properties that define the token
        /// </summary>
        public List<string> Properties { get { return properties; } }

        /// <summary>
        /// Constructor for MazeToken with the given properties
        /// </summary>
        /// <param name="props"></param>
        public MazeToken(params string[] props)
        {
            properties = props.ToList();
        }

        /// <summary>
        /// Two tokens are equall iff their properties are all equal
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
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

        /// <summary>
        /// Returns the hashcode of the string obtained by concatenating all the properties.
        /// </summary>
        /// <returns></returns>
        public override int GetHashCode()
        {
            return string.Concat(Properties).GetHashCode();
        }
    }
}
