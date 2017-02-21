using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Phase1
{
    class Phase2
    {
        private Tuple<decimal, string> recurseRecurse (int capacity, node[] tree, int currentIndex, string bestKnapsack, decimal bestVal)
        {
            if (currentIndex == tree.Length)
            {
                if (tree[currentIndex].currentCost + tree[currentIndex].cost < capacity && tree[currentIndex].currentValue + tree[currentIndex].value > bestVal)
                    return Tuple.Create(tree[currentIndex].currentValue + tree[currentIndex].value,
                                        bestKnapsack + "\n" + tree[currentIndex].name + " " + Convert.ToString(tree[currentIndex].cost) + " " + Convert.ToString(tree[currentIndex].value));
                else
                    return Tuple.Create(bestVal, bestKnapsack);
            }

            tree[currentIndex + 1].currentValue = tree[currentIndex].currentValue;
            tree[currentIndex + 1].currentCost = tree[currentIndex].currentCost;
            recurseRecurse(capacity, tree, currentIndex + 1, bestKnapsack, bestVal);

            tree[currentIndex + 1].currentValue += tree[currentIndex].value;
            tree[currentIndex + 1].currentCost += tree[currentIndex].cost;
            recurseRecurse(capacity, tree, currentIndex + 1, bestKnapsack +
                                                           "\n" +
                                                           tree[currentIndex].name + " " +
                                                           tree[currentIndex].cost + " " +
                                                           tree[currentIndex].value + " ",
                                                           bestVal);

        }

        public void exhaustiveSolution (int capacity, List<ReadCSV.item> knapsack)
        {
            node[] tree = new node[knapsack.Count+1];

            tree[0] = new node("", 0, 0, 0);

            for (var i =1; i<knapsack.Count; i++)
            {
                tree[i] = new node(knapsack[i].name, i, knapsack[i].cost, knapsack[i].value);
            }

            string returnMe = recurseRecurse()
        }
    }
}
