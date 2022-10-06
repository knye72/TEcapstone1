using System;
using System.Collections.Generic;
using System.IO;
using Capstone;

namespace Capstone
{
    public class Program
    {
        public static void Main(string[] args)
        {
            //MainMenu();
            // Make the Dictionary
            Dictionary<string, dynamic> VendingMachineItems = new Dictionary<string, dynamic>();
            // Finding the File
            string filePath = @"C:\Users\Student\git\c-sharp-minicapstonemodule1-team3\capstone\Capstone\bin\Debug\netcoreapp3.1";
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

            //foreach (string thing in VendingMachineItems.Keys)
            //{
            //    Console.WriteLine(VendingMachineItems[thing].Name + " " + VendingMachineItems[thing].Price + " " + VendingMachineItems[thing].Quantity);

            //}


            Console.WriteLine("Welcome to the Vending Machine!");
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

            

        }
        static void MainMenu()
        {
            Console.WriteLine("Welcome to the Vending Machine!");
            Console.WriteLine("Choose one.");
            Console.WriteLine("(1) Display Vending Machine Items");
            Console.WriteLine("(2) Purchase");
            Console.WriteLine("(3) Exit ");

            string choice = Console.ReadLine();
            // want to be able to access choices from here

            
        }



    }
}
