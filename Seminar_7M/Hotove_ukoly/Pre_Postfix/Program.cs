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
        
        // Deklarace proměnných, abych je nemusel deklarovat v obou funkcích
        static float number;
        static float operand1;
        static float operand2;

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

        /// <summary>
        /// Vezme seznam stringů ve Postfixu a vypočítá
        /// </summary>
        /// <param name="s">Seznam stringů s operátory a operandy</param>
        static void Postfix(string[] s)
        {
            // Procházím celý array znaků z inputu
            for (int i = 0; i < s.Length; i++)
            {
                // Pokud najdu číslo, dám ho na zásobník
                if (float.TryParse(s[i], NumberStyles.Float, CultureInfo.InvariantCulture, out number))
                    stack.Push(number);
                else
                {
                    // Pokud najdu operand, beru vrchní dvě čísla ze zásobníku a provádím operaci
                    // Try abych zjistil, když je špatný vstup
                    try
                    {
                        // Zjišťuji jakou operaci mám provést
                        switch (s[i])
                        {
                            case "+":
                                operand1 = stack.Pop();
                                operand2 = stack.Pop();
                                stack.Push(operand2 + operand1);
                                break;
                            case "-":
                                operand1 = stack.Pop();
                                operand2 = stack.Pop();
                                stack.Push(operand2 - operand1);
                                break;
                            case "*":
                                operand1 = stack.Pop();
                                operand2 = stack.Pop();
                                stack.Push(operand2 * operand1);
                                break;
                            case "/":
                                operand1 = stack.Pop();
                                operand2 = stack.Pop();
                                // Kontroluji dělení nulou
                                if (operand1 != 0)
                                    stack.Push(operand2 / operand1);
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

        /// <summary>
        /// Vezme seznam stringů ve Prefixu a vypočítá
        /// </summary>
        /// <param name="s">Seznam stringů s operátory a operandy</param>
        static void Prefix(string[] s)
        {
            string operatoR;        // Pomocná proměnná
            Stack<string> stringStack = new Stack<string>();        // Zásobník stringů, jelikož musím umět ukložit i operátory

            // Jelikož musím nějak procházet prvky až do té doby, co je na zásobníku pouze 1 prvek, tak nevim jak jednoduše zjistit špatný vstup :(
            Console.WriteLine("Nevim jak zkontrolovat jestli je tvůj vstup vyhovující, takže pokud ne, tento výsledek je špatně :)");

            // Procházím vstupní list
            for (int i = 0; i < s.Length; i++)
            {
                // Pokud najdu operátor, dávám na zásobník
                if (!float.TryParse(s[i], NumberStyles.Float, CultureInfo.InvariantCulture, out number))
                    stringStack.Push(s[i]);
                else
                {
                    // Pokud najdu číslo, také dám na zásobník a jdu vyhodnocovat
                    stringStack.Push(s[i]);

                    // Potřebuji while, jelikož po spočítání můžou být na vrcholu zásobníku opět dva operandy
                    while (true)
                    {
                        // Pokud je v zásobníku pouze jeden prvek, končím
                        if (stringStack.Count == 1)
                        {
                            Console.WriteLine(stringStack.Pop());
                            return;
                        }

                        // Tenhle if je vždy true, ale je tady aby převedl číslo na float
                        if (float.TryParse(stringStack.Peek(), NumberStyles.Float, CultureInfo.InvariantCulture, out operand2))
                        {
                            // Kontroluji "druhý" prvek v zásobníku
                            stringStack.Pop();
                            if (float.TryParse(stringStack.Peek(), NumberStyles.Float, CultureInfo.InvariantCulture, out operand1))
                            {
                                // Pokud jsou horní dva prvky na zásobníku čísla, provedu operaci a výsledek přidám na zásobník
                                stringStack.Pop();
                                operatoR = stringStack.Pop();
                                switch (operatoR)
                                {
                                    case "+":
                                        stringStack.Push((operand1 + operand2).ToString(CultureInfo.InvariantCulture));
                                        break;
                                    case "-":
                                        stringStack.Push((operand1 - operand2).ToString(CultureInfo.InvariantCulture));
                                        break;
                                    case "*":
                                        stringStack.Push((operand1 * operand2).ToString(CultureInfo.InvariantCulture));
                                        break;
                                    case "/":
                                        // Nechci dělit nulou
                                        if (operand2 != 0)
                                            stringStack.Push((operand1 / operand2).ToString(CultureInfo.InvariantCulture));
                                        else
                                        {
                                            Console.WriteLine("Neděl nulou!");
                                            return;
                                        }
                                        break;
                                }
                            }
                            else
                            {
                                // Pokud "druhý" prvek je operátor, vracím operand a pokračuji
                                stringStack.Push(operand2.ToString(CultureInfo.InvariantCulture));
                                break;
                            }
                        }                          
                    }
                }
                
            }

            Console.WriteLine(stringStack.Pop());
            return;
        }
    }
}
