using System;
using System.Collections.Generic;
using System.IO;
using Capstone;

namespace Capstone
{
    
    public class Program
    {   //We're keeping all our things here to make them accessible by the rest of the code while being outside of methods
        static Dictionary<string, dynamic> VendingMachineItems = new Dictionary<string, dynamic>();
        static Purchase purchase = new Purchase();
        static List<string> logList = new List<string>();

        public static void Main(string[] args)
        {
            //We have these two methods we're pulling from that do all the work 
            ReadFile();
            MainMenu();


        }
        public static void MainMenu()
        {
            //This is the method that makes the first menu that the customer sees
            Console.WriteLine("Welcome to the Vending Machine!");
            Console.WriteLine("Choose one.");
            Console.WriteLine("(1) Display Vending Machine Items");
            Console.WriteLine("(2) Purchase");
            Console.WriteLine("(3) Exit ");
            //Now we want the user to input something
            string choice = Console.ReadLine();
            //We're making sure that the user's input is valid 
            if (choice != "1" && choice != "2" && choice != "3" && choice != "4")
            {
                Console.WriteLine("Please input a valid choice");
                MainMenu();
            }
            //This choice shows them the Inventory and then allows them to make another choice
            else if (choice == "1")
            {
                ShowInventory();
                MainMenu();
            }
            //This choice sends them to the purchase menu
            else if (choice == "2")
            {
                PurchaseMenu();
            }
            //This choice exits the program
            else if (choice == "3")
            {
                return; 
            }
            //This choice prints out the Sales Report
            else if (choice == "4")
            {
                PrintSalesReport();
            }
        }

        public static void PurchaseMenu()
        {
            //This is the method that lets the user make purchases
            Console.WriteLine($"Current money provided: {((purchase.currentBalance).ToString("C2"))}");
            Console.WriteLine("1) Feed Money");
            Console.WriteLine("2) Purchase Item");
            Console.WriteLine("3) Finish Transaction");
            //This is where we're going to ask for input 
            string purchaseMenuChoice = Console.ReadLine();
            //Making sure that the input is valid
            if (purchaseMenuChoice != "1" && purchaseMenuChoice != "2" && purchaseMenuChoice != "3")
            {
                Console.WriteLine("Please input a valid choice");
                PurchaseMenu();
            }
            //This first choice is for sending in money
            else if (purchaseMenuChoice == "1")
            {
                //We're going to use a try/catch to make sure that the user's input is valid, and not letters or anything goofy 
                try
                {
                    Console.WriteLine("How many whole dollars would you like to add to your balance? Please use numbers and not words");
                    double moneyToAdd = double.Parse(Console.ReadLine());
                    purchase.AddToBalance(moneyToAdd);
                    logList.Add(DateTime.Now + " " + "FEED MONEY: " + moneyToAdd + " " + (purchase.currentBalance).ToString("C2"));
                    PurchaseMenu();
                }

                catch(FormatException)
                {
                    //if they type anything other than what we want, they're going to be told that their input isn't valid and sent back out 
                    Console.WriteLine("Please toss in an actual number");
                    PurchaseMenu();
                }
                
            }
            else if (purchaseMenuChoice == "2")
            {
                //This is the choice where they actually make a purchase
                if (purchase.currentBalance == 0)
                {
                    //checking to make sure they have enough money
                    Console.WriteLine("You need to add money before you make a purchase, you freeloading hippie.");
                    PurchaseMenu();
                }
                foreach (string thing in VendingMachineItems.Keys)
                {
                    //this is where we print out the current inventory for them to see
                    Console.WriteLine(VendingMachineItems[thing].SlotLocation + " " + VendingMachineItems[thing].Name + " " + (VendingMachineItems[thing].Price).ToString("C2") + " " + "Quantity Remaining: " + VendingMachineItems[thing].Quantity);

                }
                Console.WriteLine("Choose the item you want to buy");
                string buyerChoice = Console.ReadLine();
                buyerChoice = buyerChoice.ToUpper(); // making sure that we can read their input no matter what it looks like 
                if (!VendingMachineItems.ContainsKey(buyerChoice))
                {
                    //this makes sure that their input is valid 
                    Console.WriteLine("Please make a valid selection");
                    PurchaseMenu();
                }
                if (purchase.currentBalance < VendingMachineItems[buyerChoice].Price || purchase.currentBalance - VendingMachineItems[buyerChoice].Price < 0)
                {
                    //if a purchase would take them into the negative, we stop them from being able to do it 
                    Console.WriteLine("You need to add more money, you freeloading hippie.");
                    PurchaseMenu();
                }
                if (VendingMachineItems[buyerChoice].Quantity == 0)
                {
                    //this lets the user know that an item is sold out
                    Console.WriteLine("SOLD OUT");
                    PurchaseMenu();
                }

                Console.WriteLine(VendingMachineItems[buyerChoice].Message); //this prints the message associated with the item being bought
                VendingMachineItems[buyerChoice].Quantity -= 1; //we're decreasing the quantity 
                VendingMachineItems[buyerChoice].AmountSold += 1; //we're increasing the amoutn sold (for the Sales Report)
                purchase.currentBalance -= VendingMachineItems[buyerChoice].Price; //decreasing the balance by the cost of the item(s)
                Console.WriteLine($"You chose {VendingMachineItems[buyerChoice].Name}, it cost {(VendingMachineItems[buyerChoice].Price).ToString("C2")}, your remaining balance is {(purchase.currentBalance).ToString("C2")}."); //telling the user what they bought
                logList.Add(DateTime.Now + " " + VendingMachineItems[buyerChoice].Name + " " + (VendingMachineItems[buyerChoice].Price).ToString("C2") + " " + (purchase.currentBalance).ToString("C2")); //adding the transaction to the List
                PurchaseMenu(); //then we send them back to the purchase menu
            }
            else if (purchaseMenuChoice == "3")
            {
                // this is the one where we finalize our purchases 
                Console.WriteLine("So long, sucka."); //truly a poingant sendoff message 
                Console.WriteLine(purchase.ChangeOwed()); //this is where we're sending them the amount of change they're getting 
                logList.Add(DateTime.Now + " " + "GIVE CHANGE: " + purchase.ChangeOwed() + " " + (purchase.currentBalance).ToString("C2")); //then we add that to the log
                string pathToSecondFile = Environment.CurrentDirectory; //path to our log file
                string secondFile = "Log.txt"; //name of our log file 
                string areWeThereYet = Path.Combine(pathToSecondFile, secondFile); //full path to our log file
                using (StreamWriter writer = new StreamWriter(areWeThereYet)) //this is where we start up the StreamWriter, because we only want to start it once everything else is done
                {
                    foreach (string line in logList)
                    {
                        writer.WriteLine(line); //we're writing the log list to a log file
                    }
                }
                purchase.currentBalance = 0; //we then reset the balance to 0

                MainMenu();
            }
        }

        public static void ShowInventory()
        {//this is where we shoe the inventory 
            foreach (string thing in VendingMachineItems.Keys)
            {
                Console.WriteLine(VendingMachineItems[thing].SlotLocation + " " + VendingMachineItems[thing].Name + " " + $"{(VendingMachineItems[thing].Price).ToString("C2")}" + " " + "Quantity Available: " + VendingMachineItems[thing].Quantity);

            }
        }

        public static void ReadFile()
        {
            // Make the Dictionary for the inventory
            // Finding the File to read from
            string filePath = @"C:\Users\Student\git\c-sharp-minicapstonemodule1-team3\capstone\Capstone\bin\Debug\netcoreapp3.1";
            string fileName = "vendingmachine.csv";
            string fullPath = Path.Combine(filePath, fileName);
            // Reading the File
            using (StreamReader reader = new StreamReader(fullPath))
            {
                while (!reader.EndOfStream)
                {
                    //this entire thing reads the text file and pulls all of that data into objects based on type, so they can be
                    // easily accessed by the rest of our program 
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
        {//this is where we print the Sales Report
            double runningTotal = 0; //we'll be holding the total price here 
            foreach (string thing in VendingMachineItems.Keys)
            {
                Console.WriteLine(VendingMachineItems[thing].Name + "|"+ VendingMachineItems[thing].AmountSold);
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
