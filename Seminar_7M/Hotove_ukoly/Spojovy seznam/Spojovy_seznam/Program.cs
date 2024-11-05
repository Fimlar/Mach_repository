using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace Spojovy_seznam
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Node uzlik = new Node(8);          
            LinkedList list1 = new LinkedList();
            LinkedList list2 = new LinkedList();

            list1.AddLeft(2);
            list1.AddLeft(4);
            list1.AddLeft(3);

            list2.AddLeft(5);
            list2.AddLeft(6);
            list2.AddLeft(4);

            list1.PrintList();
            list2.PrintList();
            LinkedList add = list1.AddTwoNumbers(list1, list2);
            add.PrintList();
            Console.ReadLine();
        }
    }
    class Node
    {
        public Node(int value)  //konstruktor
        {
            Value = value;
        }
        public int Value { get; set;  }
        public Node Next { get; set;  }
    }
    class LinkedList
    {
        public Node Head { get; set; }
        public void AddLeft(int value)  //přidat prvek do seznamu; časová složitost je O(1)
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
        public void AddRight(int value)
        {
            Node newNode = new Node(value);
            if (Head == null)
            {
                Head = newNode;
                return;
            }
            Node current = Head;
            while (current.Next != null)
            {
                current = current.Next;
            }
            current.Next = newNode;
        }

        public bool Find(int value) //časová složitost je O(n), jelikož musíme projít celý seznam
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
        public int Min()    //opět časová složitost O(n), protože zase musíme projít celý seznam
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
        public void PrintList()     //časová složitost je O(n), protože projdu každý prvek seznamu a vytisknu ho
        {
            Node current = Head;
            while (current != null)
            {
                Console.Write(current.Value);
                Console.Write(' ');
                current = current.Next;
            }
            Console.WriteLine();
        }
        public int Len(LinkedList list)
        {
            Node node = Head;
            int len = 0;
            while (node != null)
            {
                len++;
                node = node.Next;
            }
            return len;
        }
        public LinkedList BubbleSort(LinkedList list)   //časová složitost je O(n*n)
        {
            Node current = Head;
            Node previous = Head;
            int bigger = 0;
            int smaller = 0;
            int len = list.Len(list);
            for (int i = 0; i < len; i++)
            {
                while (current.Next != null)
                {
                    if (current.Value > current.Next.Value)
                    {
                        bigger = current.Value;
                        smaller = current.Next.Value;
                        current.Next.Value = bigger;
                        current.Value = smaller;
                    }
                    previous = current;
                    current = current.Next;
                }
            }
            
            return list;
        }
        public void DestructiveIntersection(LinkedList otherList)   //časová složitost je O(n*n), protože hlavní cyklus prochází každý uzel v seznamu jednou, ale ještě je vnořený cyklus, který kontroluje jestli hodnoty už v seznamu nejsou
        {
            Node previous = null;
            Node current = Head;
            while (current != null)
            {
                if (!otherList.Find(current.Value))
                {
                    if (previous == null)
                    {
                        Head = current.Next;
                    }
                    else
                    {
                        previous.Next = current.Next;
                    }
                }
                else
                {
                    Node runner = Head;
                    bool isDuplicate = false;
                    while (runner != current)
                    {
                        if (runner.Value == current.Value)
                        {
                            isDuplicate = true;
                            break;
                        }
                        runner = runner.Next;
                    }

                    if (isDuplicate)
                    {
                        if (previous == null)
                        {
                            Head = current.Next;
                        }
                        else
                        {
                            previous.Next = current.Next;
                        }
                    }
                    else
                    {
                        previous = current; 
                    }
                }
                current = current.Next;
            }
        }
        public void DestructiveUnion(LinkedList otherList)  //časová složitost je O(n*n), kde n je součet délek obou seznamů, protože pro každý uzel v seznamu se díváme na všechny náseldující 
        {
            Node current = Head;
            Node tail = null;
            if (current != null)
            {
                while (current.Next != null)
                {
                    current = current.Next;
                }
                tail = current;
            }
            if (tail != null)
            {
                tail.Next = otherList.Head;
            }
            else
            {
                Head = otherList.Head;
            }
            current = Head;
            while (current != null)
            {
                Node runner = current;
                while (runner.Next != null)
                {
                    if (runner.Next.Value == current.Value)
                    {
                        runner.Next = runner.Next.Next;
                    }
                    else
                    {
                        runner = runner.Next;
                    }
                }
                current = current.Next;
            }
        }
        public LinkedList AddTwoNumbers(LinkedList num1, LinkedList num2)   //časová složitost je O(n), protože procházím naráz všechny prvky obou seznamů
        {
            Node current1 = num1.Head;
            Node current2 = num2.Head;
            LinkedList result = new LinkedList();
            int carry = 0;

            while (current1 != null || current2 != null || carry > 0)
            {
                int sum = carry;

                if (current1 != null)
                {
                    sum += current1.Value;
                    current1 = current1.Next;
                }

                if (current2 != null)
                {
                    sum += current2.Value;
                    current2 = current2.Next;
                }

                carry = sum / 10;
                int digit = sum % 10;

                result.AddRight(digit);
            }

            return result;
        }


    }
}
