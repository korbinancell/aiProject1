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

            Phase1 phase1 = new Phase1();
            Phase2 phase2 = new Phase2();
            ReadCSV reader = new ReadCSV();
            Tuple<int, List<ReadCSV.item>> csv = reader.readCSV("k05.csv");

            capacity = csv.Item1;
            knapsack = csv.Item2;

            //phase1.greedySolutions(capacity, knapsack);
            phase2.exhaustiveSolution(capacity, knapsack);
        }
    }
}
