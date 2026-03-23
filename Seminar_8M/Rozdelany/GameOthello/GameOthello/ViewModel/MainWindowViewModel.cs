using GameOthello.Model;
using GameOthello.MVVM;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Net.Http.Headers;
using System.Security.Cryptography.Xml;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Documents;
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
        /// List cest k buňkám, které mohu zahrát
        /// </summary>
        private List<List<CellViewModel>> paths { get; set; } = new List<List<CellViewModel>>();

        /// <summary>
        /// List buněk, které mohu zahrát
        /// </summary>
        private List<CellViewModel> playables { get; set; } = new List<CellViewModel>();

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
            // Pokud hráč neklikl na buňky, která lze zahrát, rovnou se vracíme
            if (cell.State != CellState.Playable)
                return;

            // Přidáme danou buňku hráči na řadě
            cell.State = turn;
            // Zjistíme index kliknuté buňky v listu playables
            int index = playables.IndexOf(cell);
            // Zkopíruji cestu příslušící kliknuté buňce
            List<CellViewModel> currentPath = paths[index];
            
            // Všechny buňky v cestě dám momentálnímu hráči
            foreach (CellViewModel path in currentPath)
            {
                path.State = turn;
            }

            // Změním tah na druhého hráče
            switch (turn)
            {
                case CellState.FirstPlayer:
                    turn = CellState.SecondPlayer;
                    break;
                case CellState.SecondPlayer: 
                    turn = CellState.FirstPlayer;
                    break;
            }

            foreach (var random in Cells.Where(c => c.State == CellState.Playable))
            {
                random.State = CellState.NotFound;
            }

            ColorPossibleMoves();
        }
        private void ColorPossibleMoves()
        {
            paths.Clear();
            playables.Clear();
            FindPossibleMoves();
            // Nastavím všem možným buňkám stav na Playable
            foreach (CellViewModel cell in playables)
                cell.State = CellState.Playable;
        }

        private void FindPossibleMoves()
        {
            foreach (var cell in Cells.Where(c => c.State == turn))
            {
                foreach (var dir in NeighborOffsets)
                {
                    int dx = dir.dx;
                    int dy = dir.dy;
                    // Podívám se na první buňky v hledaném směru a zjistím, jestli je opačné barvy
                    CellViewModel? nei = Cells.FirstOrDefault(c => c.Row == cell.Row + dy && c.Column == cell.Column + dx && c.State != CellState.NotFound &&
                                                                                                                   c.State != CellState.Playable &&
                                                                                                                   c.State != turn);
                    // Pokud není vhodný soused v daném směru tak končím
                    if (nei is null)
                        continue;

                    // proměnná na posouvání ve směru
                    int i = 2;
                    // List, do kterého ukládám již projité buňky v tomto bloku
                    List<CellViewModel> temporary = new List<CellViewModel>();
                    // Přidám do cesty právě prohledanou buňku
                    temporary.Add(nei);
                    while (true)
                    {
                        // Najdu další buňku
                        nei = Cells.FirstOrDefault(c => c.Row == cell.Row + i * dy && c.Column == cell.Column + i * dx);

                        if (nei is null )
                            break;
                        // Pokud jsem narazil na prázdnou buňku, tak uložím konečnou buňky, uložím cestu k ní a skončím
                        if (nei.State == CellState.NotFound)
                        {
                            playables.Add(nei);
                            paths.Add(new List<CellViewModel>(temporary));
                            break;
                        }
                        // Pokud narazím na mojí buňku nebo buňku kterou už jsem našel, tak končím
                        else if (nei.State == turn || nei.State == CellState.Playable)
                            break;
                        // Pokud narazím na cizí buňku, tak ji přidám do cesty
                        else
                        {
                            temporary.Add(nei);
                            i++;
                        }
                    }
                }
            }
        }
    }
}
