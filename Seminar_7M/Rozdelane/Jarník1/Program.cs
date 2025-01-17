using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Jarník1
{
    internal class Program
    {
        class Edge
        {
            public int Source { get; }
            public int Destination { get; }
            public int Weight { get; }

            public Edge(int source, int destination, int weight)
            {
                Source = source;
                Destination = destination;
                Weight = weight;
            }
            static void Main(string[] args)
            {
                Console.WriteLine("Zadej počet vrcholů: ");
                int n = int.Parse(Console.ReadLine());
                List<Edge> edges = ReadGraph(n);

                Console.ReadLine();
            }
            static List<Edge> ReadGraph(int n)
            {
                List<Edge> edges = new List<Edge>();
                Console.WriteLine("Zadej hrany grafu ve tvaru 'Odkud Kam Váha'. Nakonec zadej prázdný řádek.");
                while (true)
                {
                    string[] input = Console.ReadLine().Split();
                    if (input.Length == 1)
                        break;
                    Edge edge = new Edge(int.Parse(input[0]), int.Parse(input[1]), int.Parse(input[2]));
                    edges.Add(edge);
                }
                return edges;
            }
            static List<Edge> Jarnik(List<Edge> edges, int n)
            {
                List<Edge> tree = new List<Edge>();

            }
        }
    }
}
