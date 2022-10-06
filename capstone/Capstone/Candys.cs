using System;
using System.Collections.Generic;
using System.Text;

namespace Capstone
{
    class Candys
    {
        public string Name { get; set; }
        public string SlotLocation { get; set; }
        public double Price { get; set; }
        public string Type { get; set; }
        public int Quantity { get; set; } = 5;

        public Candys(string name, string slotLocation, double price, string type)
        {
            Name = name;
            SlotLocation = slotLocation;
            Price = price;
            Type = type;
        }
    }
}
