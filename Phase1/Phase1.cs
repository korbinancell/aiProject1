using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Phase1
{
    class Phase1
    {
        private Tuple<decimal, decimal, string> greedySol(int capacity, List<ReadCSV.item> knapsack)
        {
            decimal totalCost = 0, totalVal = 0;
            List<ReadCSV.item> picked = new List<ReadCSV.item>();

            foreach (var thing in knapsack)
            {
                if ((totalCost += thing.cost) <= capacity)
                {
                    totalVal += thing.value;
                    picked.Add(thing);
                }
                else
                {
                    totalCost -= thing.cost;
                    break;
                }
            }

            string items = "";
            picked.Sort((x, y) => x.name.CompareTo(y.name));
            foreach (var item in picked)
                items += item.ToString() + "\n";

            return Tuple.Create(totalVal, totalCost, items);
        }

        private Tuple<decimal, decimal, string, bool> partialKnapsack(int capacity, List<ReadCSV.item> knapsack)
        {
            decimal totalCost = 0, totalVal = 0;
            bool flag = false;
            List<ReadCSV.item> picked = new List<ReadCSV.item>();

            foreach (var thing in knapsack)
            {
                if ((totalCost + thing.cost) < capacity)
                {
                    totalCost += thing.cost;
                    totalVal += thing.value;
                    picked.Add(thing);
                }

                else
                {
                    if (totalCost == capacity)
                    {
                        picked.Add(thing);
                        flag = true;
                    }
                    else
                    {
                        totalVal += (thing.value * (capacity - totalCost) / thing.cost);
                        totalCost = capacity;

                        ReadCSV.item partial;
                        partial.name = thing.name + " partial";
                        partial.cost = capacity - totalCost;
                        partial.value = (thing.value * (capacity - totalCost) / thing.cost);
                        picked.Add(partial);
                    }
                    break;
                }
            }

            if (totalCost < capacity)
                flag = true;

            string items = "";
            picked.Sort((x, y) => x.name.CompareTo(y.name));
            foreach (var item in picked)
                items += item.ToString() + "\n";

            return Tuple.Create(totalVal, Convert.ToDecimal(capacity), items, flag);
        }

        public Tuple<decimal, decimal, decimal, decimal> greedySolutions(int capacity, List<ReadCSV.item> knapsack)
        {
            /*
            foreach (var thing in knapsack)
            {
                Console.WriteLine(thing.name + " " + thing.cost + " " + thing.value);
            }
            */

            var descValMax = greedySol(capacity, knapsack.OrderByDescending(x => x.value).ToList());
            var ascCostMax = greedySol(capacity, knapsack.OrderBy(x => x.cost).ToList());
            var descRatioMax = greedySol(capacity, knapsack.OrderByDescending(x => x.value / x.cost).ToList());
            var fracMax = partialKnapsack(capacity, knapsack.OrderByDescending(x => x.value / x.cost).ToList());

            //Console.WriteLine("Highest Values: {0}\nLowest Cost: {1}\nHighest Ratio: {2}\nPartial Knapsack: {3}", descValMax, ascCostMax, descValMax, fracMax);
            return Tuple.Create(descValMax.Item1, ascCostMax.Item1, descRatioMax.Item1, fracMax.Item1);
        }

        public Tuple<decimal, decimal, string> minimumSolution(int capacity, List<ReadCSV.item> knapsack)
        {
            var descValMax = greedySol(capacity, knapsack.OrderByDescending(x => x.value).ToList());
            var ascCostMax = greedySol(capacity, knapsack.OrderBy(x => x.cost).ToList());
            var descRatioMax = greedySol(capacity, knapsack.OrderByDescending(x => x.value / x.cost).ToList());

            //Console.WriteLine("Highest Values: {0}\nLowest Cost: {1}\nHighest Ratio: {2}\nPartial Knapsack: {3}", descValMax, ascCostMax, descValMax, fracMax);
            var test1 = descValMax.Item1 < ascCostMax.Item1 ? descValMax : ascCostMax;
            return descRatioMax.Item1 < test1.Item1 ? descRatioMax : test1;
        }

        public Tuple<decimal, decimal, string> minimumSol(int capacity, List<ReadCSV.item> knapsack)
        {
            var descValMax = greedySol(capacity, knapsack.OrderByDescending(x => x.value).ToList());
            var ascCostMax = greedySol(capacity, knapsack.OrderBy(x => x.cost).ToList());
            var descRatioMax = greedySol(capacity, knapsack.OrderByDescending(x => x.value / x.cost).ToList());

            //Console.WriteLine("Highest Values: {0}\nLowest Cost: {1}\nHighest Ratio: {2}\nPartial Knapsack: {3}", descValMax, ascCostMax, descValMax, fracMax);
            var test1 = descValMax.Item1 > ascCostMax.Item1 ? descValMax : ascCostMax;
            return descRatioMax.Item1 > test1.Item1 ? descRatioMax : test1;
        }

        public Tuple<decimal, decimal, string, bool> maximumSolution(int capacity, List<ReadCSV.item> knapsack)
        {
            return partialKnapsack(capacity, knapsack.OrderByDescending(x => x.value / x.cost).ToList());
        }
    }
}
