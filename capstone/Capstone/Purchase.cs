using System;
using System.Collections.Generic;
using System.Text;

namespace Capstone
{
    public class Purchase
    {
        public double currentBalance { get; set; } = 0;
        public double amountAdded { get; set; } 

        public Purchase()
        {

        }

        public double AddToBalance()
        {
            return currentBalance += amountAdded;
        }



    }
}
