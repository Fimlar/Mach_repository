using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Backtracking_Mince
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Input();
        }

        static void Input()
        {
            string[] input = Console.ReadLine().Split(' ');
            List<int> numbers = new List<int>();

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

                numbers.Add(value);
            }

            numbers.Sort();
            numbers.Reverse();

            int n;
            string x = Console.ReadLine();

            if (!int.TryParse(x, out n))
            {
                Console.WriteLine("Zadej platné číslo");
                return;
            }
            if (n<0)
            {
                Console.WriteLine("Zadej kladné číslo");
                return;
            }
        }
    }
}
