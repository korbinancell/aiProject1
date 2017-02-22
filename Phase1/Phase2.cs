using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Phase1
{
    class Phase2
    {
        private Tuple<decimal, decimal, string> recurse(int capacity, node[] tree, int currentIndex, string bestKnapsack, decimal bestCost, decimal bestVal)
        {
            if (currentIndex == tree.Length - 1 && tree[currentIndex].currentKnapsack == "")
            {
                return Tuple.Create(bestCost, bestVal, bestKnapsack);
            }

            string newKnapsack = "\n" + tree[currentIndex].name + " " + Convert.ToString(tree[currentIndex].cost) + " " + Convert.ToString(tree[currentIndex].value);

            if (currentIndex == tree.Length - 1)
            {
                if ((tree[currentIndex].currentCost + tree[currentIndex].cost <= capacity) && tree[currentIndex].currentValue + tree[currentIndex].value > bestVal)
                    return Tuple.Create(tree[currentIndex].currentCost + tree[currentIndex].cost,
                                        tree[currentIndex].currentValue + tree[currentIndex].value,
                                        tree[currentIndex].currentKnapsack + newKnapsack);
                else
                    return Tuple.Create(bestCost, bestVal, bestKnapsack);
            }

            if ((tree[currentIndex].currentCost + tree[currentIndex].cost <= capacity) && tree[currentIndex + 1].currentValue + tree[currentIndex].value > bestVal)
            {
                bestVal = tree[currentIndex].currentValue + tree[currentIndex].value;
                bestCost = tree[currentIndex].currentCost + tree[currentIndex].cost;
                bestKnapsack = tree[currentIndex].currentKnapsack + newKnapsack;
            }

            tree[currentIndex + 1].currentValue += tree[currentIndex].value;
            tree[currentIndex + 1].currentCost += tree[currentIndex].cost;
            tree[currentIndex + 1].currentKnapsack += newKnapsack;
            Tuple<decimal, decimal, string> rightChild = recurse(capacity, tree, currentIndex + 1, bestKnapsack, bestCost, bestVal);

            tree[currentIndex + 1].currentValue = tree[currentIndex].currentValue;
            tree[currentIndex + 1].currentCost = tree[currentIndex].currentCost;
            tree[currentIndex + 1].currentKnapsack = tree[currentIndex].currentKnapsack;
            Tuple<decimal, decimal, string> leftChild = recurse(capacity, tree, currentIndex + 1, bestKnapsack, bestCost, bestVal);

            if (rightChild.Item2 > leftChild.Item2 && rightChild.Item1 <= capacity)
                return Tuple.Create(rightChild.Item1, rightChild.Item2, rightChild.Item3);
            else if ( leftChild.Item1 <= capacity )
                return Tuple.Create(leftChild.Item1, leftChild.Item2, leftChild.Item3);
            else
                return Tuple.Create(bestCost, bestVal, bestKnapsack);
        }

        public void exhaustiveSolution (int capacity, List<ReadCSV.item> knapsack)
        {
            node[] tree = new node[knapsack.Count];

            for (var i = 0; i < knapsack.Count; i++)
            {
                tree[i] = new node(knapsack[i].name, i, knapsack[i].cost, knapsack[i].value);
            }

            Tuple<decimal, decimal, string> hopefully = recurse(capacity, tree, 0, "", 0, 0);

            Console.WriteLine(hopefully.Item1 + "\n" + hopefully.Item2 + "\n" + hopefully.Item3 + "\n capacity: " + capacity);
        }
    }
}
