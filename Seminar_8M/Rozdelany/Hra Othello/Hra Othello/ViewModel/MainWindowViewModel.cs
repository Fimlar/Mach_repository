using Hra_Othello.Model;
using Hra_Othello.MVVM;
using Microsoft.VisualBasic;
using System;
using System.CodeDom;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using System.Windows.Media;

namespace Hra_Othello.ViewModel
{
    internal class MainWindowViewModel : ViewModelBase
    {
        // Binding proměnné
        public ObservableCollection<CellViewModel> Cells { get; } = new ObservableCollection<CellViewModel>();


        // Privátní proměnné
        private bool turn = false;

        private Brush firstPlayerBrush = Brushes.Black;
        private Brush secondPlayerBrush = Brushes.White;
        private Brush possibleMoveBrush = Brushes.LightGreen;

        private List<CellViewModel> possibleMoves = new List<CellViewModel>();
        private readonly (int dx, int dy)[] NeighborOffsets =
        {
            (-1, -1), (0, -1), (1, -1),
            (-1,  0),          (1,  0),
            (-1,  1), (0,  1), (1,  1),
        };

        public ICommand StartCommand { get; }
        public ICommand ClickCommand { get; }

        public MainWindowViewModel()
        {
            StartCommand = new RelayCommand(_ => StartGame());
            ClickCommand = new RelayCommand(param => CellClicked((CellViewModel)param));
        }

        private void StartGame()
        {
            turn = true;
            for (int i = 0; i < 8; i++)
            {
                for (int j = 0; j < 8; j++)
                {
                    Cell model = new Cell();
                    Cells.Add(new CellViewModel(model) { Row = i, Column = j, IsBlack = false, CellBrush=Brushes.Green});
                }
            }
        }

        private void CellClicked(CellViewModel cell)
        {
            if (turn)
            // Na tahu je první hráč
            {
                foreach (var neighbour in FindNeighbours(cell).Where(c => c.IsBlack == true))
                {
                    neighbour.CellBrush = Brushes.Red;
                }

                ColorPossibleMoves();

                // Obarvím buňku na barvu prvního hráče
                cell.CellBrush = firstPlayerBrush;
                // Nastavím, že je na řadě hráč dva
                turn = false;
            }
            else
            // Na tahu je druhý hráč
            {
                cell.CellBrush = secondPlayerBrush;

                turn = true;
            }
        }

        private IEnumerable<CellViewModel> FindNeighbours(CellViewModel cell)
        {
            return Cells.Where(c =>
                Math.Abs(c.Row - cell.Row) <= 1 &&
                Math.Abs(c.Column - cell.Column) <= 1 &&
                c != cell);
        }

        private void ColorPossibleMoves()
        {
            if (turn)
            // Na řadě je černý
            {
                // Najdu všechny černé buňky
                foreach (var cell in Cells.Where(c => c.IsBlack))
                    foreach (var dir in NeighborOffsets)
                    {
                        
                    }
            }                 
        }
    }
}
