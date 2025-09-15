using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BeastInLabyrinth
{
    internal class Program
    {
        static void Main(string[] args)
        {
            int width = Convert.ToInt32(Console.ReadLine());
            int height = Convert.ToInt32(Console.ReadLine());

            Labyrinth lab = new Labyrinth(width, height);

            lab.LabInput();
            lab.Print();

            Console.ReadLine();
        }
    }
    class Labyrinth
    {
        public int Width { get; }
        public int Height { get; }
        private char[,] labyrinth;
        private int x;
        private int y;
        private int[] orientation = new int[2];
        private int[] right = new int[2];
        public Labyrinth(int width, int height)
        {
            Width = width;
            Height = height;
            labyrinth = new char[Width, Height];
        }

        public void LabInput()
        {
            for (int i = 0; i < Height; i++)
            {
                string input = Console.ReadLine();
                for (int j = 0; j < input.Length; j++)
                {
                    if (OrientationSet(input[j]))
                    {
                        x = j;
                        y = i;
                    }
                    labyrinth[j, i] = input[j];
                }
            }
        }

        public void Print()
        {
            for (int i = 0; i < Height; i++)
            {
                for (int j = 0; j < Width; j++)
                {
                    Console.Write(labyrinth[j, i] + " ");
                }
                Console.WriteLine();
            }
        }

        private bool OrientationSet(char c)
        {
            switch (c)
            {
                case '<':
                    orientation[0] = -1;
                    orientation[1] = 0;
                    right[0] = 0;
                    right[1] = 1;
                    return true;
                case '^':
                    orientation[0] = 0;
                    orientation[1] = -1;
                    right[0] = 1;
                    right[1] = 0;
                    return true;
                case '>':
                    orientation[0] = 1;
                    orientation[1] = 0;
                    right[0] = 0;
                    right[1] = 1;
                    return true;
                case 'v':
                    orientation[0] = 0;
                    orientation[1] = 1;
                    right[0] = -1;
                    right[1] = 0;
                    return true;
                default:
                return false;
            }
        }

        public void Turn()
        {

        }

        private bool RightWall()
        {
            if (labyrinth[x + right[0], y + right[1]] == 'X')
            {
                labyrinth[x, y] = 'X'; // Jsem cooked, potřebuji si pamatovat jak to je otočený nějak líp
            }
            return false;
        }
    }
}
