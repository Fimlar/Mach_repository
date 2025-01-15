using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Jarníkův_algoritmus
{
    internal class Program
    {
        static void Main(string[] args)
        {
            //Načtu počet vrcholů
            Console.WriteLine("Zadejte počet řádků matice:");
            int n = int.Parse(Console.ReadLine());

            //Načtu matici
            int[][] matrix = ReadMatrix(n);

            //Udělám list vzdáleností a nastavím je na nekonečno
            int[] distances = new int[n];
            for (int i = 0; i < n; i++)
            {
                distances[i] = int.MaxValue;
            }



            Console.Read();
        }
        static int[][] ReadMatrix(int n)
        {
            int[][] matrix = new int[n][];
            Console.WriteLine("Zadejte jednotlivé řádky matice (hodnoty oddělte mezerami):");
            for (int i = 0; i < n; i++)
            {
                Console.WriteLine($"Zadejte {i + 1}. řádek:");
                string[] input = Console.ReadLine().Split(' ');
                matrix[i] = new int[input.Length];
                for (int j = 0; j < n; j++)
                {
                    matrix[i][j] = int.Parse(input[j]);
                }
            }
            return matrix;
        }
        static void PrintMatrix(int[][] matrix)
        {
            foreach (var row in matrix)
            {
                Console.WriteLine(string.Join(" ", row));
            }
        }
    }
}
