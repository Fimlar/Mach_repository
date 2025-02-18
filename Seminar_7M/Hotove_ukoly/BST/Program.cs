using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BST
{
    internal class Program
    {
        static void Main(string[] args)
        { // odtud by mělo být přístupné jen to nejdůležitější, žádné vnitřní pomocné implementace.
            // Strom a jeho metody mají fungovat jako černá skříňka, která nám nabízí nějaké úkoly a my se nemusíme starat o to, jakým postupem budou splněny.
            // rozhodně také nechceme mít možnost datovou stukturu nějak měnit jinak, než je dovoleno (třeba nějakým jiným způsobem moct přidat nebo odebrat uzly, aniž by platili invarianty struktury)

            BinarySearchTree<Student> tree = new BinarySearchTree<Student>();

            // čteme data z CSV souboru se studenty (soubor je uložen ve složce projektu bin/Debug u exe souboru)
            // CSV je formát, kdy ukládáme jednotlivé hodnoty oddělené čárkou
            // v tomto případě: Id,Jméno,Příjmení,Věk,Třída
            using (StreamReader streamReader = new StreamReader("studenti_shuffled.csv"))
            {
                string line = streamReader.ReadLine();
                while (line != null)
                {
                    string[] studentData = line.Split(',');

                    Student student = new Student(
                        Convert.ToInt32(studentData[0]),    // Id
                        studentData[1],                     // Jméno
                        studentData[2],                     // Příjmení
                        Convert.ToInt16(studentData[3]),    // Věk
                        studentData[4]);                    // Třída

                    // vložíme studenta do stromu, jako klíč slouží jeho Id
                    tree.Insert(student.Id, student);
                    line = streamReader.ReadLine();
                }
            }
            Console.WriteLine(tree.Find(20).Value);
            Console.WriteLine(tree.Min().Value);
            Student sus = new Student(421, "Lukáš", "Franta", 17, "7.M");
            tree.Insert(sus.Id, sus);
            Console.WriteLine(tree.Find(421).Value);

            for (int i = 0; i < 422; i += 2)
            {
                tree.Remove(i);
            }
            Console.WriteLine(tree.Show());

            Console.ReadLine();
        }
    }
    class Node<T>
    {
        public int Key { get; set; }
        public T Value { get; set; }
        public Node<T> LeftSon { get; set; }
        public Node<T> RightSon { get; set; }

        // konstruktor
        public Node(int key, T value)
        {
            Key = key;
            Value = value;
            LeftSon = null; RightSon = null;
        }
    }
    class BinarySearchTree<T>
    {
        public Node<T> Root { get; set; }

        public string Show()
        {
            // vrací string, abychom nemuseli printit
            string output = "";

            void _show(Node<T> node)
            {
                if (node == null)
                    return;

                // pokračujeme
                _show(node.LeftSon);

                // výpis
                output += node.Key.ToString() + " ";

                _show(node.RightSon);
            }

            if (Root == null)
                return "Strom je prázdný";
            _show(Root);
            return output;
        }

        public Node<T> Find(int key)
        {
            Node<T> _find(Node<T> node, int key2) // privátní funkci mohu založit i uvnitř jiné funkce. Je pak viditelná, jen z té vnější funkce
            {
                if (node == null)
                    return null;
                if (key == node.Key)
                    return node;
                else if (key2 > node.Key)
                    return _find(node.RightSon, key2);
                else
                    return _find(node.LeftSon, key2);
            }
            return _find(Root, key);
        }

        public Node<T> Min()
        {
            Node<T> _min(Node<T> node)
            {
                if (node.LeftSon == null)
                    return node;
                return _min(node.LeftSon);
            }
            return _min(Root);
        }

        public void Insert(int newKey, T newValue)
        {
            Node<T> _insert(Node<T> node, int newKey2, T newValue2)
            {
                if (node.Key < newKey2)
                {
                    if (node.RightSon == null)
                    {
                        Node<T> newNode = new Node<T>(newKey2, newValue2);
                        node.RightSon = newNode;
                        return newNode;
                    }
                    else
                        return _insert(node.RightSon, newKey2, newValue2);
                }

                if (node.Key > newKey2)
                {
                    if (node.LeftSon == null)
                    {
                        Node<T> newNode = new Node<T>(newKey2, newValue2);
                        node.LeftSon = newNode;
                        return newNode;
                    }
                    else
                        return _insert(node.LeftSon, newKey2, newValue2);
                }
                return node;
            }

            if (Root == null)
            {
                Root = new Node<T>(newKey, newValue);
            }
            else
                _insert(Root, newKey, newValue);
        }
        public void Remove(int key)
        {
            Node<T> _remove(Node<T> node, int keyToRemove)
            {
                if (node == null)
                    return null;

                // Najdi uzel, který má být odstraněn
                if (keyToRemove < node.Key)
                {
                    node.LeftSon = _remove(node.LeftSon, keyToRemove);
                }
                else if (keyToRemove > node.Key)
                {
                    node.RightSon = _remove(node.RightSon, keyToRemove);
                }
                else
                {
                    // Případ 1: Uzel nemá potomky (je to list)
                    if (node.LeftSon == null && node.RightSon == null)
                    {
                        return null;
                    }

                    // Případ 2: Uzel má jen jednoho potomka
                    if (node.LeftSon == null)
                    {
                        return node.RightSon;
                    }
                    else if (node.RightSon == null)
                    {
                        return node.LeftSon;
                    }

                    // Případ 3: Uzel má oba potomky
                    // Najdi nejmenší uzel v pravém podstromu (inorder nástupce)
                    Node<T> successor = FindMin(node.RightSon);

                    // Nahraď hodnoty aktuálního uzlu nástupcem
                    node.Key = successor.Key;
                    node.Value = successor.Value;

                    // Odstraň nástupce z pravého podstromu
                    node.RightSon = _remove(node.RightSon, successor.Key);
                }

                return node;
            }
            Node<T> FindMin(Node<T> node)
            {
                while (node.LeftSon != null)
                {
                    node = node.LeftSon;
                }
                return node;
            }

            Root = _remove(Root, key);
        }
    }
    class Student
    {
        public int Id { get; }
        public string FirstName { get; }
        public string LastName { get; }
        public int Age { get; }

        public string ClassName { get; }

        public Student(int id, string firstName, string lastName, int age, string className)
        {
            Id = id;
            FirstName = firstName;
            LastName = lastName;
            Age = age;
            ClassName = className;
        }

        // aby se nám při Console.WriteLine(student) nevypsala jen nějaká adresa v paměti,
        // upravíme výpis objektu typu student na něco čitelného
        public override string ToString()
        {
            return string.Format("{0} {1} (ID: {2}) ze třídy {3}", FirstName, LastName, Id, ClassName);
        }
    }
}
