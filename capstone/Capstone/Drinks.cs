using System;
using System.Collections.Generic;
using System.Text;

namespace Capstone
{
    class Drinks
    {
        public string Name { get; set; }
        public string SlotLocation { get; set; }
        public double Price { get; set; }
        public string Type { get; set; }
        public int Quantity { get; set; } = 5;
        public string Message { get; set; } = "Glug Glug, Yum!";

        public Drinks(string name, string slotLocation, double price, string type)
        {
            Name = name;
            SlotLocation = slotLocation;
            Price = price;
            Type = type;
        }
    }
}
