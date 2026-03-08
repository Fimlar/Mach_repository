using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Markup;

namespace Backtracking_Mince
{
    internal class Program
    {
        static void Main(string[] args)
        {
            CoinsBacktrack coinsBackTrack = new CoinsBacktrack();

            coinsBackTrack.Input();

            Console.ReadLine();
        }
    }
    class CoinsBacktrack
    {
        public int N = -1;
        private List<int> values = new List<int>();


        /// <summary>
        /// Funkce pro načtení inputu z konzole
        /// </summary>
        public void Input()
        {
            string[] input = Console.ReadLine().Split(' ');

            // Získám hodnoty mincí
            foreach (string part in input)
            {
                if (!int.TryParse(part, out int value))
                {
                    Console.WriteLine($"Chyba: '{part}' není platné číslo.");
                    return;
                }

                if (value <= 0)
                {
                    Console.WriteLine($"Chyba: '{value}' není kladné číslo.");
                    return;
                }

                values.Add(value);
            }

            values.Sort();
            values.Reverse();

            string x = Console.ReadLine();

            // Získám hledanou sumu
            if (!int.TryParse(x, out N))
            {
                Console.WriteLine("Zadej platné číslo");
                return;
            }
            if (N < 0)
            {
                Console.WriteLine("Zadej kladné číslo");
                return;
            }

            return;
        }

        /// <summary>
        /// 
        /// </summary>
        public void BackTrack(int drag)
        {
            foreach (int i in values)
            {

            }
        }
    }
}
