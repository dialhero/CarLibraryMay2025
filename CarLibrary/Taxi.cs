using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarLibrary
{
    public class Taxi : Car, IEnergy
    {
        private IEnergy _energySource;
        private bool _meterStarted;
        public double StartPrice { get; set; }
        public double PricePerKm { get; set; }
        public double PricePerMinute { get; set; }

        
        public Taxi(IEnergy energySource)
        {
            _energySource = energySource;
        }


        public double EnergyLevel
        {
            get => _energySource.EnergyLevel;
            set => _energySource.EnergyLevel = value;
        }


        public double MaxEnergy
        {
            get => _energySource.MaxEnergy;
            set => _energySource.MaxEnergy = value;
        }

        public void Refill(double amount) => _energySource.Refill(amount);

        public void UseEnergy(double km) => _energySource.UseEnergy(km);

        public double CalculateEnergyUsed(double km) => _energySource.CalculateEnergyUsed(km);

        public override bool CanDrive(double km) => _meterStarted && _energySource.EnergyLevel >= _energySource.CalculateEnergyUsed(km);

        public override void Drive(double km)

        {
            if (!_meterStarted)
                throw new InvalidOperationException("Meter not started");
            if (!CanDrive(km))
                throw new InvalidOperationException("Not enough energy");
            _energySource.UseEnergy(km);
            Odometer += (int)km;
        }



        public void StartMeter() => _meterStarted = true;
        public void StopMeter() => _meterStarted = false;


        public double CalculateFare(double km, double minutes)
        {
            return StartPrice + (km * PricePerKm) + (minutes * PricePerMinute);
        }


        public override string ToString()
        {
            return base.ToString() + ",Taxi";
        }

        public static Taxi FromString(string input)
        {
            var parts = input.Split(',');
            if (parts.Length != 6 || parts[0] != "Taxi")
                throw new FormatException("Forkert format på Taxi");

            var energyType = parts[1];

            Car energyCar = energyType switch
            {
                "FuelCar" => FuelCar.FromString(string.Join(',', parts.Skip(1))),
                "ElectricCar" => ElectricCar.FromString(string.Join(',', parts.Skip(1))),
                _ => throw new FormatException("Ukendt energikilde for Taxi")
            };

            var taxi = new Taxi((IEnergy)energyCar)
            {

                Brand = energyCar.Brand,
                Model = energyCar.Model,
                LicensePlate = energyCar.LicensePlate,
                Odometer = energyCar.Odometer
            };

            return taxi;
        }
    }
}
