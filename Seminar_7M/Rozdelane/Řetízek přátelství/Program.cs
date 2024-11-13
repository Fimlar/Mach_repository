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
            bool[,] matrix = MatrixInput();
            PrintMatrix(matrix);
            Console.ReadLine();
        }
        static bool[,] MatrixInput()
        {
            int n = Convert.ToInt32(Console.ReadLine());
            bool[,] matrix = new bool[n, n];
            PrintMatrix(matrix);
            List<string> input = new List<string>(Console.ReadLine().Split());
            int[,] inputs = new int[2, input.Count()];
            for (int i = 0; i < input.Count(); i++)
            {
                inputs[i, 0] = int.Parse(input[i][0].ToString());
                inputs[i, 1] = int.Parse(input[i][2].ToString());
            }
            for (int i = 0; i < input.Count()-1; i++)
            {
                matrix[inputs[i, 0], inputs[i, 1]] = true;
                matrix[inputs[i, 1], inputs[i, 0]] = true;
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
        static List<int> FindNeighbours(List<int> neighbours)
        {

            return neighbours;
        }
    }
}
