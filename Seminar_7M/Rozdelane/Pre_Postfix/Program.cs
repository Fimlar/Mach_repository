using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Pre_Postfix
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Evaluate evaluate = new Evaluate();
            evaluate.Main2();
        }
    }

    // Zbytečná třída xdxdxd
    class Evaluate
    {
        static Stack<float> stack = new Stack<float>();

        /// <summary>
        /// Funkce co to celý zapne
        /// </summary>
        public void Main2()
        {
            int decision;
            Console.WriteLine("zadej jestli vstup je Prefix (1) nebo Postfix (2):");
            if (!int.TryParse(Console.ReadLine(), out decision))
            {
                Console.WriteLine("Zadej platnou hodnotu!");
                return;
            }
            Console.WriteLine("Zadej input:");
            string input = Console.ReadLine();
            var s = input.Split(' ');
            if (decision == 1)
                Prefix(s);
            else
                Postfix(s);
            return;
        }


        static void Postfix(string[] s)
        {
            float x;
            float a;
            float b;
            
            for (int i = 0; i < s.Length; i++)
            {
                if (float.TryParse(s[i], NumberStyles.Float, CultureInfo.InvariantCulture, out x))
                    stack.Push(x);
                else
                {
                    try
                    {
                        switch (s[i])
                        {
                            case "+":
                                a = stack.Pop();
                                b = stack.Pop();
                                stack.Push(b + a);
                                break;
                            case "-":
                                a = stack.Pop();
                                b = stack.Pop();
                                stack.Push(b - a);
                                break;
                            case "*":
                                a = stack.Pop();
                                b = stack.Pop();
                                stack.Push(b * a);
                                break;
                            case "/":
                                a = stack.Pop();
                                b = stack.Pop();
                                if (a != 0)
                                    stack.Push(b / a);
                                else
                                {
                                    Console.WriteLine("Neděl nulou!");
                                    return;
                                }
                                break;
                        }
                    }
                    catch
                    {
                        Console.WriteLine("Neplatný výraz: chybí operand/y");
                        return;
                    }
                }
            }

            // Hledám chybu
            if (stack.Count > 1)
            {
                Console.WriteLine("Neplatný výraz: chybí operátor/y");
                return;
            }
                
            Console.WriteLine(stack.Pop());
            return;
        }

        static void Prefix(string[] s)
        {
            Array.Reverse(s);
            Postfix(s);
            return;
        }
    }
}
