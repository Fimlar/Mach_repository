using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;

namespace BeastInLabyrinth
{
    internal class Program
    {
        static void Main(string[] args)
        {
            int width = Convert.ToInt32(Console.ReadLine());
            int height = Convert.ToInt32(Console.ReadLine());

            Labyrinth lab = new Labyrinth(width, height);

            lab.LabInput();
            Console.WriteLine();

            for (int i = 1; i <= 20; i++)
            {
                lab.Turn();
                Console.WriteLine($"{i}. krok");
                PrintLabyrinth(lab, lab.labyrinth, lab.Beasts);
            }

            Console.ReadLine();
        }

        /// <summary>
        /// Metoda pro vytisknutí labyrintu
        /// </summary>
        /// <param name="lab"></param>
        /// <param name="matrix"></param>
        /// <param name="beast"></param>
        static void PrintLabyrinth(Labyrinth lab, char[,] matrix, List<Beast> beasts)
        {
            // 1️⃣ vytvoříme kopii labyrintu (abychom nepsali přímo do originálu)
            char[,] temp = new char[lab.Width, lab.Height];
            for (int y = 0; y < lab.Height; y++)
            {
                for (int x = 0; x < lab.Width; x++)
                {
                    temp[x, y] = matrix[x, y];
                }
            }

            // 2️⃣ umístíme beasty do kopie
            foreach (Beast beast in beasts)
            {
                temp[beast.X, beast.Y] = beast.Shape;
            }

            // 3️⃣ vytiskneme kopii
            for (int y = 0; y < lab.Height; y++)
            {
                for (int x = 0; x < lab.Width; x++)
                {
                    Console.Write(temp[x, y]);
                }
                Console.WriteLine();
            }
            Console.WriteLine();
        }
    }
    /// <summary>
    /// Třída popisující pole labyrintu
    /// </summary>
    class Labyrinth
    {
        public int Width { get; }
        public int Height { get; }
        public char[,] labyrinth;
        public List<Beast> Beasts { get; }

        /// <summary>
        /// Konstruktor třídy Labyrinth
        /// </summary>
        /// <param name="width">Šířka pole</param>
        /// <param name="height">Výška pole</param>
        public Labyrinth(int width, int height)
        {
            Width = width;
            Height = height;
            labyrinth = new char[Width, Height];
            Beasts = new List<Beast>();
        }

        /// <summary>
        /// Metoda pro nahrání pole
        /// </summary>
        public void LabInput()
        {
            for (int i = 0; i < Height; i++)
            {
                // Řádek na vstupu
                string input = Console.ReadLine();
                for (int j = 0; j < input.Length; j++)
                {
                    // Pokud není momentální znak zeď ani mezera, nastavím objekt beast se souřadnicemi tohoto znaku
                    if (input[j] != 'X' && input[j] != '.')
                    {
                        Beast beast = new Beast(input[j], j, i);
                        Beasts.Add(beast);
                        // Na místo příšery dám mezeru
                        labyrinth[j, i] = '.';
                        continue;
                    }
                    labyrinth[j, i] = input[j];
                }
            }
        }

        /// <summary>
        /// Metoda, která vykoná příslušný tah
        /// </summary>
        public void Turn()
        {
            foreach (Beast beast in Beasts)
            {
                // Když vpravo není zeď
                if (!beast.IsToRight(labyrinth))
                    // Pokud je vpravo přede mnou poslední zeď, od které jsem odešel, posunu se k ní (dopředu)
                    // Tahle podmínka je potřeba pro pozici, kdy se potřebuji otočit okolo rohu, takže jsem odešel od té zdi a potřebuji pokračovat podél ní
                    if (beast.IsFrontRightLast(labyrinth))
                        beast.MoveForward();
                    // Jinak jdu vpřed
                    else
                        beast.TurnRight();
                // Pokud je přede mnou stěna, otočím se doleva
                else if (beast.IsInFront(labyrinth))
                    beast.TurnLeft();
                else
                    beast.MoveForward();
            }
        }
    }

    /// <summary>
    /// Třída popisující příšeru
    /// </summary>
    class Beast
    {
        public char Shape {  get; private set; }
        public int X {  get; private set; }
        public int Y { get; private set; }

        /// <summary>
        ///  Souřadnice poslední zdi, od které jsem se posunul vpřed
        /// </summary>
        private int[] lastWall = new int[2];

        /// <summary>
        /// Array všech možných orientací šipky
        /// </summary>
        private char[] orientations = new char[4] { '<', '^', '>', 'v' };

        /// <summary>
        /// List dvojic souřadnic, které ukazují jak se změní souřadnice po kroku vpřed
        /// </summary>
        private int[,] forwardMove = new int[,] { { -1,  0 }, {  0, -1 }, {  1,  0 }, {  0, 1 }};


        /// <summary>
        /// Index toho, v jaké pozici v orientations a forwardMove zrovna jsme
        /// </summary>
        private int orientIndex = -1;

        /// <summary>
        /// Konstruktor třídy beast
        /// </summary>
        /// <param name="shape">Znak jak je monstrum natočení</param>
        /// <param name="x">Pozice monstra na ose x</param>
        /// <param name="y">Pozice monstra na ose y</param>
        public Beast(char shape, int x, int y)
        {
            Shape = shape;
            X = x;
            Y = y;
            orientIndex = Array.IndexOf(orientations, shape);
        }

        /// <summary>
        /// Metoda, která otočí monstrum doleva
        /// </summary>
        public void TurnLeft()
        {
            // Změním shape na tvar o jedna vlevo od momentálního
            // orientIndex - 1 nejde, protože prý (-1 % 4) = -1 ???
            Shape = orientations[(orientIndex + 3) % 4];
            orientIndex += 3;
        }   

        /// <summary>
        /// Metoda, která otočí monstrum doprava
        /// </summary>
        public void TurnRight()
        {
            Shape = orientations[(orientIndex + 1) % 4];
            orientIndex++;
        }

        /// <summary>
        /// Metoda, která pohne monstrem dopředu
        /// </summary>
        public void MoveForward()
        {
            // Uložím si souřadnice zdi, kterou mám momentálně vpravo, než od ní odejdu
            lastWall[0] = (X + forwardMove[(orientIndex + 1) % 4, 0]);
            lastWall[1] = (Y + forwardMove[(orientIndex + 1) % 4, 1]);
            X += forwardMove[orientIndex%4, 0];
            Y += forwardMove[orientIndex%4, 1];
        }

        /// <summary>
        /// Funkce, která zkontroluje, jestli je před monstrem v labyrintu zeď
        /// </summary>
        /// <param name="lab">Pole labyrintu</param>
        /// <returns>true pokud je</returns>
        public bool IsInFront(char[,] lab)
        {
            // Triviální
            if (lab[X + forwardMove[orientIndex % 4, 0], Y + forwardMove[orientIndex % 4, 1]] != '.')
                return true;
            return false;
        }

        /// <summary>
        /// Funkce, která zkontroluje, jestli je doprava od monstra v labyrintu zeď
        /// </summary>
        /// <param name="lab">Pole labyrintu</param>
        /// <returns>true pokud je</returns>
        public bool IsToRight(char[,] lab)
        {
            // Když by monstrum otočené o jedna doprava šlo dopředu, dojde na souřadnice zdi vpravo ode mě
            // Proto přičítám k mým souřadnicím změnu souřadnic následující orientace v seznamu. Pokud tam je zeď, vracím true
            if (lab[X + forwardMove[(orientIndex+1) % 4, 0], Y + forwardMove[(orientIndex + 1) % 4, 1]] == 'X')
                return true;
            return false;
        }

        /// <summary>
        /// Funkce, která zkontroluje, jestli se souřadnice o jedna vpravo a dopředu shodují se souřadnicemi poslední opuštěné zdi
        /// </summary>
        /// <param name="lab"></param>
        /// <returns>true, pokud se shodují</returns>
        public bool IsFrontRightLast(char[,] lab)
        {
            // Souřadnice získám tak, že sečtu změny pohybu dopředu a doprava
            if ((X + forwardMove[orientIndex % 4, 0]) + forwardMove[(orientIndex + 1) % 4, 0] == lastWall[0] && (Y + forwardMove[orientIndex % 4, 1]) + forwardMove[(orientIndex + 1) % 4, 1] == lastWall[1])
                return true;
            return false;
        }
    }
}