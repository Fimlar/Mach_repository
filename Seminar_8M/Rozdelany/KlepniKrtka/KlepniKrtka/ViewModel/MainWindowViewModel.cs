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

        private int _score;

        public int Score
        {
            get { return _score; }
            set { _score = value; OnPropertyChanged(); }
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

            EndTimer();

            while (_isGameRunning)
            {
                ChangeMolePosition();
                await Task.Delay(1000);
            }
                
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

        private async void EndTimer()
        {
            await Task.Delay(60000);
            _isGameRunning = false;
        }
    }
}
