using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameOthello.Model
{
    internal class Cell
    {
        public int Column { get; set; }
        public int Row { get; set; }
        public CellState State { get; set; }
    }
}
