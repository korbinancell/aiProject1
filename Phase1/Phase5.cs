﻿using System;
using System.Collections.Generic;
using System.Diagnostics;

namespace Phase1
{
    class Phase5
    {
        private Tuple<decimal, decimal, string> reCurse(int capacity, node[] tree, int currentIndex, string bestKnapsack, decimal bestCost, decimal bestVal, decimal remainingValue, decimal minimumVal, Stopwatch timer)
        {
            if (TimeSpan.Compare(timer.Elapsed, new TimeSpan(0,10,0)) == 1)
                return Tuple.Create(bestCost, bestVal, bestKnapsack);
            if (tree[currentIndex].currentCost > capacity)
                return Tuple.Create(bestCost, bestVal, bestKnapsack);

            if (tree[currentIndex].currentValue + remainingValue < minimumVal)
            {
                Console.WriteLine(tree[currentIndex].currentValue + remainingValue + " " + minimumVal);
                return Tuple.Create(bestCost, bestVal, bestKnapsack);
            }

            if (currentIndex == tree.Length - 1 && tree[currentIndex].currentKnapsack == "")
            {
                return Tuple.Create(bestCost, bestVal, bestKnapsack);
            }

            if (currentIndex == tree.Length - 1)
            {
                if ((tree[currentIndex].currentCost + tree[currentIndex].cost <= capacity) && tree[currentIndex].currentValue + tree[currentIndex].value > bestVal)
                    return Tuple.Create(tree[currentIndex].currentCost + tree[currentIndex].cost,
                                        tree[currentIndex].currentValue + tree[currentIndex].value,
                                        tree[currentIndex].currentKnapsack + tree[currentIndex].ToString());
                else
                    return Tuple.Create(bestCost, bestVal, bestKnapsack);
            }

            if ((tree[currentIndex].currentCost + tree[currentIndex].cost <= capacity) && tree[currentIndex + 1].currentValue + tree[currentIndex].value > bestVal)
            {
                bestVal = tree[currentIndex].currentValue + tree[currentIndex].value;
                bestCost = tree[currentIndex].currentCost + tree[currentIndex].cost;
                bestKnapsack = tree[currentIndex].currentKnapsack + tree[currentIndex].ToString();
            }

            tree[currentIndex + 1].currentValue += tree[currentIndex].value;
            tree[currentIndex + 1].currentCost += tree[currentIndex].cost;
            tree[currentIndex + 1].currentKnapsack += tree[currentIndex].ToString();
            Tuple<decimal, decimal, string> rightChild = reCurse(capacity, tree, currentIndex + 1, bestKnapsack, bestCost, bestVal, remainingValue - tree[currentIndex].value, minimumVal, timer);

            tree[currentIndex + 1].currentValue = tree[currentIndex].currentValue;
            tree[currentIndex + 1].currentCost = tree[currentIndex].currentCost;
            tree[currentIndex + 1].currentKnapsack = tree[currentIndex].currentKnapsack;
            Tuple<decimal, decimal, string> leftChild = reCurse(capacity, tree, currentIndex + 1, bestKnapsack, bestCost, bestVal, remainingValue - tree[currentIndex].value, minimumVal, timer);

            if (rightChild.Item2 > leftChild.Item2 && rightChild.Item1 <= capacity)
                return Tuple.Create(rightChild.Item1, rightChild.Item2, rightChild.Item3);
            else if (leftChild.Item1 <= capacity)
                return Tuple.Create(leftChild.Item1, leftChild.Item2, leftChild.Item3);
            else
                return Tuple.Create(bestCost, bestVal, bestKnapsack);
        }

        public Tuple<decimal, decimal, string, string> optimized_1_Solution(int capacity, List<ReadCSV.item> knapsack, decimal minimumSolution)
        {
            /*  
             *  sorting cost vs value
             *  
             *  Cut out after 10 min
             */
            Phase1 phase1 = new Phase1();
            Stopwatch timer = new Stopwatch();

            timer.Start();
            var tup = phase1.maximumSolution(capacity, knapsack);
            timer.Stop();

            if(tup.Item4)
                return Tuple.Create(tup.Item1, tup.Item2, tup.Item3, Convert.ToString(timer.Elapsed));

            timer.Reset();

            decimal remainingValue = 0;
            node[] tree = new node[knapsack.Count];

            for (var i = 0; i < knapsack.Count; i++)
            {
                tree[i] = new node(knapsack[i].name, i, knapsack[i].cost, knapsack[i].value);
                remainingValue += knapsack[i].value;
            }

            timer.Start();
            var timeMe = reCurse(capacity, tree, 0, "", 0, 0, remainingValue, minimumSolution, timer);
            if(timer.IsRunning)
                timer.Stop();

            return Tuple.Create(timeMe.Item1, timeMe.Item2, timeMe.Item3, Convert.ToString(timer.Elapsed));
        }
    }
}
