using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;

namespace Hra_Othello.Model
{
    internal class Cell
    {
        public int Row { get; set; }
        public int Column { get; set; }

        public Brush CellBrush { get; set; }
    }
}
