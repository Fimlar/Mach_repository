using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Spojovy_seznam
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Node uzlik = new Node(8);          
            
        }
    }
    class Node
    {
        public Node(int value)  //konstruktor
        {
            Value = value;
        }
        public int Value { get; }
        public Node Next { get; set;  }
    }
    class LinkedList
    {
        public Node Head { get; set; }
        public void Add(int value)  //přidat prvek do seznamu
        {
            if (Head == null)   //když je seznam prázdný
            {
                Head = new Node(value); //vyrobím nový prvek seznamu
            }   
            else
            {
                Node newNode = new Node(value);
                newNode.Next = Head;
                Head = newNode;
            }
        }
        public bool Find(int value)
        {
            Node node = Head;
            while (node != null)   //dokud nedojedeme na konec seznamu
            {
                if (node.Value == value)
                    return true;
                node = node.Next;
            }
            return false;
        }
        public int Min()
        {
            int min = Head.Next.Value;
            Node node = Head;
            while (node != null)
            {
                if (node.Value < min)
                    min = node.Value;
                node = node.Next;
            }
            return min;
        }
    }
}
