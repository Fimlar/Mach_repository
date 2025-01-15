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
            Console.WriteLine("Zadejte počet řádků matice:");
            int n = int.Parse(Console.ReadLine());
            int[][] matrix = ReadMatrix(n);
            Array distances = new int[n];
            
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
