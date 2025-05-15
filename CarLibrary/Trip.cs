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

       

    }
}
