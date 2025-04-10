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
            //(int height, int width, int winCount, int decision) = Input();
            //Position P = new Position(height, width, winCount);

            Position P = new Position(6, 7, 4);
            P.SetPosition("27171343233171313214");
            Solver solver = new Solver(7);
            P.PrintBoard();
            (int score1, int bestCol1) = solver.AlphaBeta(P, -21, 21);
            //(int score2, int bestCol2) = solver.Negamax(P);
            Console.WriteLine($"{score1} {bestCol1}");
            //Console.WriteLine($"{score2} {bestCol2}");

            /*if (decision == 1)
                vsAI(P);
            else
                vsPlayer(P);*/




        }
        
        static (int, int, int, int) Input()
        {
            Console.WriteLine("Ahoj! Vítej ve hře Connect4. Zadej prosím nějaké základní informace o tom jak chceš hrát: ");
            Console.WriteLine("Zadej výšku hracího pole: ");
            int height = Convert.ToInt32(Console.ReadLine());
            Console.WriteLine("Zadej šířku hracího pole: ");
            int width = Convert.ToInt32(Console.ReadLine());
            Console.WriteLine("Na kolik výherních kamenů chceš hrát?");
            int winCount = Convert.ToInt32(Console.ReadLine());
            Console.WriteLine("Chceš hrát proti AI (napiš 1) nebo hráči (napiš 2)?");
            int decision = Convert.ToInt32(Console.ReadLine());
            return (height, width, winCount, decision);
        }


        /// <summary>
        /// Provede tah do sloupce, který napíšeme do konzole
        /// </summary>
        /// <param name="P">Momentální stav hracího pole</param>
        /// <returns>true: hráč zahráním tohoto pole vyhrál; false: hra pokračuje bez výhry</returns>
        static bool Turn(Position P)
        {
            int col;
            while (true)
            {
                Console.Write("Zadej číslo sloupce: ");
                string input = Console.ReadLine();

                if (!int.TryParse(input, out col))                  // Zkoušíme jestli je input integer
                {
                    Console.WriteLine("Zadej platné číslo sloupce");
                    continue;
                }
                col--;
                if (col < 0 || col >= P.WIDTH)                      // Zkoušíme jestli je input v hranicích pole
                {
                    Console.WriteLine("Zadej platné číslo sloupce");
                    continue;
                }
                if (!P.CanPlay(col))                                // Zkoušíme jestli není chtěný sloupec plný
                {
                    Console.WriteLine("Toto pole je zabrané");
                    continue;
                }
                if (P.IsWinningMove(col, 1 + P.NbMoves() % 2))      // Zkoušíme jestli tímto tahem hráč vyhraje
                {
                    P.Play(col);
                    P.PrintBoard();
                    return true;
                }
                // Zahrajeme tah
                P.Play(col);
                break;
            }
            P.PrintBoard();
            return false;
        }

        static void vsAI(Position P)
        {
            Console.WriteLine("Zadej počet tahů, po kterém se bude používat Negamax: ");
            int movesCount = Convert.ToInt32(Console.ReadLine());
            Console.WriteLine("Chceš začínat ty nebo AI? (1 - já, 2 - AI)");
            int startDecision = Convert.ToInt32(Console.ReadLine());
            Solver solver = new Solver(P.WIDTH);

            while (true)
            {
                if (startDecision%2 == 1)
                {
                    Console.WriteLine("Jseš na tahu");
                    if (Turn(P))
                    {
                        Console.WriteLine("Vyhrál jsi!");
                        return;
                    }
                    startDecision++;
                    if (P.NbMoves() == P.WIDTH * P.HEIGHT)
                    {
                        Console.WriteLine("Hra skončila remízou.");
                        return;
                    }
                }
                else
                {
                    if (movesCount <= P.NbMoves())
                    {
                        
                    }
                }
            }
            


            /*while (true)
            {
                if (movesCount <= P.NbMoves())
                {
                    // Tah AI
                    (int, int) output = solver.Negamax(P);
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
                    if (P.StupidAIPlay())
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

            }*/
        }

        /*static void vsPlayer(PositionOriginal P)
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
        }*/
    }
    
}
