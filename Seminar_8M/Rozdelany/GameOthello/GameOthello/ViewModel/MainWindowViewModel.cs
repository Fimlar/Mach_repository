using GameOthello.Model;
using GameOthello.MVVM;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Security.Cryptography.Xml;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;

namespace GameOthello.ViewModel
{
    internal class MainWindowViewModel : ViewModelBase
    {
        /// <summary>
        /// Kontroluje jestli hra již začala
        /// </summary>
		private bool _isGameRunning;
		public bool IsGameRunning
		{
			get { return _isGameRunning; }
			set 
            { 
                _isGameRunning = value;
                OnPropertyChanged();
            }
		}

        private readonly (int dx, int dy)[] NeighborOffsets =
        {
            (-1, -1), (0, -1), (1, -1),
            (-1,  0),          (1,  0),
            (-1,  1), (0,  1), (1,  1),
        };

        /// <summary>
        /// Příkaz, který spustí metodu StartGame
        /// </summary>
        public RelayCommand StartCommand => new RelayCommand(execute => StartGame(), canExecute => !_isGameRunning);
        public RelayCommand ClickCommand => new RelayCommand(param => CellClicked((CellViewModel)param));

        /// <summary>
        /// Kolekce buněk pro zobrazení
        /// </summary>
        public ObservableCollection<CellViewModel> Cells { get; } = new ObservableCollection<CellViewModel>();

        /// <summary>
        /// Proměnná ukládající kdo je právě na řadě
        /// </summary>
        private CellState turn = CellState.FirstPlayer;
        public MainWindowViewModel()
        {
            _isGameRunning=false;
        }

        private void StartGame()
        {
            IsGameRunning = true;

            for (int y = 0; y < 8; y++)
            {
                for (int x = 0; x < 8; x++)
                {
                    Cell model = new Cell();
                    CellState state = CellState.NotFound;
                    if (y == 3 && x == 3)
                        state = CellState.SecondPlayer;
                    else if (y == 3 && x == 4)
                        state = CellState.FirstPlayer;
                    else if (y == 4 && x == 4)
                        state = CellState.SecondPlayer;
                    else if (y == 4 && x == 3)
                        state = CellState.FirstPlayer;

                    Cells.Add(new CellViewModel(model)
                    {
                        Row = y,
                        Column = x,
                        State = state
                    });
                }
            }
            ColorPossibleMoves();
        }

        private void CellClicked(CellViewModel cell)
        {

        }

        private void ColorPossibleMoves()
        {
            foreach (var cell in Cells.Where(c => c.State == turn))
            {
                foreach (var dir in NeighborOffsets)
                {
                    int dx = dir.dx;
                    int dy = dir.dy;
                    try
                    {
                        // soused v tomto směru
                        CellViewModel nei = Cells.First(c => c.Row == cell.Row + dy && c.Column == cell.Column + dx && c.State != CellState.NotFound
                                                                                                                    && c.State != CellState.Playable
                                                                                                                    && c.State != turn);
                        // o jedno dál než soused
                        CellViewModel nxt = Cells.First(c => c.Row == cell.Row + dy * 2 && c.Column == cell.Column + dx * 2);
                    }
                    catch
                    {
                        continue;
                    }
                }
            }
        }
    }
}
