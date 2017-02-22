using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Phase1
{
    class Phase1
    {
        private decimal greedySol (int capacity, List<ReadCSV.item> knapsack)
        {
            decimal totalCost = 0, totalVal = 0;

            foreach (var thing in knapsack)
            {
                if ((totalCost += thing.cost) <= capacity)
                    totalVal += thing.value;
                else
                    break;
            }

            return totalVal;
        }

        private decimal partialKnapsack (int capacity, List<ReadCSV.item> knapsack)
        {
            decimal totalCost = 0, totalVal = 0;

            foreach (var thing in knapsack)
            {
                if ((totalCost + thing.cost) <= capacity)
                {
                    totalCost += thing.cost;
                    totalVal += thing.value;
                }

                else
                {
                    totalVal += (thing.value * (capacity - totalCost) / thing.cost);
                    break;
                }
            }

            return totalVal;
        }

        public Tuple<decimal, decimal, decimal, decimal> greedySolutions (int capacity, List<ReadCSV.item> knapsack)
        {
            foreach (var thing in knapsack)
            {
                Console.WriteLine(thing.name + " " + thing.cost + " " + thing.value);
            }

            var descValMax = greedySol(capacity, knapsack.OrderByDescending(x => x.value).ToList());
            var ascCostMax = greedySol(capacity, knapsack.OrderBy(x => x.cost).ToList());
            var descRatioMax = greedySol(capacity, knapsack.OrderByDescending(x => x.value / x.cost).ToList());
            var fracMax = partialKnapsack(capacity, knapsack.OrderByDescending(x => x.value / x.cost).ToList());

            //Console.WriteLine("Highest Values: {0}\nLowest Cost: {1}\nHighest Ratio: {2}\nPartial Knapsack: {3}", descValMax, ascCostMax, descValMax, fracMax);
            return Tuple.Create(descValMax, ascCostMax, descRatioMax, fracMax);
        }

        public decimal minimumSolution(int capacity, List<ReadCSV.item> knapsack)
        {
            var descValMax = greedySol(capacity, knapsack.OrderByDescending(x => x.value).ToList());
            var ascCostMax = greedySol(capacity, knapsack.OrderBy(x => x.cost).ToList());
            var descRatioMax = greedySol(capacity, knapsack.OrderByDescending(x => x.value / x.cost).ToList());
            var fracMax = partialKnapsack(capacity, knapsack.OrderByDescending(x => x.value / x.cost).ToList());

            //Console.WriteLine("Highest Values: {0}\nLowest Cost: {1}\nHighest Ratio: {2}\nPartial Knapsack: {3}", descValMax, ascCostMax, descValMax, fracMax);
            return Math.Min(Math.Min(descValMax, ascCostMax), Math.Min( descRatioMax, fracMax));
        }
    }
}
