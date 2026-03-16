using System;
using System.CodeDom;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Knapsack_dynamicly
{
    internal class Program
    {
        static void Main(string[] args)
        {
            string[] names = new string[60];
            int[] val = new int[60];
            int[] wt = new int[60];
            (val, wt, names) = Input();

            List<string> result = new List<string>();
            result = Knapsack(names, val, wt);
        }

        static (int[], int[], string[]) Input()
        {
            string[] input;

            string[] names = new string[60];
            int[] val = new int[60];
            int[] wt = new int[60];
            for (int i = 0; i < 60; i++)
            {
                input = Console.ReadLine().Split(',');
                names[i] = input[0];
                wt[i] = int.Parse(input[1]);
                val[i] = int.Parse(input[2]);
            }

            return (val, wt, names);
        }

        static List<string> Knapsack(string[] names, int[] val, int[] wt)
        {
            List<string> result = new List<string>();

            int[,] table = new int[49, 61];

            int currentWt;
            int currentVal;

            for (int row = 1; row < 49; row++)
            {
                for (int column = 0; column < 61; column++)
                {
                    currentVal = val[row-1];
                    currentWt = wt[row-1];

                    try
                    {
                        if (table[row-1, column] < table[row-1, column - currentWt] + currentVal)
                        {
                            table[row, column] = table[row - 1, column - currentWt] + currentVal;
                        }
                        else
                        {
                            table[row, column] = table[row - 1, column];
                        }
                    }
                    catch
                    {
                        table[row, column] = table[row - 1, column];
                        continue;
                    }
                }
                /*PrintMatrix(table);
                Console.WriteLine();*/
            }

            //PrintMatrix(table);
            int n = table[48, 60];
            Console.WriteLine(n);

            return result;
        }
        static void PrintMatrix(int[,] matrix)
        {
            int rows = matrix.GetLength(0);
            int cols = matrix.GetLength(1);

            for (int i = 0; i < rows; i++)
            {
                for (int j = 0; j < cols; j++)
                {
                    Console.Write(matrix[i, j]);
                    if (j < cols - 1)
                        Console.Write(" ");
                }
                Console.WriteLine();
            }
        }

    }
}
