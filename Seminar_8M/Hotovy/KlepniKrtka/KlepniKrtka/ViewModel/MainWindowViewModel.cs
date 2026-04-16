using KlepniKrtka.Model;
using KlepniKrtka.MVVM;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media.Animation;

namespace KlepniKrtka.ViewModel
{
    internal class MainWindowViewModel : ViewModelBase
    {
        Random random = new Random();       // Iniciace Random, protože to z nějakého hloupého důvodu není static???
        
        #region Binding
        //Proměnné pro data binding
        public ObservableCollection<CellViewModel> Cells { get; set; }
        public RelayCommand StartCommand => new RelayCommand(execute => StartGame(), canExecute => _isGameRunning == false);
        public RelayCommand ClickCommand => new RelayCommand(execute => CellClicked(execute as CellViewModel), canExecute => _isGameRunning == true);
        public RelayCommand StopCommand => new RelayCommand(execute => StopGame(), canExecute => _isGameRunning == true);

        private int _score;
        /// <summary>
        /// Proměnná na uložení počtu klepnutých krtků
        /// </summary>
        public int Score
        {
            get { return _score; }
            set { _score = value; OnPropertyChanged(); }
        }

        private TimeSpan _remainingTime;
        /// <summary>
        /// Proměnná zaznamenající zbývající čas do konce hry
        /// </summary>
        public TimeSpan RemainingTime
        {
            get { return _remainingTime; }
            set { _remainingTime = value; OnPropertyChanged(); }
        }

        private Difficulty _selectedDifficulty;
        /// <summary>
        /// Proměnná v níž je uložena vybraná obtížnost
        /// </summary>
        public Difficulty SelectedDifficulty
        {
            get { return _selectedDifficulty; }
            set { _selectedDifficulty = value; OnPropertyChanged(); }
        }
        #endregion

        #region Private
        // Privátní proměnné pro použití u logiky
        private bool _isGameRunning = false;
        /// <summary>
        /// Random Xová souřadnice (krtka)
        /// </summary>
        private int rx = 0;
        /// <summary>
        /// Random Yová souřadnice (krtka)
        /// </summary>
        private int ry = 0;
        /// <summary>
        /// Náhodně vybraná buňka (většinou v ní je krtek)
        /// </summary>
        private CellViewModel rndCell { get; set; }
        #endregion

        public MainWindowViewModel()
        {
            Cells = new ObservableCollection<CellViewModel>();
        }

        #region Logic
        /// <summary>
        /// Funkce, která se spustí po kliknutí na tlačítko Start
        /// </summary>
        private async Task StartGame()
        {
            StopGame();     // Provedu zastavení hry pro vymazání všech nepotřebných věcí
            // Nastavím delay podle vybrané obtížnosti
            int delay = SelectedDifficulty switch
            {
                Difficulty.Easy => 800,
                Difficulty.Medium => 600,
                Difficulty.Hard => 400,
                _ => 800
            };

            // Zaplním pole buňkami
            for (int i = 0; i < 4; i++)
            {
                for (int j = 0; j < 4; j++)
                {
                    Cells.Add(new CellViewModel(new Cell(i, j)));
                }
            }

            // Dám do random buňky krtka
            rx = random.Next(4);
            ry = random.Next(4);
            rndCell = Cells.First(c => c.Row == ry && c.Column == rx);
            rndCell.IsMole = true;

            _isGameRunning = true;

            // Nadefinuji a zapnu Tasky, aby toho mohlo běžet několik současně
            Task logicTask = GameLogicLoop(delay);
            Task timerTask = EndTimer(60);

            await Task.WhenAll(timerTask, logicTask);
        }

        /// <summary>
        /// Funkce, která se spustí po kliknutí na buňku v hracím poli
        /// </summary>
        /// <param name="clicked">Kliknutá buňka</param>
        private void CellClicked(CellViewModel clicked)
        {
            if (clicked.IsMole)
                Score++;
        }
        /// <summary>
        /// Task, který spouští v zadaném intervalu změnu pozice krtka
        /// </summary>
        /// <param name="delay">Int v milisekundách jak často se změní pozice krtka</param>
        /// <returns></returns>
        private async Task GameLogicLoop(int delay)
        {
            while (_isGameRunning)
            {
                ChangeMolePosition();
                await Task.Delay(delay);
            }
        }
        /// <summary>
        /// Náhodně mění pozici krtka
        /// </summary>
        private async void ChangeMolePosition()
        {
            rndCell.IsMole = false;
            rx = random.Next(4);
            ry = random.Next(4);
            if (rx == rndCell.Column && ry == rndCell.Row)
            {
                ChangeMolePosition();
                return;
            }
            rndCell = Cells.First(c => c.Row == ry && c.Column == rx);
            rndCell.IsMole = true;
        }
        /// <summary>
        /// Task, který zajišťuje vypnutí hry po daném čase
        /// </summary>
        /// <param name="timeToPlay"></param>
        /// <returns></returns>
        private async Task EndTimer(int timeToPlay)
        {
            TimeSpan totalTime = TimeSpan.FromSeconds(timeToPlay);  // Čas jak dlouho chci hrát
            RemainingTime = totalTime;
            var sw = System.Diagnostics.Stopwatch.StartNew();       // Používám systémovou věc Stopwatch

            while (sw.Elapsed < totalTime && _isGameRunning)        // Každých 10 milisekund aktualizuji zbývající čas
            {
                RemainingTime = totalTime - sw.Elapsed;
                await Task.Delay(10);
            }

            // Když dojde čas, vypínáme hru
            RemainingTime = TimeSpan.Zero;
            _isGameRunning = false;
            sw.Stop();
        }
        /// <summary>
        /// Metoda pro reset všech hodnot při vypnutí hry
        /// </summary>
        private void StopGame()
        {
            // Reset všech hodnot
            _isGameRunning = false;
            Cells.Clear();
            Score = 0;
        }
        #endregion
    }
}
