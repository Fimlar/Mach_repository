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
            (int height, int width, int winCount, int playerDecision) = Input();
            Position P = new Position(height, width, winCount);

            if (playerDecision == 1)
                vsAI(P);
            else
                vsPlayer(P);

            Console.WriteLine("Díky za zahrání!");
        }

        /// <summary>
        /// Nahrání výšky a šířky hracího pole, počtu kamenů pro výhru a jestli chceme hrát proti AI či člověku
        /// </summary>
        /// <returns>Výška, šířka, winCount a proti komu chceme hrát</returns>
        static (int, int, int, int) Input()
        {
            Console.WriteLine("Ahoj! Vítej ve hře Connect4.");
            Console.WriteLine("Zadej výšku hracího pole: ");
            if (!int.TryParse(Console.ReadLine(), out int height))
                Console.WriteLine("Super, neumíš napsat číslo. Nehodlám sem kvůli tobě přidávat celej while cyklus, takže laskavě restartuj program a nauč se psát čísla.");
            Console.WriteLine("Zadej šířku hracího pole: ");
            if (!int.TryParse(Console.ReadLine(), out int width))
                Console.WriteLine("Super, neumíš napsat číslo. Nehodlám sem kvůli tobě přidávat celej while cyklus, takže laskavě restartuj program a nauč se psát čísla.");
            Console.WriteLine("Na kolik výherních kamenů chceš hrát?");
            if (!int.TryParse(Console.ReadLine(), out int winCount))
                Console.WriteLine("Super, neumíš napsat číslo. Nehodlám sem kvůli tobě přidávat celej while cyklus, takže laskavě restartuj program a nauč se psát čísla.");
            Console.WriteLine("Chceš hrát proti AI (napiš 1) nebo hráči (napiš 2)?");
            if (!int.TryParse(Console.ReadLine(), out int playerDecision))
                Console.WriteLine("Super, neumíš napsat číslo. Nehodlám sem kvůli tobě přidávat celej while cyklus, takže laskavě restartuj program a nauč se psát čísla.");
            return (height, width, winCount, playerDecision);
        }


        /// <summary>
        /// Provede tah do sloupce, který napíšeme do konzole
        /// </summary>
        /// <param name="P">Momentální stav hracího pole</param>
        /// <returns>true: hráč zahráním tohoto pole vyhrál; false: hra pokračuje bez výhry</returns>
        static bool Turn(Position P)
        {
            // While pro vícero pokusů o napsání funkčního sloupce
            while (true)
            {
                Console.Write("Zadej číslo sloupce: ");
                string input = Console.ReadLine();

                if (!int.TryParse(input, out int col))                  // Zkoušíme jestli je input integer
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
                {                                                   // Pokud vyhraje, vracíme true
                    P.Play(col);
                    P.PrintBoard();
                    return true;
                }
                // Zahrajeme tah
                P.Play(col);
                break;
            }
            // Vracíme false, jelikož tah byl proveden, ale nebyl výherní
            P.PrintBoard();
            return false;
        }


        /// <summary>
        /// Hrajeme proti AI
        /// </summary>
        /// <param name="P">Počáteční pozice</param>
        static void vsAI(Position P)
        {
            // Nahrání vstupů
            Console.WriteLine("Zadej počet tahů, po kterém se bude používat Negamax. ");
            Console.WriteLine("Pro normální velikost 7x6 doporučuji cca 16 tahů, poté musíš upravovat podle velikosti pole. (čím větší pole, tím déle to potrvá)");
            if (!int.TryParse(Console.ReadLine(), out int movesCount))
                Console.WriteLine("Super, neumíš napsat číslo. Nehodlám sem kvůli tobě přidávat celej while cyklus, takže laskavě restartuj program a nauč se psát čísla.");
            Console.WriteLine("Chceš začínat ty nebo AI? (1 - já, 2 - AI)");
            if (!int.TryParse(Console.ReadLine(), out int startDecision))
                Console.WriteLine("Super, neumíš napsat číslo. Nehodlám sem kvůli tobě přidávat celej while cyklus, takže laskavě restartuj program a nauč se psát čísla.");

            Console.WriteLine("Sloupce se číslují zleva od čísla 1.");

            Solver solver = new Solver(P.WIDTH);        // Iniciace objektu solver třídy Solver
            int bestCol;                                // Deklarování proměnné vracené funkcí AlphaBeta

            P.PrintBoard();         // Tiskneme pole pro přehlednost pro uživatele

            // Opakujeme, dokud někdo nevyhraje, nebo není remíza 
            while (true)
            {
                // Check remízy
                if (P.NbMoves() == P.WIDTH * P.HEIGHT)
                {
                    Console.WriteLine("Hra skončila remízou.");
                    return;
                }
                //Console.WriteLine(P.NbMoves());

                // Zjišťuje, jestli je na řadě člověk nebo AI. Záleží na tom kdo začíná
                if (startDecision % 2 == 1)
                {
                    // Na tahu je člověk
                    Console.WriteLine("Jseš na tahu");
                    if (Turn(P))                                    // Provedeme tah a pokud je výherní, tak končíme
                    {
                        Console.WriteLine("Vyhrál jsi!");
                        return;
                    }
                    startDecision++;            // Zvětšujeme pro posunutí tahu na AI
                }
                else
                {
                    // Na tahu je AI

                    // Kontrolujeme, jestli je už odehráno dost tahů na zapnutí Alpha-beta pruningu Negamaxu. Pokud ho zapneme moc brzo, bude to trvat moc dlouho, jelikož složitost je exponenciální
                    if (movesCount > P.NbMoves())
                    {
                        // Bylo zahráno moc málo tahů, takže pouštíme "hloupý" algoritmus
                        if (solver.StupidAIPlay(P))
                        {
                            // Pokud AI zahrálo výherní tah, končíme
                            Console.WriteLine("AI vyhrálo");
                            return;
                        }
                        startDecision++;        // Zvětšujeme pro posunutí tahu na člověka
                    }
                    else
                    {
                        // Jsme dostatečně daleko ve hře, zapínáme zapínáme pásy!!!!
                        bestCol = solver.AlphaBeta(P, -P.HEIGHT * P.WIDTH / 2, P.HEIGHT * P.WIDTH / 2).Item2;                   // Pustíme algoritmus a uložíme nejlepší tah

                        // Jestliže je nejlepší tah výherní, končíme
                        if (P.IsWinningMove(bestCol, 1 + P.NbMoves() % 2))
                        {
                            P.Play(bestCol);
                            P.PrintBoard();
                            Console.WriteLine("AI vyhrálo");
                            return;
                        }

                        // Hrajeme tah a jde se dál
                        P.Play(bestCol);
                        P.PrintBoard();
                        startDecision++;
                    }
                }
            }

        }


        /// <summary>
        /// Hrajeme proti člověku
        /// </summary>
        /// <param name="P">Počáteční pozice</param>
        static void vsPlayer(Position P)
        {
            // Načítáme jména hráčů
            Console.WriteLine("Zadej jméno prvního hráče: ");
            string name1 = Console.ReadLine();
            Console.WriteLine("Zadej jméno druhého hráče: ");
            string name2 = Console.ReadLine();

            Console.WriteLine("Sloupce se číslují zleva od čísla 1.");

            P.PrintBoard();         // Tiskneme pole pro přehlednost pro uživatele

            // Hrajeme dokud někdo nevyhraje, nebo hra neskončí remízou
            while (true)
            {
                // Check remízy
                if (P.NbMoves() == P.WIDTH * P.HEIGHT)
                {
                    Console.WriteLine("Hra skončila remízou.");
                    return;
                }

                // Tah hráče 1
                Console.WriteLine($"Na tahu je {name1}");
                if (Turn(P))
                {
                    Console.WriteLine($"Vyhrál/a {name1}");
                    return;
                }

                // Tah hráče 2
                Console.WriteLine($"Na tahu je {name2}");
                if (Turn(P))
                {
                    Console.WriteLine($"Vyhrál/a {name2}");
                    return;
                }
            }
        }
    }
}
