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
        static List<string> logList = new List<string>();

        public static void Main(string[] args)
        {
            ReadFile();
            MainMenu();


        }
        public static void MainMenu()
        {
            Console.WriteLine("Welcome to the Vending Machine!");
            Console.WriteLine("Choose one.");
            Console.WriteLine("(1) Display Vending Machine Items");
            Console.WriteLine("(2) Purchase");
            Console.WriteLine("(3) Exit ");

            
            string choice = Console.ReadLine();

            if (choice != "1" && choice != "2" && choice != "3" && choice != "4")
            {
                Console.WriteLine("Please input a valid choice");
                MainMenu();
            }

            else if (choice == "1")
            {
                ShowInventory();
                MainMenu();
            }
            else if (choice == "2")
            {
                PurchaseMenu();
            }
            else if (choice == "3")
            {
                return; 
            }
            else if (choice == "4")
            {
                PrintSalesReport();
            }


        }
        public static void PurchaseMenu()
        {
            
            Console.WriteLine($"Current money provided: {purchase.currentBalance}");
            Console.WriteLine("1) Feed Money");
            Console.WriteLine("2) Purchase Item");
            Console.WriteLine("3) Finish Transaction");
            string purchaseMenuChoice = Console.ReadLine();

            if (purchaseMenuChoice != "1" && purchaseMenuChoice != "2" && purchaseMenuChoice != "3")
            {
                Console.WriteLine("Please input a valid choice");
                PurchaseMenu();
            }


            else if (purchaseMenuChoice == "1")
            {

                try
                {
                    Console.WriteLine("How many whole dollars would you like to add to your balance? Please use numbers and not words");
                    double moneyToAdd = double.Parse(Console.ReadLine());
                    purchase.AddToBalance(moneyToAdd);
                    logList.Add(DateTime.Now + " " + "FEED MONEY: " + moneyToAdd + " " + purchase.currentBalance);
                    PurchaseMenu();
                }

                catch(FormatException)
                {
                    Console.WriteLine("Please toss in an actual number");
                    PurchaseMenu();
                }
                
            }
            else if (purchaseMenuChoice == "2")
            {
                if (purchase.currentBalance == 0)
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
                buyerChoice = buyerChoice.ToUpper();
                if (!VendingMachineItems.ContainsKey(buyerChoice))
                {
                    Console.WriteLine("Please make a valid selection");
                    PurchaseMenu();
                }
                if (purchase.currentBalance < VendingMachineItems[buyerChoice].Price || purchase.currentBalance - VendingMachineItems[buyerChoice].Price < 0)
                {
                    Console.WriteLine("You need to add more money, you freeloading hippie.");
                    PurchaseMenu();
                }
                if (VendingMachineItems[buyerChoice].Quantity == 0)
                {
                    Console.WriteLine("SOLD OUT");
                    PurchaseMenu();
                }

                Console.WriteLine(VendingMachineItems[buyerChoice].Message);
                VendingMachineItems[buyerChoice].Quantity -= 1;
                VendingMachineItems[buyerChoice].AmountSold += 1;
                purchase.currentBalance -= VendingMachineItems[buyerChoice].Price;
                Console.WriteLine($"You chose {VendingMachineItems[buyerChoice].Name}, it cost {VendingMachineItems[buyerChoice].Price}, your remaining balance is {purchase.currentBalance}.");
                logList.Add(DateTime.Now + " " + VendingMachineItems[buyerChoice].Name + " " + VendingMachineItems[buyerChoice].Price + " " + purchase.currentBalance);
                PurchaseMenu();
            }
            else if (purchaseMenuChoice == "3")
            {
                Console.WriteLine("So long, sucka.");
                Console.WriteLine(purchase.ChangeOwed());
                logList.Add(DateTime.Now + " " + "GIVE CHANGE: " + purchase.ChangeOwed() + " " + purchase.currentBalance);
                string pathToSecondFile = Environment.CurrentDirectory;
                string secondFile = "Log.txt";
                string areWeThereYet = Path.Combine(pathToSecondFile, secondFile);
                using (StreamWriter writer = new StreamWriter(areWeThereYet))
                {
                    foreach (string line in logList)
                    {
                        writer.WriteLine(line);
                    }
                }
                purchase.currentBalance = 0;

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

        public static void ReadFile()
        {
            // Make the Dictionary for the inventory
            // Finding the File to read from
            string filePath = @"C:\Users\Sam\git\c-sharp-minicapstonemodule1-team3\capstone\Capstone\bin\Debug\netcoreapp3.1";
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
                        Chips item = new Chips(lineHolder[1], lineHolder[0], Double.Parse(lineHolder[2]), lineHolder[3]);
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
        }

        public static void PrintSalesReport()
        {
            double runningTotal = 0;
            foreach (string thing in VendingMachineItems.Keys)
            {
                Console.WriteLine(VendingMachineItems[thing].Name + " "+ VendingMachineItems[thing].AmountSold);
                if (VendingMachineItems[thing].AmountSold > 0)
                {
                    runningTotal += (VendingMachineItems[thing].AmountSold) * (VendingMachineItems[thing].Price);
                }

            }
            Console.WriteLine();
            Console.WriteLine("TOTAL SALES: $"+ runningTotal);
            MainMenu();
        }



    }
}
