using GameOthello.Model;
using GameOthello.MVVM;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;

namespace GameOthello.ViewModel
{
    internal class CellViewModel : ViewModelBase
    {
        private readonly Cell _model;

        public CellViewModel(Cell model)
        {
            _model = model;
        }

        public int Column { get; set; }
        public int Row { get; set; }

        private CellState _state;

        public CellState State
        {
            get => _model.State;
            set 
            { 
                if (_model.State != value)
                {
                    _model.State = value;
                    OnPropertyChanged();
                }
            }
        }
    }
}
