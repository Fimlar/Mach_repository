using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Connect_5
{
    internal class Program
    {
        static void Main(string[] args)
        {
            //načtení vstupů
            Console.WriteLine("Zadej šířku hracího pole: ");
            int width = Convert.ToInt32(Console.ReadLine());
            Console.WriteLine("Zadej výšku hracího pole: ");
            int height = Convert.ToInt32(Console.ReadLine());
            Console.WriteLine("Zadej jméno prvního hráče: ");
            string name1 = Console.ReadLine();
            Console.WriteLine("Zadej jméno druhého hráče: ");
            string name2 = Console.ReadLine();

            //3d array: 2d hrací pole a pro každé pole počet spojitých horizontálně, vertikálně a obě diagonály
            int[,,] board = new int[height, width, 5];

            

            //Samotná hra
            while (true)
            {
                Console.WriteLine($"Na tahu je {name1}");
                Turn(board, width, height, 1);
                Console.WriteLine($"Na tahu je {name2}");
                Turn(board, width, height, 2);
            }
        }
        static void PrintMatrix(int[,,] matrix)
        {
            int rows = matrix.GetLength(0);
            int cols = matrix.GetLength(1);
            for (int i = 0; i < rows; i++)
            {
                for (int j = 0; j < cols; j++)
                {
                    Console.Write(matrix[i, j, 0]);
                    if (j < cols - 1)
                        Console.Write(" ");
                }
                Console.WriteLine();
            }
        }
        static int[,,] Turn(int[,,] board, int width, int height, int player)
        {
            //error prevention
            int col;
            while (true)
            {
                col = Convert.ToInt32(Console.ReadLine());
                if (0 > col || col >= width)
                {
                    Console.WriteLine("Zadej platné číslo sloupce");
                    continue;
                }
                    
                if (board[0, col, 0] != 0)
                {
                    Console.WriteLine("Toto pole je zabrané");
                    continue;
                }  
                break;
            }
            
            for (int i = 0; i < height; i++)
            {
                //dávám naspod
                if (i == height - 1)
                {
                    board[i, col, 0] = player;
                    break;
                }
                   
                //dávám nad najitý
                if (board[i + 1, col, 0] == 0)
                    continue;
                else
                {
                    board[i, col, 0] = player;
                    break;
                }
            }

            PrintMatrix(board);
            return board;
        }
    }
}
