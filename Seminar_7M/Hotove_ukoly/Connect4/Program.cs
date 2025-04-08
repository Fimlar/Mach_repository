using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace Connect4
{
    internal class Program
    {
        static void Main(string[] args)
        {
            //načtení vstupů
            Console.WriteLine("Zadej šířku hracího pole: ");
            int width = Convert.ToInt32(Console.ReadLine());
            Console.WriteLine("Zadej výšku hracího pole: ");
            int height = Convert.ToInt32(Console.ReadLine());
            
            Console.WriteLine("Na kolik výherních hrajete?");
            int winCount = Convert.ToInt32(Console.ReadLine());

            Position P = new Position(width, height, winCount);

            Console.WriteLine("Chceš hrát proti AI (napiš 1) nebo hráči (napiš 2)?");
            int decision = Convert.ToInt32(Console.ReadLine());

            if (decision == 1)
                vsAI(P);
            else
                vsPlayer(P);


            // Testovací kód
            /*Connect4solver solver = new Connect4solver();
            string line = Console.ReadLine();
            
            Position P = new Position();
            
            if (P.SetPosition(line) != line.Length)
            {
                Console.Error.WriteLine($"Neplatný tah {P.NbMoves() + 1}");
            }
            else
            {
                P.PrintBoard();
                ulong startTime = GetTimeMicrosec();
                (int, int) output = solver.SolveNega(P);

                int score = output.Item1;
                int bestCol = output.Item2;

                ulong endTime = GetTimeMicrosec();
                Console.WriteLine($"{line} {score} {solver.GetNodeCount()} {endTime - startTime} {P.moves}");

            }
            Console.WriteLine();

            // Kód pro testování Alpha-beta pruningu
            
            if (P.SetPosition(line) != line.Length)
            {
                Console.Error.WriteLine($"Neplatný tah {P.NbMoves() + 1}");
            }
            else
            {
                ulong startTime = GetTimeMicrosec();
                (int score1, int score2) = solver.SolveBoth(P);
                ulong endTime = GetTimeMicrosec();
                Console.WriteLine(score1);
                Console.WriteLine(score2);
            }
            */


        }

        static void vsAI(Position P)
        {
            Console.WriteLine("Zadej počet tahů, po kterém se bude používat Negamax: ");
            int movesCount = Convert.ToInt32(Console.ReadLine());
            Console.WriteLine("Začíná AI a pokud ti to vadí, tak si to předělej!");
            Connect4solver solver = new Connect4solver();
            while (true)
            {
                if (movesCount <= P.NbMoves())
                {
                    // Tah AI
                    (int, int) output = solver.SolveNega(P);
                    int score = output.Item1;
                    int bestCol = output.Item2;
                    if (P.IsWinningMove(bestCol, 0))
                    {
                        Console.WriteLine("AI vyhrálo");
                        return;
                    }
                    P.Play(bestCol);
                    P.PrintBoard();

                    // Tah hráče
                    Console.WriteLine("Jseš na tahu");
                    if (Turn(P))
                        return;
                }
                else
                {
                    Console.WriteLine("Na tahu je AI");
                    if (P.AIStupidPlay())
                    {
                        Console.WriteLine("AI vyhrálo");
                        return;
                    }
                    P.PrintBoard();

                    // Tah hráče
                    Console.WriteLine("Jseš na tahu");
                    if (Turn(P))
                    {
                        Console.WriteLine("Vyhrál jsi");
                        return;
                    }
                        
                }

            }
        }

        static void vsPlayer(Position P)
        {
            Console.WriteLine("Zadej jméno prvního hráče: ");
            string name1 = Console.ReadLine();
            Console.WriteLine("Zadej jméno druhého hráče: ");
            string name2 = Console.ReadLine();
            while (true)
            {
                Console.WriteLine($"Na tahu je {name1}");
                if (Turn(P))
                {
                    Console.WriteLine($"Vyhrál {name1}");
                    return;
                }
                
                Console.WriteLine($"Na tahu je {name2}");
                if (Turn(P))
                {
                    Console.WriteLine($"Vyhrál {name2}");
                    return;
                }
            }
        }
        static bool Turn(Position P)
        {
            int col;
            while (true)
            {
                Console.Write("Zadej číslo sloupce: ");
                string input = Console.ReadLine();

                if (!int.TryParse(input, out col))
                {
                    Console.WriteLine("Zadej platné číslo sloupce");
                    continue;
                }
                if (col < 0 || col >= P.WIDTH)
                {
                    Console.WriteLine("Zadej platné číslo sloupce");
                    continue;
                }
                if (!P.CanPlay(col))
                {
                    Console.WriteLine("Toto pole je zabrané");
                    continue;
                }

                if (P.IsWinningMove(col, 0))
                {
                    P.Play(col);
                    P.PrintBoard();
                    return true;
                }
                P.Play(col);
                    

                break;
            }
            P.PrintBoard();
            return false;
        }


        static ulong GetTimeMicrosec()
        {
            return (ulong)(Stopwatch.GetTimestamp() / (Stopwatch.Frequency / 1000000));
        }
    }
    class Position
    {
        public int WIDTH {  get; private set; }
        public int HEIGHT { get; private set; }
        public int winCount { get; private set; }
        public int[,] board;
        public int[] height;
        public int moves;


        /*public const int WIDTH = 7;
        public const int HEIGHT = 6;
        public const int winCount = 4;
        public int[,] board = new int[HEIGHT, WIDTH];
        public int[] height = new int[WIDTH];
        public int moves = 0;*/


        public Position(int width, int height2, int winCount)
        {
            WIDTH = width;
            HEIGHT = height2;
            this.winCount = winCount;

            board = new int[HEIGHT, WIDTH];
            height = new int[WIDTH];

            for (int i = 0; i < HEIGHT; i++)
                for (int j = 0; j < WIDTH; j++)
                    board[i, j] = 0;
        }

        public Position(Position P)
        {
            WIDTH = P.WIDTH;
            HEIGHT = P.HEIGHT;
            winCount = P.winCount;

            board = new int[HEIGHT, WIDTH];
            height = new int[WIDTH];
            moves = P.moves;

            for (int i = 0; i < HEIGHT; i++)
                for (int j = 0; j < WIDTH; j++)
                    board[i, j] = P.board[i, j];
            for (int i = 0; i < WIDTH; i++)
                height[i] = P.height[i];
        }

        // Kontrola jestli lze zahrát na do tohoto sloupce, či jestli je moc plný
        public bool CanPlay(int col)
        {
            return height[col] < HEIGHT;
        }

        // Samotný tah
        public void Play(int col)
        {
            board[HEIGHT - height[col]-1, col] = 1 + moves % 2;
            height[col]++;
            moves++;
        }

        // Určuje, zda současný hráč vyhraje při zahrání daného sloupce
        public bool IsWinningMove(int col, int player)
        {
            return CheckRow(col, player) || CheckDiagonal(col, player);
        }

        // Kontroluje výhru na řádku a sloupci
        public bool CheckRow(int col, int player)
        {
            if (height[col] >= HEIGHT)
                return false;

            int y = HEIGHT - height[col] - 1;
            int currentPlayer;
            if (player == 0)
                currentPlayer = 1 + moves % 2;
            else
            {
                currentPlayer = 2;
                winCount--;
            }
                

            board[y, col] = currentPlayer;


            // Kontrola řádku (horizontální)
            int countRow = 0;
            for (int i = -winCount + 1; i < winCount; i++)
            {
                int x = col + i;
                if (x >= 0 && x < WIDTH)
                {
                    if (board[y, x] == currentPlayer)
                    {
                        countRow++;
                        if (countRow == winCount)
                        {
                            board[y, col] = 0;
                            if (player == 1)
                                winCount++;
                            return true;
                        }
                            
                    }
                    else
                    {
                        countRow = 0;
                    }
                }
            }

            // Kontrola sloupce (vertikální)
            int countColumn = 0;
            for (int i = y; i < HEIGHT; i++)
            {
                if (board[i, col] == currentPlayer)
                {
                    countColumn++;
                    if (countColumn == winCount)
                    {
                        board[y, col] = 0;
                        if (player == 1)
                            winCount++;
                        return true;
                    }
                        
                }
                else
                {
                    countColumn = 0;
                }
            }
            board[y, col] = 0;
            if (player == 1)
                winCount++;
            return false;
        }

        // Kontroluje výhru na diagonálách
        public bool CheckDiagonal(int col, int player)
        {
            if (height[col] >= HEIGHT)
                return false;

            int y = HEIGHT - height[col] - 1;
            int currentPlayer;
            if (player == 0)
                currentPlayer = 1 + moves % 2;
            else
            {
                currentPlayer = 2;
                winCount--;
            }
                


            board[y, col] = currentPlayer;

            // Diagonála  (zleva nahoře doprava dolů)
            int countDiag1 = 0;
            for (int d = -winCount + 1; d < winCount; d++)
            {                   
                int i = y + d;
                int j = col + d;
                if (i >= 0 && i < HEIGHT && j >= 0 && j < WIDTH)
                {
                    if (board[i, j] == currentPlayer)
                    {
                        countDiag1++;
                        if (countDiag1 == winCount)
                        {
                            board[y, col] = 0;
                            if (player == 1)
                                winCount++;
                            return true;
                        }
                            
                    }
                    else
                    {
                        countDiag1 = 0;
                    }
                }
            }

            // Diagonála  (zleva dole doprava nahoru)
            int countDiag2 = 0;
            for (int d = -winCount + 1; d < winCount; d++)
            {
                int i = y + d;
                int j = col - d;
                if (i >= 0 && i < HEIGHT && j >= 0 && j < WIDTH)
                {
                    if (board[i, j] == currentPlayer)
                    {
                        countDiag2++;
                        if (countDiag2 == winCount)
                        {
                            board[y, col] = 0;
                            if (player == 1)
                                winCount++;
                            return true;
                        }
                            
                    }
                    else
                    {
                        countDiag2 = 0;
                    }
                }
            }
            board[y, col] = 0;
            if (player == 1)
                winCount++;
            return false;
        }

        // Vrací počet tahů od začátku hry
        public int NbMoves()
        {
            return moves;
        }

        // Vytiskne celou hrací plochu
        public void PrintBoard()
        {
            for (int i = 0; i < HEIGHT; i++)
            {
                for (int j = 0; j < WIDTH; j++)
                {
                    if (board[i, j] == 0)
                        Console.Write("[ ] ");  // Prázdná buňka
                    else if (board[i, j] == 1)
                        Console.Write("[X] ");  // Tah hráče 1
                    else
                        Console.Write("[O] ");  // Tah hráče 2
                }
                Console.WriteLine();  // Nový řádek po každém řádku hracího pole
            }
            Console.WriteLine();  // Prázdný řádek mezi jednotlivými výtisky hracího pole
        }

        // Slouží k iniciaci startovní pozice pomocí stringu čísel (každé číslo odpovídá vhození žetonu do příslušného sloupce
        public int SetPosition(string seq)
        {
            for (int i = 0; i < seq.Length; i++)
            {
                int col = seq[i] - '1';
                if (col < 0 || col >= this.WIDTH || !CanPlay(col))    // Check jestli lze tento sloupec zahrát
                    return i;
                Play(col);
            }
            moves = seq.Length;
            return moves;
        }

        // AI tahy dokud není chytrá
        public bool AIStupidPlay()
        {
            for (int i = 0; i < WIDTH; i++)
            {
                if (IsWinningMove(i, 1))
                {
                    Play(i);
                    return false;
                }
                if (IsWinningMove(i, 0))
                {
                    Play(i);
                    PrintBoard();
                    return true;
                }
            }
            for (int i = 0; i < WIDTH; i++)
            {
                if (CanPlay(i))
                {
                    Play(i);
                    return false;
                }
            }
            return false;
        }

    }
    class Connect4solver
    {
        private ulong nodeCount; // Počítadlo prozkoumaných uzlů

        private (int, int) Negamax(Position P)
        {
            int bestCol = -1;
            nodeCount++;

            if (P.NbMoves() == P.WIDTH * P.HEIGHT) // Remíza
                return (0, bestCol);

            for (int i = 0; i < P.WIDTH; i++) // Kontrola, zda může hráč okamžitě vyhrát
                if (P.CanPlay(i) && P.IsWinningMove(i, 0))
                    return ((P.WIDTH * P.HEIGHT + 1 - P.NbMoves()) / 2, bestCol);

            int bestScore = -P.WIDTH * P.HEIGHT;
            

            for (int x = 0; x < P.WIDTH; x++)
            {
                if (P.CanPlay(x))
                {
                    Position P2 = new Position(P);
                    P2.Play(x);
                    //P2.PrintBoard();
                    int score = -Negamax(P2).Item1;
                    if (score > bestScore)
                    {
                        bestScore = score;
                        bestCol = x;
                    }
                        
                }
            }

            return (bestScore, bestCol);
        }

        public (int, int) SolveNega(Position P)
        {
            nodeCount = 0;
            return Negamax(P);
        }

        public ulong GetNodeCount()
        {
            return nodeCount;
        }

        private int AlphaBetaNega(Position P, int alpha, int beta)
        {
            nodeCount++;
            if (P.NbMoves() == P.WIDTH * P.HEIGHT) // Remíza
                return 0;

            for (int i = 0; i < P.WIDTH; i++) // Kontrola, zda může hráč okamžitě vyhrát
                if (P.CanPlay(i) && P.IsWinningMove(i, 0))
                    return (P.WIDTH * P.HEIGHT + 1 - P.NbMoves()) / 2;

            int max = (P.WIDTH * P.HEIGHT - 1 - P.NbMoves()) / 2;

            // Není potřeba ponechávat hodnoty mimo interval [alpha;beta]
            if (beta > max)
            {
                beta = max;
                if (alpha >= beta)
                    return beta;
            }

            for (int x = 0; x < P.WIDTH; x++)
            {
                if (P.CanPlay(x))
                {
                    Position P2 = new Position(P);
                    P2.Play(x);
                    //P2.PrintBoard();
                    int score = -AlphaBetaNega(P2, -beta, -alpha);
                    if (score >= beta)      // Zmenšíme interval, když najdeme lepší skóre
                        return score;
                    if (score > alpha)
                        alpha = score;
                }
            }
            return alpha;
        }

        public (int, int) SolveBoth(Position P)
        {
            nodeCount = 0;
            int score1 = AlphaBetaNega(P, int.MinValue, int.MaxValue);
            ulong nodes1 = nodeCount;

            nodeCount = 0;
            var (score2, _) = Negamax(P);
            ulong nodes2 = nodeCount;

            Console.WriteLine($"AlphaBeta: {score1}, Nodes: {nodes1}");
            Console.WriteLine($"Negamax:   {score2}, Nodes: {nodes2}");

            return (score1, score2);
        }

    }
}
