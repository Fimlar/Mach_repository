using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Stable_marriage_problem
{
    internal class Program
    {
        static void Main(string[] args)
        {
            int n = Convert.ToInt32(Console.ReadLine());
            matrix main = new matrix(n);
            Input(main, n);

            for (int i = 0; i < n; i++)
            {
                Console.WriteLine(main.women[i, main.order[i] - 1]);
            }
            Console.ReadLine();

        }
        static void Input(matrix main, int n)
        {

            for (int i = 0; i < (n); i++)
            {
                string[] input = Console.ReadLine().Split(' ');
                main.WomanInput(input, i);
            }
            for (int i = 0; i < (n); i++)
            {
                string[] input = Console.ReadLine().Split(' ');
                main.ManInput(input, i);
            }
            main.GaleShapely();
        }


    }
    class matrix
    {
        public matrix(int n)
        {
            N = n;
            women = new int[n, n];
            men = new int[n, n];
            order = new int[n];
            manStatus = new int[n];
            womanIndex = new int[n];
            for (int i = 0; i < n; i++) 
            {
                manStatus[i] = n;
            }

            queue = new Queue<int>();
            for (int i = 0; i < n; i++)
            {
                queue.Enqueue(i);
            }
        }

        public int[,] women;
        private int[,] men;
        public int[] order; 
        private int[] manStatus; 
        private Queue<int> queue;
        private int N;
        private int[] womanIndex;
        
        public void ManInput(string[] row, int rowIndex)
        {
            for (int i = 0; i < row.Length; i++)
            {
                men[rowIndex, i] = Convert.ToInt32(row[i]);
            }
        }
        public void WomanInput(string[] row, int rowIndex)
        {
            for (int i = 0; i < row.Length; i++)
            {
                women[rowIndex, i] = Convert.ToInt32(row[i]);
            }
        }

        public void GaleShapely()
        {
            while (queue.Count != 0)
            {
                int womanNow = queue.Dequeue();
                int chosen = women[womanNow, order[womanNow]] - 1;
                ManWant(womanNow, chosen);
                order[womanNow] += 1;
            }
        }

        private void ManWant(int she, int he)
        {
            for (int i = 0; i < N; i++)
            {
                int currentWoman = men[he, i] - 1;
                if (currentWoman == she) // hledám tuto ženu v mužově seznamu
                {
                    if (manStatus[he] == N) // pokud ještě neměl muž přiřazenou ženu, tak mu dám tuhle
                    {
                        manStatus[he] = i;
                        break;
                    }
                    else if (manStatus[he] > i)  // přebíhá
                    {
                        queue.Enqueue(men[he, manStatus[he]] - 1);
                        manStatus[he] = i;
                        break;
                    }
                    else // nelíbí se mu
                    {
                        queue.Enqueue(she);
                        break;
                    }
                }
            }
        }

    }
}