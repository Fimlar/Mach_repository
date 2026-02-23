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
        private List<CellViewModel> possibleNeighbours = new List<CellViewModel>();

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
                    Brush brush = Brushes.Green;

                    if (i == 3 && j == 3)
                        brush = secondPlayerBrush;
                    else if (i == 3 && j == 4)
                        brush = firstPlayerBrush;
                    else if (i == 4 && j == 4)
                        brush = secondPlayerBrush;
                    else if (i == 4 && j == 3)
                        brush = firstPlayerBrush;

                    Cells.Add(new CellViewModel(model)
                    {
                        Row = i,
                        Column = j,
                        CellBrush = brush
                    });
                }
            }
            ColorPossibleMoves();
        }

        private void CellClicked(CellViewModel cell)
        {
            // pokud kliknu na jiné než možné pole
            if (cell.CellBrush != possibleMoveBrush)
                return;

            // Barvím buňky zpět a přebarvuji „ukradené“ buňky
            // Procházím všechny možné buňky
            for (int i = 0;i < possibleMoves.Count; i++)
            {
                CellViewModel c = possibleMoves[i];
                // Pokud je zkoumaná buňka ta možná, kterou momentálně prohledávám, přebarvím příslušného souseda
                if (cell.Row == c.Row && cell.Column == c.Column)
                {
                    if (turn)
                        possibleNeighbours[i].CellBrush = firstPlayerBrush;
                    else
                        possibleNeighbours[i].CellBrush = secondPlayerBrush;
                }
                c.CellBrush = Brushes.Green;
            }
            // Promažu listy s tahy
            possibleMoves = new List<CellViewModel>();
            possibleNeighbours = new List<CellViewModel>();

            if (turn)
            // Na tahu je první hráč
            {
                // Obarvím buňku na barvu prvního hráče
                cell.CellBrush = firstPlayerBrush;
                // Nastavím, že je na řadě hráč dva
                turn = false;
                ColorPossibleMoves();
            }
            else
            // Na tahu je druhý hráč
            {
                cell.CellBrush = secondPlayerBrush;

                turn = true;

                ColorPossibleMoves();
            }
        }

        private void ColorPossibleMoves()
        {
            
            Brush brush1;
            Brush brush2;
            // nastavím brush podle toho, kdo je zrovna na řadě
            if (turn)
            {
                brush1 = firstPlayerBrush;
                brush2 = secondPlayerBrush;
            }

            else
            {
                brush1 = secondPlayerBrush;
                brush2 = firstPlayerBrush;
            }
                

            // Najdu všechny buňky dané barvy
            foreach (var cell in Cells.Where(c => c.CellBrush == brush1))
                foreach (var dir in NeighborOffsets)
                {
                    int dx = dir.dx;
                    int dy = dir.dy;
                    try
                    {
                        // soused v tomto směru
                        CellViewModel nei = Cells.First(c => c.Row == cell.Row + dy && c.Column == cell.Column + dx);
                        // o jedno dál než soused
                        CellViewModel nxt = Cells.First(c => c.Row == cell.Row + dy * 2 && c.Column == cell.Column + dx * 2);
                        if (nei.CellBrush == brush2 && nxt.CellBrush == Brushes.Green)
                        {
                            possibleMoves.Add(nxt);
                            possibleNeighbours.Add(nei);
                            nxt.CellBrush = possibleMoveBrush;
                            OnPropertyChanged();
                        } 
                    }
                    catch
                    {
                        continue;
                    }
                }
            return;
        }
    }
}
