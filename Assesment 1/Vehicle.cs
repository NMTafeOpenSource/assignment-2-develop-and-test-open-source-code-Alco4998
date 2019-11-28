using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Assesment_1
{
    public class Vehicle
    {
        public string       manufacturer     { get; set; }
        public string       model            { get; set; }
        public int          makeyear         { get; set; }
        public string       regNumb          { get; set; }
        public int          distanceTraveled { get; set; }
        public int          tankCapacity     { get; set; }
        public FuelPurchase fuelPurchase     { get; set; }

        /*
         * Because i have had a brainfart and can't remeber how C# does its object constructors
         */
        public Vehicle(string manufacturer, string model, int makeyear, string regNumb, int distanceTraveled, int tankCapacity)
        {
            this.manufacturer = manufacturer;
            this.model = model;
            this.makeyear = makeyear;
            this.regNumb = regNumb;
            this.distanceTraveled = distanceTraveled;
            this.tankCapacity = tankCapacity;
        }

        //public void printDetails() { } To do, because it will be handled differently

        public void addDistance(int distance)
        {
            distanceTraveled += distance;
        }

        public void addFuel(double liters, double price)
        {
            fuelPurchase.purchaseFuel(liters, price);
        }
    }
}