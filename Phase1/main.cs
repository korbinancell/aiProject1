﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Phase1
{
    class main
    {
        static void Main(string[] args)
        {
            int capacity;
            List<ReadCSV.item> knapsack;

            Console.Write("Enter .csv to read: ");
            string filename = Console.ReadLine();

            Phase1 phase1 = new Phase1();
            Phase2 phase2 = new Phase2();
            Phase3 phase3 = new Phase3();
            Phase4 phase4 = new Phase4();
            ReadCSV reader = new ReadCSV();
            Tuple<int, List<ReadCSV.item>> csv = reader.readCSV(filename);

            capacity = csv.Item1;
            knapsack = csv.Item2;

            /*phase1.greedySolutions(capacity, knapsack);
            phase2.exhaustiveSolution(capacity, knapsack);
            phase3.optimized_1_Solution(capacity, knapsack);
            */
            phase4.printSolutions(capacity, knapsack, filename);
        }
    }
}
