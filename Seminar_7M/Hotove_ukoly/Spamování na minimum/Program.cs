using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Spamování_na_minimum
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
            int start = tuple.Item4;
            int[,] tree = Dijkstra(matrix, vertices, n, start);
            PrintMatrix(tree);

            Console.ReadLine();
        }
        static (int[,], int[][], int, int) ReadMatrix()          //Vracím matici, jagged array s vlastnostma vrcholů, počet vrcholů a počáteční vrchol
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
            //Vytvořím list vrcholů se stavem (0 = nenalezený, 1 = otevřený, 2 = uzavřený), ohodnocením a rodičem
            int[][] vertices = new int[n][];
            for (int i = 0; i < n; i++)
            {
                vertices[i] = new int[3];
                vertices[i][0] = 0;
                vertices[i][1] = int.MaxValue;
                vertices[i][2] = -1;
            }

            //Načtení jmen a startu a cíle
            Console.WriteLine("Zadej seznam jmen spolužáků v pořadí odpovídající matici oddělených středníkem: ");
            string[] names = Console.ReadLine().Split(';');
            Console.WriteLine("Zadej počátečního studenta: ");
            string startName = Console.ReadLine();
            int start = -1;
            for (int i = 0; i < n; i++)
            {
                if (names[i] == startName)
                {
                    start = i;
                    break;
                }
            }
            return (matrix, vertices, n, start);
        }
        static int[,] Dijkstra(int[,] matrix, int[][] vertices, int n, int start)
        {
            vertices[start][0] = 1;
            vertices[start][1] = 0;
            int v = -1;
            while (true)
            {
                //Vybírám otevřený vrchol s nejmenším ohodnocením, pokud neexistuje, končím
                int min = int.MaxValue;
                for (int i = 0; i < n; i++)
                {
                    if (vertices[i][0] == 1 && vertices[i][1] < min)
                    {
                        min = vertices[i][1];
                        v = i;
                    }
                        
                }
                if (min == int.MaxValue)
                    break;

                //Projdu všechny sousedy najitého vrcholu a nastavím jim vzdálenosti
                for (int i = 0; i < n; i++)
                {
                    if (matrix[v, i] != -1 && v != i && vertices[i][1] > vertices[v][1] + matrix[v, i])
                    {
                        vertices[i][1] = vertices[v][1] + matrix[v, i];
                        vertices[i][0] = 1;
                        vertices[i][2] = v;
                    }
                }
                vertices[v][0] = 2;
            }

            //Vytvořím strom nejkratších cest
            int[,] tree = new int[n, n];
            for (int i = 0; i < n; i++)
            {
                if (vertices[i][2] != -1)
                    tree[vertices[i][2], i] = 1;
            }
            return tree;
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
