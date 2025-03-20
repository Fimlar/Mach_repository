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
            Console.WriteLine("Na kolik výherních hrajete?");
            int winCount = Convert.ToInt32(Console.ReadLine());


            int[,] board = new int[height, width];
            int[] position;

            //Samotná hra
            while (true)
            {
                Console.WriteLine($"Na tahu je {name1}");
                (board, position) = Turn(board, width, height, 1);
                if (Check(board, winCount, 1, position))
                {
                    Console.WriteLine($"{name1} vyhrál!");
                    break;
                }
                
                Console.WriteLine($"Na tahu je {name2}");
                Turn(board, width, height, 2);
                if (Check(board, winCount, 2, position))
                {
                    Console.WriteLine($"{name2} vyhrál!");
                    break;
                }
            }
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
        static (int[,], int[]) Turn(int[,] board, int width, int height, int player)
        {
            int[] position = new int[2];
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
                    
                if (board[0, col] != 0)
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
                    board[i, col] = player;
                    position[0] = i;
                    position[1] = col;
                    break;
                }
                   
                //dávám nad najitý
                if (board[i + 1, col] == 0)
                    continue;
                else
                {
                    board[i, col] = player;
                    position[0] = i;
                    position[1] = col;
                    break;
                }
            }

            PrintMatrix(board);
            return (board, position);
        }

        static bool Check(int[,] board, int winCount, int player, int[] position)
        {
            return CheckRow(board, winCount, player, position) || CheckDiagonal(board, winCount, player, position);
        }
        static bool CheckRow(int[,] board, int winCount, int player, int[] position)
        {
            int y = position[0];
            int x = position[1];
            int countRow = 0;
            int countColumn = 0;
            for (int i = -winCount+1; i < winCount+1; i++)
            {
                //kontroluji řádek
                if (countRow == winCount || countColumn == winCount)
                    return true;
                try
                {
                    if (board[y, x + i] == player)
                        countRow++;
                    else
                        countRow = 0;

                }
                catch (Exception e)
                {
                    //nic
                }
                //kontroluji sloupec
                try
                {
                    if (board[y + i, x] == player)
                        countColumn++;
                    else
                        countColumn = 0;

                }
                catch (Exception e)
                {
                    continue;
                }

            }

            return false;
        }

        static bool CheckDiagonal(int[,] board, int winCount, int player, int[] position)
        {
            int y = position[0];
            int x = position[1];
            int countRow = 0;
            int countColumn = 0;
            for (int i = -winCount + 1; i < winCount + 1; i++)
            {
                //kontroluji řádek
                if (countRow == winCount || countColumn == winCount)
                    return true;
                try
                {
                    if (board[y + i, x + i] == player)
                        countRow++;
                    else
                        countRow = 0;

                }
                catch (Exception e)
                {
                    //nic
                }
                //kontroluji sloupec
                try
                {
                    if (board[y + i, x - i] == player)
                        countColumn++;
                    else
                        countColumn = 0;

                }
                catch (Exception e)
                {
                    continue;
                }

            }

            return false;
        }
    }
}
