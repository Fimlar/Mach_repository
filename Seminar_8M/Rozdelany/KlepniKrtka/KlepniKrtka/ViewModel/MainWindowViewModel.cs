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
        Random random = new Random();
        private int rx = 0;
        private int ry = 0;
        private CellViewModel rndCell { get; set; }
        //Proměnné pro data binding
        public ObservableCollection<CellViewModel> Cells { get; set; }
        public RelayCommand StartCommand => new RelayCommand(execute => StartGame(), canExecute => _isGameRunning == false);
        public RelayCommand ClickCommand => new RelayCommand(execute => CellClicked(execute as CellViewModel), canExecute => _isGameRunning == true);
        public RelayCommand StopCommand => new RelayCommand(execute => StopGame(), canExecute => _isGameRunning == true);

        private int _score;

        public int Score
        {
            get { return _score; }
            set { _score = value; OnPropertyChanged(); }
        }

        private TimeSpan _remainingTime;

        public TimeSpan RemainingTime
        {
            get { return _remainingTime; }
            set { _remainingTime = value; OnPropertyChanged(); }
        }


        // Privátní proměnné pro použití u logiky
        private bool _isGameRunning = false;

        public MainWindowViewModel()
        {
            Cells = new ObservableCollection<CellViewModel>();
        }

        /// <summary>
        /// Funkce, která se spustí po kliknutí na tlačítko Start
        /// </summary>
        private async Task StartGame()
        {
            for (int i = 0; i < 4; i++)
            {
                for (int j = 0; j < 4; j++)
                {
                    Cells.Add(new CellViewModel(new Cell(i, j)));
                }
            }

            rx = random.Next(4);
            ry = random.Next(4);
            rndCell = Cells.First(c => c.Row == ry && c.Column == rx);
            rndCell.IsMole = true;

            _isGameRunning = true;

            Task logicTask = GameLogicLoop();
            Task timerTask = EndTimer(20);

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
        private async Task GameLogicLoop()
        {
            while (_isGameRunning)
            {
                ChangeMolePosition();
                await Task.Delay(1000);
            }
        }
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

        private async Task EndTimer(int timeToPlay)
        {
            TimeSpan totalTime = TimeSpan.FromSeconds(timeToPlay);  // Čas jak dlouho chci hrát
            RemainingTime = totalTime;
            var sw = System.Diagnostics.Stopwatch.StartNew();

            while (sw.Elapsed < totalTime && _isGameRunning)
            {
                RemainingTime = totalTime - sw.Elapsed;
                await Task.Delay(10);
            }

            RemainingTime = TimeSpan.Zero;
            _isGameRunning = false;
            sw.Stop();
        }

        private void StopGame()
        {
            // Reset všech hodnot
            _isGameRunning = false;
            Cells.Clear();
            Score = 0;
        }
    }
}
