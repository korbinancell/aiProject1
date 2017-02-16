using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Phase1
{
    struct item
    {
        public string name;
        public int cost;
        public int value;
    }

    class Phase1
    {
        public Tuple<int, List<item>> readCSV(string name)
        {
            int capacity;
            List<item> knapsack = new List<item>();

            string[] lines = System.IO.File.ReadAllLines(@name);

            capacity = Int32.Parse(lines[0]);

            foreach (var line in lines.Skip(1))
            {
                var hold = line.Split(',');
                
                
                item temp;

                temp.name = hold[0];
                temp.cost = Convert.ToInt32(hold[1]);
                temp.value = Convert.ToInt32(hold[2]);

                knapsack.Add(temp);
            }

            return Tuple.Create(capacity, knapsack);
        }

        public int greedySol (int capacity, List<item> knapsack)
        {
            int totalCost = 0, totalVal = 0;

            foreach (var thing in knapsack)
            {
                if ((totalCost += thing.cost) <= capacity)
                    totalVal += thing.value;
                else
                    break;
            }

            return totalVal;
        }

        public double partialKnapsack (int capacity, List<item> knapsack)
        {
            double totalCost = 0, totalVal = 0;

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

        static void Main(string[] args)
        {
            int capacity;
            List<item> knapsack;
            Phase1 phase = new Phase1();
            Tuple<int, List<item>> csv = phase.readCSV("k05.csv");
            
            capacity = csv.Item1;
            knapsack = csv.Item2;

            Console.WriteLine(capacity);

            foreach (var thing in knapsack)
            {
                Console.WriteLine(thing.name +" "+ thing.cost +" "+ thing.value);
            }

            int descValMax = phase.greedySol(capacity, knapsack.OrderByDescending(x => x.value).ToList());
            int ascCostMax = phase.greedySol(capacity, knapsack.OrderBy(x => x.cost).ToList());
            int descRatioMax = phase.greedySol(capacity, knapsack.OrderByDescending(x => x.cost/x.value).ToList());
            double fracMax = phase.partialKnapsack(capacity, knapsack.OrderByDescending(x => x.cost / x.value).ToList());

            Console.WriteLine("Highest Values: {0}\nLowest Cost: {1}\nHighest Ratio: {2}\nPartial Knapsack: {3}", descValMax, ascCostMax, descValMax, fracMax);

        }
    }
}
