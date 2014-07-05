using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MazeLogSolver
{
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
        
        public TokenPrintMapping()
        {
            tokenMap = new Dictionary<MazeToken, Tuple<string, ConsoleColor>>();
            ColorPropertyIndex = -1;
        }

        public void MapTokenTo(MazeToken token, string print, ConsoleColor color = ConsoleColor.White)
        {
            tokenMap[token] = new Tuple<string, ConsoleColor>(print, color);
        }

        public Tuple<string, ConsoleColor> GetOutput(MazeToken token)
        {
            if (tokenMap.ContainsKey(token))
            {
                return tokenMap[token];
            }
            else
            {
                ConsoleColor color = ConsoleColor.White;
                if (ColorPropertyIndex > -1)
                {
                    color = standardColorMap[token.Properties[ColorPropertyIndex]];
                }
                return new Tuple<string, ConsoleColor>(string.Join(",", token.Properties.Where((text, index) => index != ColorPropertyIndex)), color);
            }
        }

        public int GetMaxWidth()
        {
            return tokenMap.Count > 0 ? tokenMap.Values.Max(s => s.Item1.Length) : 1;
        }
        public int GetMaxHeight()
        {
            return tokenMap.Count > 0 ? tokenMap.Values.Max(s => s.Item1.Split('\n').Length) : 1;
        }
    }
}
