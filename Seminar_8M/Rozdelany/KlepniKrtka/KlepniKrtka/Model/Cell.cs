using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KlepniKrtka.Model
{
    internal class Cell
    {
        public int Row { get; set; }
        public int Column { get; set; }
        public bool IsMole { get; set; }

        public Cell(int row, int column)
        {
            Row = row;
            Column = column;
            IsMole = false;
        }
    }
}
