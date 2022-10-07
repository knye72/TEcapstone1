using System;
using System.Collections.Generic;
using System.Text;

namespace Capstone
{
    public class Chips
    {
        public string Name { get; set; }
        public string SlotLocation { get; set; }
        public double Price { get; set; }
        public string Type { get; set; }
        public int Quantity { get; set; } = 5;
        public string Message { get; set; } = "Crunch Crunch, Yum!";

        public Chips(string name, string slotLocation, double price, string type)
        {
            Name = name;
            SlotLocation = slotLocation;
            Price = price;
            Type = type;
        }

    }
}
