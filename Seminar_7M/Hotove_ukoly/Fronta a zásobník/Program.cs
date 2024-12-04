using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Fronta_a_zásobník
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine(SpravneUzavorkovani());
            Console.Read();
        }
        static bool SpravneUzavorkovani()
        {
            string input = Console.ReadLine();
            Stack<char> stack = new Stack<char>();
            for (int i = 0; i < input.Length; i++)
            {
                char a = input[i];
                if (stack.Count == 0)
                    stack.Push(a);
                else
                {
                    if (a == '(' | a == '[' | a == '{')
                        stack.Push(a);
                    else
                    {
                        char b = stack.Pop();
                        if (a != InvertBrackets(b))
                            return false;
                    }
                }
            }
            if (stack.Count != 0)
                return false;
            return true;
        }
        static char InvertBrackets(char m)
        {
            switch (m)
            {
                case '(':
                    return ')';
                case ')':
                    return '(';
                case '[':
                    return ']';
                case ']':
                    return '[';
                case '{':
                    return '}';
                case '}':
                    return '{';
                default:
                    return m;
            }
        }
    }
}
