using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarLibrary
{
    public class FuelCar : Car, IEnergy
    {
        public double EnergyLevel { get; set; }
        public double MaxEnergy { get; set; }
        public double KmPerLiter { get; set; }

        public void Refill(double amount)
        {
            EnergyLevel = Math.Min(MaxEnergy, EnergyLevel + amount);
        }

        public void UseEnergy(double km)
        {
            EnergyLevel -= CalculateEnergyUsed(km);
        }

        public double CalculateEnergyUsed(double km) => km / KmPerLiter;

        public override bool CanDrive(double km) => EnergyLevel >= CalculateEnergyUsed(km);

        public override void Drive(double km)

        {
            if (!CanDrive(km)) throw new InvalidOperationException("Not enough fuel");
            UseEnergy(km);
            Odometer += (int)km;
        }

        public override string ToString()
        {
            return $"FuelCar,{base.ToString()}";
        }


        public static FuelCar FromString(string input)
        {
            var parts = input.Split(',');
            if (parts.Length != 5 || parts[0] != "FuelCar")
                throw new FormatException("Forkert format på FuelCar");

            return new FuelCar
            {
                Brand = parts[1],
                Model = parts[2],
                LicensePlate = parts[3],
                Odometer = int.Parse(parts[4])
            };
        }

    }
}
