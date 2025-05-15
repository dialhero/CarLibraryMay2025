using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarLibrary
{
    public class ElectricCar : Car, IEnergy
    {
        public double EnergyLevel { get; set; }
        public double MaxEnergy { get; set; }
        public double KmPerKWh { get; set; }


        public void Refill(double amount)
        {
            EnergyLevel = Math.Min(MaxEnergy, EnergyLevel + amount);
        }


        public void UseEnergy(double km)
        {
            EnergyLevel -= CalculateEnergyUsed(km);
        }


        public double CalculateEnergyUsed(double km) => km / KmPerKWh;


        public override bool CanDrive(double km) => EnergyLevel >= CalculateEnergyUsed(km);


        public override void Drive(double km)
        {
            if (!CanDrive(km)) throw new InvalidOperationException("Not enough battery");
            UseEnergy(km);
            Odometer += (int)km;
        }

        public static ElectricCar FromString(string input)
        {
            var parts = input.Split(',');
            if (parts.Length != 4)
                throw new FormatException("Forkert format på ElectricCar");

            return new ElectricCar
            {
                Brand = parts[0],
                Model = parts[1],
                LicensePlate = parts[2],
                Odometer = int.Parse(parts[3])
            };
        }
    }
}
