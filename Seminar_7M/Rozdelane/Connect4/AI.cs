using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Security.Permissions;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace Connect4
{
    /// <summary>
    /// Třída, která uchovává hrací pole
    /// </summary>
    public class Position
    {
        public bool SetPosition(string seq)
        {
            for (int i = 0; i < seq.Length; i++)
            {
                int col = seq[i] - '1';
                if (col < 0 || col >= this.WIDTH || !CanPlay(col))      // Kontrolujeme, jestli lze daný sloupec zahrát
                    return false;
                Play(col);
            }
            moves = seq.Length;         // Nastavujeme počet zahraných tahů na délku vstupu
            return true;
        }


        /// <summary>
        /// Vlastnosti HEIGHT, WIDTH a winCount potřebuju vždy získat, ovšem nikdy je nemusím jinde měnit
        /// </summary>
        public int HEIGHT { get; private set; }
        public int WIDTH { get; private set; }
        public int winCount { get; private set; }

        private int[,] board;       // Hrací pole   
        private int[] height;       // Pole s výškou každého sloupce
        private int moves = 0;      // Počet tahů od začátku hry


        /// <summary>
        /// Konstruktor třídy Position
        /// </summary>
        /// <param name="hEIGHT">Výška hracího pole</param>
        /// <param name="wIDTH">Šířka hracího pole</param>
        /// <param name="winCount">Počet vítězných kamenů</param>
        public Position(int hEIGHT, int wIDTH, int winCount)
        {
            HEIGHT = hEIGHT;
            WIDTH = wIDTH;
            this.winCount = winCount;
            this.board = new int[wIDTH, hEIGHT];
            this.height = new int[wIDTH];
        }


        /// <summary>
        /// Konstruktor, který zkopíruje zadanou pozici
        /// </summary>
        /// <param name="P">Zadaná pozice</param>
        public Position(Position P)
        {
            WIDTH = P.WIDTH;
            HEIGHT = P.HEIGHT;
            winCount = P.winCount;

            board = new int[WIDTH, HEIGHT];
            height = new int[WIDTH];
            moves = P.moves;

            for (int i = 0; i < HEIGHT; i++)
                for (int j = 0; j < WIDTH; j++)
                    board[j, i] = P.board[j, i];
            for (int i = 0; i < WIDTH; i++)
                height[i] = P.height[i];
        }


        /// <summary>
        /// Vytiskne hrací pole
        /// </summary>
        public void PrintBoard()
        {
            for (int i = 0; i < HEIGHT; i++)
            {
                for (int j = 0; j < WIDTH; j++)
                {
                    if (board[j, i] == 0)
                        Console.Write("[ ] ");  // Prázdná buňka
                    else if (board[j, i] == 1)
                        Console.Write("[X] ");  // Tah hráče 1
                    else
                        Console.Write("[O] ");  // Tah hráče 2
                }
                Console.WriteLine();  // Nový řádek po každém řádku hracího pole
            }
            Console.WriteLine();  // Prázdný řádek mezi jednotlivými výtisky hracího pole
        }


        /// <summary>
        /// Zkontoluje jestli není vybraný sloupec plný
        /// </summary>
        /// <param name="col">Číslo sloupce, do kterého chceme hodit kámen</param>
        /// <returns>true: sem kámen hodit můžeme; false: sloupec je plný</returns>
        public bool CanPlay(int col)
        {
            return height[col] < HEIGHT;
        }


        /// <summary>
        /// Vrací počet zahraných tahů
        /// </summary>
        /// <returns>Počet zahraných tahů</returns>
        public int NbMoves()
        {
            return moves;
        }


        /// <summary>
        /// Zahraje kámen do vybraného sloupce
        /// </summary>
        /// <param name="col">Sloupec, do kterého hrajeme kámen</param>
        public void Play(int col)
        {
            board[col, HEIGHT - height[col]-1] = 1 + moves % 2;     // Položíme kámen na jeho pozici
            height[col]++;                                          // Zvětšíme výšku v tomto sloupci
            moves++;                                                // Zvětšíme počet zahraných tahů
        }


        /// <summary>
        /// Kontroluje jestli když zahraji kámen do zadaného sloupce, tak někdo vyhraje
        /// </summary>
        /// <param name="col">Sloupec, do kterého házíme kámen</param>
        /// <param name="currentPlayer">Hráč, pro kterého kontrolujeme výhru</param>
        /// <returns>true: hráč vyhraje; false: hráč nevyhraje</returns>
        public bool IsWinningMove(int col, int currentPlayer)
        {
            int y = HEIGHT - height[col] - 1;
            return CheckDirection(col, y, 1, 0, currentPlayer) || CheckDirection(col, y, 0, 1, currentPlayer) || CheckDirection(col, y, 1, 1, currentPlayer) || CheckDirection(col, y, -1, 1, currentPlayer);
        }


        /// <summary>
        /// Kontroluje pro zadaný směr výhru hráče
        /// </summary>
        /// <param name="col">Sloupec, do kterého házíme kámen</param>
        /// <param name="row">Řada, do které kámen dopadne</param>
        /// <param name="dRow">Směr v řádku</param>
        /// <param name="dCol">Směr ve sloupci</param>
        /// <param name="currentPlayer">Hráč, pro kterého kontrolujeme výhru</param>
        /// <returns>true: v tomto směru je výhra; false: v tomto směru není výhra</returns>
        public bool CheckDirection(int col, int row, int dRow, int dCol, int currentPlayer)
        {
            int count = 1; // začínáme s 1, protože aktuální tah je už na pozici (col, row)

            // Zkontrolujeme oba směry (pozitivní a negativní směr)
            for (int j = -1; j < 2; j+=2)
            {
                for (int i = 1; i < winCount; i++) // počítáme od 1, protože pozice (col, row) už je zahrnuta
                {
                    int newRow = row + i * dRow * j;  // nový řádek
                    int newCol = col + i * dCol * j;  // nový sloupec
                    if (newRow < 0 || newRow >= HEIGHT || newCol < 0 || newCol >= WIDTH || board[newCol, newRow] != currentPlayer)
                        break; // pokud je mimo hranice nebo hodnoty se liší, přerušíme kontrolu
                    count++; // přidáme 1 k počtu propojených žetonů
                }
            }
            return count >= winCount; // pokud je propojeno dostatečně žetonů, hráč vyhrál
        }



        
    }

    public class Solver
    {
        private ulong nodeCount;        // Počet prozkoumaných uzlů
        private int[] columnOrder;      // Pořadí, ve kterém se prozkoumávají sloupce
        private int bestCol;

        /// <summary>
        /// Konstruktor třídy Solver; slouží primárně k nastavení priority sloupců
        /// </summary>
        /// <param name="WIDTH">Šířka hracího pole, pro nastavení počtu sloupců</param>
        public Solver(int WIDTH)
        {
            nodeCount = 0;
            columnOrder = new int[WIDTH];
            bestCol = -1;

            for (int i = 0; i < WIDTH; i++)
            {
                columnOrder[i] = WIDTH / 2 + (1 - 2 * (i % 2)) * (i + 1) / 2;         // Nastavuje pořadí sloupců, začíná se uprostřed a dále se alternuje levý a pravý sloupec
            }
        }


        /// <summary>
        /// Jednodužší verze MiniMaxu, která vyhodnotí skóre pozice a nejlepší tah
        /// </summary>
        /// <param name="P"></param>
        /// <returns>skóre pozice, nejlepší tah</returns>
        public (int, int) Negamax(Position P)
        {
            int bestCol = -1;
            nodeCount++;

            if (P.NbMoves() == P.WIDTH * P.HEIGHT) // Remíza
                return (0, bestCol);

            for (int i = 0; i < P.WIDTH; i++) // Kontrola, zda může hráč okamžitě vyhrát
                if (P.CanPlay(i) && P.IsWinningMove(i, 1 + P.NbMoves() % 2))
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


        /// <summary>
        /// Negamax s aplha-beta pruningem
        /// </summary>
        /// <param name="P">Pozice na prozkoumání</param>
        /// <param name="alpha">Skóre minimálního nebo maximálního hráče (nvm moc tomu nerozumim)</param>
        /// <param name="beta">To stejný jak alpha</param>
        /// <returns>Skóre pozice</returns>
        public (int, int) AlphaBeta(Position P, int alpha, int beta)
        {
            nodeCount++;
            if (P.NbMoves() == P.WIDTH * P.HEIGHT) // Remíza
                return (0, bestCol);

            for (int i = 0; i < P.WIDTH; i++) // Kontrola, zda může hráč okamžitě vyhrát
                if (P.CanPlay(i) && P.IsWinningMove(i, 1 + P.NbMoves() % 2))
                {
                    bestCol = i;
                    return ((P.WIDTH * P.HEIGHT + 1 - P.NbMoves()) / 2, bestCol);
                }
                    

            int max = (P.WIDTH * P.HEIGHT - 1 - P.NbMoves()) / 2;

            // Není potřeba ponechávat hodnoty mimo interval [alpha;beta]
            if (beta > max)
            {
                beta = max;
                if (alpha >= beta)
                    return (beta, bestCol);
            }

            for (int x = 0; x < P.WIDTH; x++)
            {
                if (P.CanPlay(columnOrder[x]))
                {
                    Position P2 = new Position(P);
                    P2.Play(columnOrder[x]);
                    //P2.PrintBoard();
                    int score = -AlphaBeta(P2, -beta, -alpha).Item1;
                    if (score >= beta)      // Zmenšíme interval, když najdeme lepší skóre
                    {
                        bestCol = columnOrder[x];
                        return (score, bestCol);
                    }     
                        
                    if (score > alpha)
                        alpha = score;
                }
            }
            return (alpha, bestCol);
        }



        public bool StupidAIPlay(Position P)
        {

            return true;
        }
    }
}
