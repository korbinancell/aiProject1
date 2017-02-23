using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Phase1
{
    class node
    {
        public string name { get; set; }
        public int itemNum { get; set; }
        public decimal value { get; set; }
        public decimal cost { get; set; }

        public string currentKnapsack { get; set; }
        public decimal currentCost { get; set; }
        public decimal currentValue { get; set; }

        public node(string nm, int inum, decimal cst, decimal val)
        {
            name = nm;
            itemNum = inum;
            value = val;
            cost = cst;

            currentKnapsack = "";
            currentCost = 0;
            currentValue = 0;
        }

        public void setCurrents (string crK, int crC, int crV)
        {
            currentKnapsack = crK;
            currentCost = crC;
            currentValue = crV;
        }

        public override string ToString()
        {
            return name + "," + cost + "," + value + "\n";
        }
    }
}
