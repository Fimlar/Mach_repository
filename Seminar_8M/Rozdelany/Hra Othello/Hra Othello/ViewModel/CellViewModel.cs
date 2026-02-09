using Hra_Othello.Model;
using Hra_Othello.MVVM;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;

namespace Hra_Othello.ViewModel
{
    internal class CellViewModel : ViewModelBase
    {
        private Cell _model;
        public CellViewModel(Cell model)
        {
            _model = model;
        }
        public int Row { get; set; }
        public int Column {  get; set; }

        private Brush _cellBrush = Brushes.Transparent;

        public Brush CellBrush
        {
            get { return _cellBrush; }
            set { _cellBrush = value; OnPropertyChanged(); }
        }



        private bool _isBlack;
        public bool IsBlack
        {
            get { return _isBlack; }
            set { _isBlack = value; OnPropertyChanged(); }
        }


    }
}
