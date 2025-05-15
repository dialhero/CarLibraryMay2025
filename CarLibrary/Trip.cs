using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarLibrary
{
    public class Trip
    {
        public double Distance { get; }
        public DateTime TripDate { get; }
        public DateTime StartTime { get; }
        public DateTime EndTime { get; }
        public string LicensePlate => Car?.LicensePlate;

        public Car Car { get; }


        public Trip(double distance, DateTime tripDate, DateTime startTime, DateTime endTime, Car car)
        {
            Distance = distance;
            TripDate = tripDate;
            StartTime = startTime;
            EndTime = endTime;
            Car = car;
        }

        public TimeSpan CalculateDuration() => EndTime - StartTime;

        public double CalculateTripPrice(double energyUnitPrice)
        {
            if (Car is Taxi taxi)
            {
                double minutes = CalculateDuration().TotalMinutes;
                return taxi.CalculateFare(Distance, minutes);
            }

            else if (Car is IEnergy energyCar)
            {
                double energyUsed = energyCar.CalculateEnergyUsed(Distance);
                return energyUsed * energyUnitPrice;
            }

            throw new InvalidOperationException("Unsupported car type");
        }

        public void PrintTripDetails()
        {
            Console.WriteLine(ToString());
        }

        public override string ToString()
        {
            return $"{TripDate:yyyy-MM-dd},{StartTime:HH:mm},{EndTime:HH:mm},{Distance},{CalculateDuration().TotalMinutes:F1},{CalculateTripPrice(2.5):F2},{Car}";
        }

        public static Trip FromString(string input, ICarRepository carRepo)
        {
            var parts = input.Split(',');

            if (parts.Length != 5)
                throw new FormatException("Input skal indeholde 5 kommaseparerede værdier");

            DateTime tripDate = DateTime.Parse(parts[0]);
            DateTime start = DateTime.Parse(parts[1]);
            DateTime end = DateTime.Parse(parts[2]);
            double distance = double.Parse(parts[3]);
            string licensePlate = parts[4];

            var car = carRepo.GetCar(licensePlate)
                      ?? throw new InvalidOperationException($"Bil med nr. {licensePlate} ikke fundet");

            return new Trip(distance, tripDate, start, end, car);
        }

    }
}
