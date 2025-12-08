using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace PísemnáPráce
{
    internal class Program
    {
        static void Main(string[] args)
        {
            var tuple = Input();
            int s = tuple.Item1;
            int[,] obstacles = tuple.Item2;
            int[] start = tuple.Item3;
            int[] end = tuple.Item4;

            int n = BFS(s, obstacles, start, end);
            if (n == -1)
            {
                Console.WriteLine("Do cíle se koněm nejde dostat.");
            }
            else
                Console.WriteLine(n);

        }
        static (int, int[,], int[], int[]) Input()
        {
            using (StreamReader sr = new StreamReader(@"..\..\..\..\vstupni_soubory\2.txt"))
            {
                // načtení s počtu přakážek
                int s = int.Parse(sr.ReadLine());
                int[,] obstacles = new int[s, 2];

                // načtení souřadnic překážek
                for (int i = 0; i < s; i++)
                {
                    var line = sr.ReadLine().Split();

                    obstacles[i, 0] = Convert.ToInt32(line[1]);
                    obstacles[i, 1] = Convert.ToInt32(line[0]);
                }
                int[] start = new int[2];
                int[] end = new int[2];

                // načtu start
                var l = sr.ReadLine().Split();

                start[0] = Convert.ToInt32(l[1]);
                start[1] = Convert.ToInt32(l[0]);

                l = sr.ReadLine().Split();

                end[0] = Convert.ToInt32(l[1]);
                end[1] = Convert.ToInt32(l[0]);

                return (s,  obstacles, start, end);
            }
        }

        static List<(int, int)> FindNeighbours(int[,] obstacles, int x, int y, int s)
        {
            // všechny možné tahy koně
            int[,] possibleMoves = { { 1, 2 }, { 1, -2 }, { -1, 2 }, { -1, -2 },
                                     { 2, 1 }, { 2, -1 }, { -2, 1 }, { -2, -1 } };

            List<(int, int)> ret = new List<(int, int)> ();

            int check = 0;

            // pro všechny možné tahy hledám
            for (int i = 0; i < 8; i++)
            {
                int movex = possibleMoves[i, 0];
                int movey = possibleMoves[i, 1];
                // pro všechny překážky hledám možné tahy
                for (int j = 0; j < s; j++)
                {
                    int obstaclex = obstacles[j, 0];
                    int obstacley = obstacles[j, 1];
                    if ((x + movex == obstaclex && y + movey == obstacley) ||     // pokud tam je překážka
                        ((x+movex < 0 || y+movey < 0) || (x + movex > 7 || y + movey > 7)))   // pokud jsem mimo pole
                    {
                        check++;
                    }
                }
                // funguje to i se všemi překážkami
                if (check == 0)
                    ret.Add((x + movex, y + movey));
                check = 0;
            }


            return ret;
        }

        static int BFS(int s, int[,] obstacles, int[] start, int[] end)
        {
            // pole a ohodnocení jedntolivých políček
            int[,] matrix = new int[8, 8];

            // stav jednotlivých políček (0 - nenalezený, 1 - otevřený, 2- zavřený)
            int[,] states = new int[8, 8];

            // nastavení na počáteční hodnoty
            for (int i = 0; i < 8; i++)
            {
                for (int j = 0; j < 8; j++)
                {
                    matrix[i, j] = int.MaxValue;
                    states[i, j] = 0;
                }
            }

            Queue<int[]> queue = new Queue<int[]>();
            matrix[start[0], start[1]] = 0;
            queue.Enqueue(start);

            // prohledávám, dokud není přázdná fronta
            while (queue.Count > 0)
            {
                // vrchol, ze kterého budu prohledávat
                int[] current = queue.Dequeue();
                int x = current[0];
                int y = current[1];
                int currentValue = matrix[x, y];

                if (x == end[0] && y == end[1])
                {
                    return currentValue;
                }

                

                // najdu všechny sousedy, do kterých se dá jít
                List<(int, int)> neighbours = FindNeighbours(obstacles, x, y, s);


                for (int i = 0; i < neighbours.Count; i++)
                {
                    int neighbourx = neighbours[i].Item1;
                    int neighboury = neighbours[i].Item2;
                    if (states[neighbourx, neighboury] != 2 &&                  // pokud není vrchol uzavřený
                        matrix[neighbourx, neighboury] > currentValue + 1)      // pokud jsem našel lepší cestu
                    {
                        // zapíšu novou cestu
                        matrix[neighbourx, neighboury] = currentValue + 1;
                        states[neighbourx, neighboury] = 1;

                        // kopíruji to, protože to nemůžu prostě přidat
                        int[] currentNeighbour = new int[2];
                        currentNeighbour[0] = neighbourx;
                        currentNeighbour[1] = neighboury;

                        queue.Enqueue(currentNeighbour);
                    }
                }
                // uzavřu vrchol, ze kterého jsem právě prohledával
                states[x, y] = 2;
            }
            // nedošel jsem do cíle, ale už nemám co prohledávat => cesta neexistuje
            return -1;
        }
    }
}
