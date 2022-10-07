using System;
using System.Collections.Generic;
using System.IO;
using Capstone;

namespace Capstone
{
    
    public class Program
    {
        static Dictionary<string, dynamic> VendingMachineItems = new Dictionary<string, dynamic>();
        static Purchase purchase = new Purchase();


        public static void Main(string[] args)
        {

            // Make the Dictionary
            // Finding the File
            string filePath = @"C:\Users\Student\git\c-sharp-minicapstonemodule1-team3\c-sharp-minicapstonemodule1-team3\capstone\Capstone\bin\Debug\netcoreapp3.1";
            string fileName = "vendingmachine.csv";
            string fullPath = Path.Combine(filePath, fileName);
            // Reading the File
            using (StreamReader reader = new StreamReader(fullPath))
            {
                while (!reader.EndOfStream)
                {
                    string currentLine = reader.ReadLine();
                    if (currentLine.Contains("Chip"))
                    {
                        string[] lineHolder = currentLine.Split('|');
                        Chips item  = new Chips(lineHolder[1], lineHolder[0], Double.Parse(lineHolder[2]), lineHolder[3]);
                        VendingMachineItems.Add(lineHolder[0], item);
                    }
                    else if (currentLine.Contains("Candy"))
                    {
                        string[] lineHolder = currentLine.Split('|');
                        Candys item = new Candys(lineHolder[1], lineHolder[0], Double.Parse(lineHolder[2]), lineHolder[3]);
                        VendingMachineItems.Add(lineHolder[0], item);
                    }
                    else if (currentLine.Contains("Drink"))
                    {
                        string[] lineHolder = currentLine.Split('|');
                        Drinks item = new Drinks(lineHolder[1], lineHolder[0], Double.Parse(lineHolder[2]), lineHolder[3]);
                        VendingMachineItems.Add(lineHolder[0], item);
                    }
                    else if (currentLine.Contains("Gum"))
                    {
                        string[] lineHolder = currentLine.Split('|');
                        Gums item = new Gums(lineHolder[1], lineHolder[0], Double.Parse(lineHolder[2]), lineHolder[3]);
                        VendingMachineItems.Add(lineHolder[0], item);
                    }

                }
            }
            MainMenu();




            /*Console.WriteLine("Welcome to the Vending Machine!");
            Console.WriteLine("Choose one.");
            Console.WriteLine("(1) Display Vending Machine Items");
            Console.WriteLine("(2) Purchase");
            Console.WriteLine("(3) Exit ");

            string choice = Console.ReadLine();

            if (choice == "1")
            {
                foreach (string thing in VendingMachineItems.Keys)
                {
                    Console.WriteLine(VendingMachineItems[thing].Name + " " + VendingMachineItems[thing].Price + " " + VendingMachineItems[thing].Quantity);

                }
            }
*/


        }
        public static void MainMenu()
        {
            Console.WriteLine("Welcome to the Vending Machine!");
            Console.WriteLine("Choose one.");
            Console.WriteLine("(1) Display Vending Machine Items");
            Console.WriteLine("(2) Purchase");
            Console.WriteLine("(3) Exit ");

            
            string choice = Console.ReadLine();

            if (choice == "1")
            {
                ShowInventory();
            }
            if (choice == "2")
            {
                PurchaseMenu();
            }

            
        }
        public static void PurchaseMenu()
        {
            Console.WriteLine($"Current money provided: {purchase.currentBalance}");
            Console.WriteLine("1) Feed Money");
            Console.WriteLine("2) Purchase Item");
            Console.WriteLine("3) Finish Transaction");
            string purchaseMenuChoice = Console.ReadLine();


            if (purchaseMenuChoice == "1")
            {
                Console.WriteLine("How many whole dollars would you like to add to your balance? Please use numbers and not words");
                double moneyToAdd = double.Parse(Console.ReadLine());

                //if they don't do a number we hit htem with a message like "hey asshole, we said to use a number. try again."

                purchase.AddToBalance(moneyToAdd);
                PurchaseMenu();
            }
            if(purchaseMenuChoice == "2")
            {
                if(purchase.currentBalance == 0)
                {
                    Console.WriteLine("You need to add money before you make a purchase, you freeloading hippie.");
                    PurchaseMenu();
                }
                foreach (string thing in VendingMachineItems.Keys)
                {
                    Console.WriteLine(VendingMachineItems[thing].SlotLocation + " " + VendingMachineItems[thing].Name + " " + VendingMachineItems[thing].Price + " " + VendingMachineItems[thing].Quantity);

                }
                Console.WriteLine("Choose the item you want to buy");
                string buyerChoice = Console.ReadLine();
                if(purchase.currentBalance < VendingMachineItems[buyerChoice].Price || purchase.currentBalance - VendingMachineItems[buyerChoice].Price < 0)
                {
                    Console.WriteLine("You need to add more money, you freeloading hippie.");
                    PurchaseMenu();
                }
                Console.WriteLine($"You chose {VendingMachineItems[buyerChoice].Name}");
                VendingMachineItems[buyerChoice].Quantity -= 1;
                purchase.currentBalance -= VendingMachineItems[buyerChoice].Price;
                PurchaseMenu();
            }
            if(purchaseMenuChoice == "3")
            {
                Console.WriteLine("So long, sucka.");
                MainMenu();
            }
        }

        public static void ShowInventory()
        {
            foreach (string thing in VendingMachineItems.Keys)
            {
                Console.WriteLine(VendingMachineItems[thing].SlotLocation + " " + VendingMachineItems[thing].Name + " " + VendingMachineItems[thing].Price + " " + VendingMachineItems[thing].Quantity);

            }
            // want to be able to access choices from here


        }



    }
}
