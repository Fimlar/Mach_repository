using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Řetízek_přátelství
{
    internal class Program
    {
        static void Main(string[] args)
        {
            //paměťová složitost tohoto řešení je O(n*n), jelikož si vytvoříme matici sousednosti s rozměry n*n a pak si držíme několik maximálně n dlouhých listů
            // časová složitost je také O(n*n), protože při prohledávání do šířky musíme projít pro každý z n vrcholů jeho maximálně n-1 sousedů
            Console.WriteLine("Zadejte počet vrcholů:");    //vytvoření matice sousednosti
            int n = Convert.ToInt32(Console.ReadLine());
            Console.WriteLine("Zadejte hrany ve formátu \"0-1 1-2\" atd.:");
            bool[,] matrix = MatrixInput(n);

            Console.WriteLine("Zadejte počáteční vrchol:");     //nahrání startu a cíle
            int start = Convert.ToInt32(Console.ReadLine());
            Console.WriteLine("Zadejte cílový vrchol:");
            int end = Convert.ToInt32(Console.ReadLine());

            List<int> path = BFS(matrix, start, end, n);    //najití cesty

            if (path != null)   //vypsání nejkratší cesty
            {
                Console.WriteLine("Nejkratší cesta:");
                foreach (int vertex in path)
                {
                    Console.Write(vertex + " ");
                }
            }
            else
            {
                Console.WriteLine("Cesta neexistuje.");
            }

            Console.ReadLine();
        }
        static bool[,] MatrixInput(int n)
        {
            bool[,] matrix = new bool[n, n];
            List<string> input = new List<string>(Console.ReadLine().Split());
            int[,] coords = new int[input.Count(), 2];
            for (int i = 0; i < input.Count(); i++)
            {
                coords[i, 0] = int.Parse(input[i][0].ToString())-1;
                coords[i, 1] = int.Parse(input[i][2].ToString())-1;
            }
            for (int i = 0; i < input.Count(); i++)
            {
                matrix[coords[i, 0], coords[i, 1]] = true;
                matrix[coords[i, 1], coords[i, 0]] = true;
            }
            return matrix;
        }
        static void PrintMatrix(bool[,] matrix)
        {
            for (int i = 0; i < matrix.GetLength(0); i++) // Procházení řádků
            {
                for (int j = 0; j < matrix.GetLength(1); j++) // Procházení sloupců
                {
                    Console.Write(matrix[i, j] ? "1 " : "0 "); // Výpis '1' pro true a '0' pro false
                }
                Console.WriteLine(); // Nový řádek po každém řádku matice
            }
        }
        static List<int> FindNeighbours(bool[,] matrix, int vertex, int n)
        {
            List<int> neighbours = new List<int>();
            for (int i = 0; i < n; i++)
            {
                if (matrix[vertex, i])
                {
                    neighbours.Add(i);
                }
            }
            return neighbours;
        }
        static List<int> BFS(bool[,] matrix, int start, int end, int n)
        {
            bool[] visited = new bool[n];
            int[] parent = new int[n];
            for (int i = 0; i < n; i++)
            {
                parent[i] = -1;
            }

            Queue<int> queue = new Queue<int>();
            visited[start] = true;
            queue.Enqueue(start);

            while (queue.Count > 0)
            {
                int current = queue.Dequeue();

                if (current == end)
                {
                    List<int> path = new List<int>();
                    while (current != -1)
                    {
                        path.Add(current);
                        current = parent[current];
                    }
                    path.Reverse();
                    return path;
                }

                foreach (int neighbour in FindNeighbours(matrix, current, n))
                {
                    if (!visited[neighbour])
                    {
                        visited[neighbour] = true;
                        parent[neighbour] = current;
                        queue.Enqueue(neighbour);
                    }
                }
            }
            return null;
        }
    }
}
