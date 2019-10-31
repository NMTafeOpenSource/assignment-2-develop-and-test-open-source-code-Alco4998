using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Assesment_1
{
    public class FuelPurchase
    {
        protected double fuelEconomy { get; set; }
        private double liters = 0;
        private double cost = 0;
        
        public void setFuelEconomy()
        {
            this.fuelEconomy = fuelEconomy;
        }

        public void purchaseFuel(double amount, double price)
        {
            this.liters += amount;
            this.cost += price;
        }
    }
}
