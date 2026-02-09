using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hra_Othello.Model
{
    internal class Cell
    {
        public int Row { get; set; }
        public int Column { get; set; }
        public bool IsBlack {  get; set; }

        /// <summary>
        /// Vlastnost určující, jestli daná buňka přísluší nějakému hráči
        /// </summary>
        public bool Colored { get; set; }
    }
}
