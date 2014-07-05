using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MazeLogSolver
{
    /// <summary>
    /// Describes how to print out various tokens
    /// </summary>
    class TokenPrintMapping
    {
        private Dictionary<MazeToken, Tuple<string, ConsoleColor>> tokenMap;

        private static Dictionary<string, ConsoleColor> standardColorMap;

        /// <summary>
        /// Index of the property that represents that color of the token. -1 for none. Standard values:
        /// g = green
        /// o = orange
        /// k = pink
        /// b = blue
        /// p = purple
        /// y = yellow
        /// </summary>
        public int ColorPropertyIndex
        {
            get;
            set;
        }

        static TokenPrintMapping()
        {
            standardColorMap = new Dictionary<string, ConsoleColor>();
            standardColorMap.Add("g", ConsoleColor.Green);
            standardColorMap.Add("o", ConsoleColor.Gray);
            standardColorMap.Add("k", ConsoleColor.Magenta);
            standardColorMap.Add("b", ConsoleColor.Blue);
            standardColorMap.Add("p", ConsoleColor.DarkMagenta);
            standardColorMap.Add("y", ConsoleColor.Yellow);
        }

        /// <summary>
        /// Constructor
        /// </summary>
        public TokenPrintMapping()
        {
            tokenMap = new Dictionary<MazeToken, Tuple<string, ConsoleColor>>();
            ColorPropertyIndex = -1;
        }

        /// <summary>
        /// Specify how to print a specific token
        /// </summary>
        /// <param name="token">The token to specify</param>
        /// <param name="print">Its string represntation</param>
        /// <param name="color">The color to print the token in</param>
        public void MapTokenTo(MazeToken token, string print, ConsoleColor color = ConsoleColor.White)
        {
            tokenMap[token] = new Tuple<string, ConsoleColor>(print, color);
        }

        /// <summary>
        /// Gets the string represntation and console color for the given token
        /// </summary>
        /// <param name="token"></param>
        /// <returns></returns>
        public Tuple<string, ConsoleColor> GetOutput(MazeToken token)
        {
            // If the token is in the map, use that
            if (tokenMap.ContainsKey(token))
            {
                return tokenMap[token];
            }
            else
            {
                // If there is a specified color property, set that. Otherwise use white.
                ConsoleColor color = ConsoleColor.White;
                if (ColorPropertyIndex > -1)
                {
                    color = standardColorMap[token.Properties[ColorPropertyIndex]];
                }
                return new Tuple<string, ConsoleColor>(string.Join(",", token.Properties.Where((text, index) => index != ColorPropertyIndex)), color);
            }
        }

        /// <summary>
        /// Get the maximum width of all string representations of tokens.
        /// </summary>
        /// <returns></returns>
        public int GetMaxWidth()
        {
            // Todo: proper accommodation of non-mapped tokens
            return tokenMap.Count > 0 ? tokenMap.Values.Max(s => s.Item1.Length) : 1;
        }

        /// <summary>
        /// Get the maximum height of all string representations of tokens.
        /// </summary>
        /// <returns></returns>
        public int GetMaxHeight()
        {
            // Todo: proper accommodation of non-mapped tokens
            return tokenMap.Count > 0 ? tokenMap.Values.Max(s => s.Item1.Split('\n').Length) : 1;
        }
    }
}
