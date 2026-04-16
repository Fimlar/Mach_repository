using KlepniKrtka.Model;
using KlepniKrtka.MVVM;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KlepniKrtka.ViewModel
{
    internal class CellViewModel : ViewModelBase
    {
        private Cell _model;
        public CellViewModel(Cell model)
        {
            _model = model;
        }

        // Nastavuji, aby se při volání vrátily hodnoty z modelu a spustilo se OnPropertyChanged
		public int Row
		{
			get => _model.Row;
			set { _model.Row = value; OnPropertyChanged(); }
		}
        public int Column
        {
            get => _model.Column;
            set {  _model.Column = value; OnPropertyChanged(); }
        }

        public bool IsMole
        {
            get => _model.IsMole;
            set { _model.IsMole = value; OnPropertyChanged(); }
        }
	}
}
