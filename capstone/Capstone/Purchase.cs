using System;
using System.Collections.Generic;
using System.Text;

namespace Capstone
{
    public class Purchase
    {
        public double currentBalance { get; set; } = 0;

        public Purchase()
        {

        }

        //Add to Balance
        //Change owed
        //reduce quantity
        //dispense an item
        //purchase menu (1. feed money 2. choose item 3. return to main)


        public double AddToBalance(double amountAdded)
        {
            //Console.WriteLine("How much would you like to add to your balance? Please use full dollar amounts.");
            return currentBalance += amountAdded;
        }


        public string ChangeOwed()
        {
            int nickels = 0;
            int dimes = 0;
            int quarters = 0;

            double temporaryBalance = currentBalance;

            while (temporaryBalance > .25)
            {
                quarters += 1;
                temporaryBalance -= .25;
            }
           while (temporaryBalance > .10)
            {
                dimes += 1;
                temporaryBalance -= .10;
            }
           while (temporaryBalance >= .05)
            {
                nickels += 1;
                temporaryBalance -= .05;
            }


            return $"Your change is {quarters} Quarters, {dimes} Dimes, {nickels} Nickles. Enjoy!";
        }

    }

}

