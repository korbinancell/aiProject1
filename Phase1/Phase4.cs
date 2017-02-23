using System;
using System.Collections.Generic;

namespace Phase1
{
    class Phase4
    {
        public void printSolutions(int capacity, List<ReadCSV.item> knapsack, string filename)
        {
            node[] tree = new node[knapsack.Count];

            Phase1 phase1 = new Phase1();
            Phase2 phase2 = new Phase2();
            Phase3 phase3 = new Phase3();

            Tuple<decimal, decimal, decimal, decimal> greedySol = phase1.greedySolutions(capacity, knapsack);

            var minSol = phase1.minimumSol(capacity, knapsack);
            var minSol2 = phase1.minimumSolution(capacity, knapsack);
            var maxSol = phase1.maximumSolution(capacity, knapsack);

            var dumbSearch = phase2.exhaustiveSolution(capacity, knapsack);
            var lessDumbSearch = phase3.optimized_1_Solution(capacity, knapsack, minSol2.Item1);

            var txt = System.IO.Path.GetFileNameWithoutExtension(@filename);
            txt += ".txt";

            System.IO.File.WriteAllText(@txt, filename + "\n" +
                                              "Capacity: " + capacity + "\n" +
                                              "Best Greedy Min: " + minSol.Item1 + " " + minSol.Item2 + "\n" +
                                              minSol.Item3 + "\n" +
                                              "Best Greedy Max: " + maxSol.Item1 + " " + maxSol.Item2 + "\n" +
                                              maxSol.Item3 + "\n" +
                                              "Optimal Solution: "+ lessDumbSearch.Item2 + " " + lessDumbSearch.Item1 + "\n" +
                                              lessDumbSearch.Item3 + "\n" +
                                              "Dumb Search: " + dumbSearch.Item4 + "\n" +
                                              "Less Dumb Time: " + lessDumbSearch.Item4);

            Console.WriteLine(dumbSearch.Item3 + "\n" + lessDumbSearch.Item3);
              
        }
    }
}
