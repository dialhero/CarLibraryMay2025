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

        public Taxi()
        {
            
        }


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

        public override object Fromstring(string input)
        {
            return new Taxi();
        }
    }
}
