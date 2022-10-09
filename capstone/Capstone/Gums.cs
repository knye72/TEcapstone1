﻿using System;
using System.Collections.Generic;
using System.Text;

namespace Capstone
{
    class Gums
    {
        public string Name { get; set; }
        public string SlotLocation { get; set; }
        public double Price { get; set; }
        public string Type { get; set; }
        public int Quantity { get; set; } = 5;
        public int AmountSold { get; set; } = 0;
        public string Message { get; set; } = "Chew Chew, Yum!";

        public Gums(string name, string slotLocation, double price, string type)
        {
            Name = name;
            SlotLocation = slotLocation;
            Price = price;
            Type = type;
        }

    }
}
