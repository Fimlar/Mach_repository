using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Runtime.InteropServices.ComTypes;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace Nahodne_dvojice
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Random rnd = new Random();
            int n = 15;
            //FileCreate(n);
            bool[,] matrix = ReadFileReturnMatrix(n);
            

            int[] partnersCount = ReadFileReturnPartners(n);
            int oddCount = ReadFileReturnOddCount(n);
            List<Tuple<int, int>> pairs = new List<Tuple<int, int>>();

            //načtení listu chybějících lidí
            Console.WriteLine("Kdo chybí");
            string input = Console.ReadLine();
            
            string[] inputStrings = input.Split(' ');
            int[] missing = new int[inputStrings.Length];
            bool cek = false;
            bool cek2 = false;

            if (string.IsNullOrEmpty(input) == false)
            {
                for (int i = 0; i < inputStrings.Length; i++)
                {
                    missing[i] = int.Parse(inputStrings[i]);
                }
            }
            else
            {
                //ček je abych nemusel řešit když je nikdo nechybí a missing.Length = 1
                cek = true;
            }

            //marknutí v matici chybějící
            if (cek == false)
            {
                for (int i = 0; i < missing.Length; i++)
                {
                    matrix[missing[i], n] = true;
                }
            }
            
            
            //ošetření lichého počtu lidí
            int randomPerson = 0;

            
            if (((n - missing.Length) % 2 == 1) | cek == true)
            {
                for (int i = 0; i < n; i++)
                {
                    randomPerson = rnd.Next(0, n);
                    if (matrix[randomPerson, n + 1] == false)
                    {
                        matrix[randomPerson, n + 1] = true;
                        oddCount += 1;
                        matrix[randomPerson, n] = true;
                        cek2 = true;
                        break;
                    }
                }
                if (oddCount == n)
                {
                    for (int i = 0; i < n; i++)
                    {
                        matrix[i, n] = false;
                        oddCount = 0;
                    }
                }
            }


            //"seřazení" podle počtu možných partnerů
            List<Tuple<int, int>> countsWithIndex = new List<Tuple<int, int>>();
            for (int i = 0; i < n; i++)
            {
                Tuple<int, int> t1 = new Tuple<int, int>(partnersCount[i], i);
                countsWithIndex.Add(t1);
            }
            countsWithIndex = countsWithIndex.OrderByDescending(x => x.Item1).ToList();
            Console.WriteLine(randomPerson);
            //Udělání algoritmu
            for (int i = 0; i < n; i++)
            {
                int currentPerson = countsWithIndex[i].Item2;
                if (matrix[currentPerson, n] == false)
                {
                    while (true)
                    {
                        int randFalse = rnd.Next(0, n);
                        if (randFalse == currentPerson)
                        {
                            continue;
                        }
                        if (matrix[randFalse, n] == false && matrix[currentPerson, randFalse] == false)
                        {
                            matrix[currentPerson, n] = true;
                            matrix[randFalse, n] = true;
                            matrix[currentPerson, randFalse] = true;
                            matrix[randFalse, currentPerson] = true;
                            Tuple<int, int> t1 = new Tuple<int, int>(currentPerson, randFalse);
                            pairs.Add(t1);
                            partnersCount[currentPerson]++;
                            partnersCount[randFalse]++;
                            break;
                        }
                    }
                }
            }
            pairs.ForEach(Console.WriteLine);
            if (cek2)
            {
                Console.WriteLine(randomPerson);
            }
            
            
            //Array.ForEach(partnersCount, Console.WriteLine);
            FinalSave(matrix, n, partnersCount, oddCount);

            Console.ReadLine();
        }
        static void FileCreate(int n)
        {
            using (StreamWriter sw = new StreamWriter("input.txt"))
            {
                //napsání počáteční matice
                for (int i = 0; i < n; i++)
                {
                    for (int y = 0; y < n + 2; y++)
                    {
                        if (y == i)
                        {
                            sw.Write("1");
                        }
                        else
                        {
                            sw.Write("0");
                        }
                    }
                    sw.WriteLine();
                }
                //napsání počátečních počtů partnerů
                for (int i = 0; i < n - 1; i++)
                {
                    sw.Write("0 ");
                }
                sw.WriteLine("0");
                //napsání počtu lichých
                sw.Write("0");

            }
        }
        static bool[,] ReadFileReturnMatrix(int n)
        {
            bool[,] matrix = new bool[n, n + 2];
            string[] lines = File.ReadAllLines("input.txt");

            Console.WriteLine($"Number of lines read: {lines.Length}");
            for (int i = 0; i < n; i++)
            {
                for (int j = 0; j < n + 2; j++)
                {
                    matrix[i, j] = lines[i][j] == '1';
                }
            }
            return matrix;
        }
        static int[] ReadFileReturnPartners(int n)
        {
            string line = File.ReadLines("input.txt").Skip(n).Take(1).First();
            string[] inputStrings = line.Split(' ');
            int[] partnersCount = new int[inputStrings.Length];
            for (int i = 0; i < n; i++)
            {
                partnersCount[i] = int.Parse(inputStrings[i]);
            }
            return partnersCount;
        }
        static int ReadFileReturnOddCount(int n)
        {
            return Convert.ToInt32(File.ReadLines("input.txt").Skip(n + 1).Take(1).First());
        }
        static void PrintMatrix(bool[,] matrix, int n)
        {
            for (int i = 0; i < n; i++)
            {
                for (int j = 0; j < n + 2; j++)
                {
                    Console.Write(matrix[i, j] + " ");
                }
                Console.WriteLine();
            }
        }
        static void FinalSave(bool[,] matrix, int n, int[] partnersCount, int oddCount)
        {
            using (StreamWriter writer = new StreamWriter("input.txt"))
            {
                //uložení matice
                for (int i = 0; i < n; i++)
                {
                    for (int j = 0; j < n + 2; j++)
                    {
                        if (j == n)
                        {
                            writer.Write("0");
                        }
                        else
                        {
                            writer.Write(matrix[i, j] ? "1" : "0");
                        }
                    }
                    writer.WriteLine();
                }
                for (int i = 0; i < partnersCount.Length-1; i++)
                {
                    writer.Write(partnersCount[i]);
                    writer.Write(' ');
                }
                writer.WriteLine(partnersCount[n-1]);
                writer.Write(oddCount);
            }
        }
    }
}
