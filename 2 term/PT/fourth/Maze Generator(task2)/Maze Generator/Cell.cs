using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Maze_Generator
{
    public class Cell
    {
        public bool Up { get; set; }
        public bool Right { get; set; }
        public bool Down { get; set; }
        public bool Left { get; set; }

        public Cell()
        {
            Up = false;
            Right = false;
            Down = false;
            Left = false;
        }
    }
}
