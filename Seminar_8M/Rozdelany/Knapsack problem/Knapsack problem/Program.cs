using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Knapsack_problem
{
    internal class Program
    {
        // Je to špatný přístup, ale to vím od začátku
        static void Main(string[] args)
        {
            Knapsack knapsak = new Knapsack(
                               new int[] { 2, 2, 4, 5, 3 },
                               new int[] { 3, 1, 3, 4, 2 },
                               7);

            Backtrack(knapsak, 0);
        }

        static Knapsack Backtrack(Knapsack knapsak, int index)
        {
            if (index == 5)
                return knapsak;
            // Přidám item do batohu
            knapsak.AddItem(index);

            // Zkontroluji, jestli batoh nepřesáhl maximální hmotnost
            if (knapsak.Weight > knapsak.MaxWeight)
            {
                knapsak.RemoveItem(index);
                return Backtrack(knapsak, index + 1);
            }
            else
            {
                return Backtrack(knapsak, index + 1);
            }
        }
    }

    class Knapsack
    {
        public int Price { get; set; }
        public int Weight { get; set; }
        public int[] Items { get; set; }
        private int[] prices;
        private int[] weights;
        public int MaxWeight;

        public Knapsack(int[] _prices, int[] _weights, int _maxWeight)
        {
            prices = _prices;
            weights = _weights;
            MaxWeight = _maxWeight;
            Items = new int[_prices.Count()];
        }

        /// <summary>
        /// Funkce, která přidá věc do batohu
        /// </summary>
        /// <param name="index">Index věci na přídání</param>
        /// <param name="prices">List cen</param>
        /// <param name="weights">List hmotností</param>
        public Knapsack AddItem(int index)
        {
            Items[index] = 1;
            Price += prices[index];
            Weight += weights[index];
        }

        public void RemoveItem(int index)
        {
            Items[index] = 0;
            Price -= prices[index];
            Weight -= weights[index];
        }
    }
}
