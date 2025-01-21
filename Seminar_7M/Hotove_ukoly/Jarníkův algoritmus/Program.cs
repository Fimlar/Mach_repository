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
            //Načtení vstupů
            var tuple = ReadMatrix();
            int[,] matrix = tuple.Item1;
            int[][] vertices = tuple.Item2;
            int n = tuple.Item3;

            int[,] tree = Jarnik(matrix, vertices, n);
            PrintMatrix(tree);


            Console.Read();
        }
        static (int[,], int[][], int) ReadMatrix()          //Vracím matici, jagged array s vlastnostma vrcholů a počet vrcholů
        {
            Console.WriteLine("Zadej po řádcích matici sousednosti grafu: ");
            int[] input = Console.ReadLine().Split().Select(int.Parse).ToArray();
            int n = input.Length;
            int[,] matrix = new int[n, n];

            //Nahraju vstup do matice
            for (int i = 0; i < n; i++)
            {
                for (int j = 0; j < n; j++)
                {
                    matrix[i, j] = input[j];
                }
                if (i == n - 1)
                    break;
                input = Console.ReadLine().Split().Select(int.Parse).ToArray();
            }

            //Vytvořím list vrcholů se stavem (0 = mimo, 1 = soused, 2 = uvnitř), ohodnocením a p (druhý konec nejlehčí hrany)
            int[][] vertices = new int[n][];
            for (int i = 0; i < n; i++)
            {
                vertices[i] = new int[3];
                vertices[i][0] = 0;
                vertices[i][1] = int.MaxValue;
                vertices[i][2] = -1;
            }

            return (matrix, vertices, n);
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
        static int[,] Jarnik(int[,] matrix, int[][] vertices, int n)
        {
            int[,] tree = new int[n, n];
            vertices[0][0] = 1;
            vertices[0][1] = 0;
            int u = -1;
            while (true)
            {
                //Vybírám sousední vrchol s nejmenším ohodnocením, pokud neexistuje, končím
                int min = int.MaxValue;
                for (int i = 0; i < n; i++)
                {
                    if (vertices[i][0] == 1 && vertices[i][1] < min)
                    {
                        min = vertices[i][1];
                        u = i;
                    }

                }
                if (min == int.MaxValue)
                    break;

                vertices[u][0] = 2;
                if (vertices[u][2] != -1)
                    tree[u, vertices[u][2]] = 1;
                    tree[vertices[u][2], u] = 1;

                for (int i = 0; i < n; i++)
                {
                    if (matrix[u, i] != -1 && u != i && vertices[i][0] != 2 && vertices[i][1] > matrix[u, i])
                    {
                        vertices[i][0] = 1;
                        vertices[i][1] = matrix[u, i];
                        vertices[i][2] = u;
                    }
                }
            }
            return tree;
        }
    }
}
