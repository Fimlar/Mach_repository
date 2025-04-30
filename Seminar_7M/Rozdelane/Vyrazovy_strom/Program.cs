using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace Vyrazovy_strom
{
    internal class Program
    {
        static void Main(string[] args)
        {
            ExpressionTree<string> tree = new ExpressionTree<string>();
            Console.WriteLine("Napiš příklad v Postfixu:");
            string[] input = Console.ReadLine().Split();
            tree.CreateFromPost(tree, input);
            Console.WriteLine(tree.ShowInfix());
            Console.WriteLine(tree.ShowPrefix());
        }
    }
    class Node<T>
    {
        // public int Key { get; set; }
        public T Value { get; set; }
        public Node<T> LeftSon { get; set; }
        public Node<T> RightSon { get; set; }
        public Node<T> Parent { get; set; }

        // konstruktor
        public Node(T value)
        {
            // Key = key;
            Value = value;
            LeftSon = null; RightSon = null; Parent = null;
        }
    }
    class ExpressionTree<T>
    {
        static Stack<Node<string>> stack = new Stack<Node<string>>();
        public Node<string> Root { get; set; }

        public ExpressionTree<T> CreateFromPost(ExpressionTree<T> tree, string[] input)
        {

            for (int i = 0; i < input.Length; i++)
            {
                if (float.TryParse(input[i], NumberStyles.Float, CultureInfo.InvariantCulture, out float number))
                {
                    // Pokud najdu číslo
                    Node<string>node = new Node<string>(input[i]);
                    stack.Push(node);
                }
                    
                else
                {
                    // Pokud najdu operátor
                    Node<string> node = new Node<string>(input[i]);
                    node.RightSon = stack.Pop();
                    node.LeftSon = stack.Pop();
                    stack.Push(node);
                    if (i ==  input.Length - 1)
                        Root = node;
                }
            }
            return tree;
        }

        public string ShowInfix()
        {
            void _show(Node<string> node, StringBuilder nodes)
            {
                if (node != null)
                {
                    if (node.RightSon != null && node.LeftSon != null)
                        nodes.Append("(");

                    _show(node.LeftSon, nodes);
                    nodes.Append(node.Value); //využijeme StringBuilder, abychom nevytvářely s každým dalším klíčem nový string => časově i paměťově daleko méně náročné než += u stringu
                    
                    _show(node.RightSon, nodes);
                    if (node.RightSon != null && node.LeftSon != null)
                        nodes.Append(")");
                }
            }
            StringBuilder sb = new StringBuilder();
            _show(Root, sb);
            sb.Remove(0, 1);
            sb.Remove(sb.Length - 1, 1);
            return sb.ToString(); // výpis ponecháme jednou naráz v Mainu, WriteLine do konzole je časově drahá operace
        }

        public string ShowPrefix()
        {
            void _show(Node<string> node, StringBuilder nodes)
            {
                if (node != null)
                {
                    nodes.Append(node.Value); //využijeme StringBuilder, abychom nevytvářely s každým dalším klíčem nový string => časově i paměťově daleko méně náročné než += u stringu
                    nodes.Append(" ");
                    _show(node.LeftSon, nodes);
                    _show(node.RightSon, nodes);
                }
            }
            StringBuilder sb = new StringBuilder();
            _show(Root, sb);
            return sb.ToString(); // výpis ponecháme jednou naráz v Mainu, WriteLine do konzole je časově drahá operace
        }
    }
}
