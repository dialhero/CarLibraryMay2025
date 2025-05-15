using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarLibrary
{
    public class CarRepository : ICarRepository
    {
        public string FilePath { get; set; }

        public CarRepository(string filepath)
        { FilePath = filepath; }

        public IEnumerable<Car> GetAllCars()
        {
            var cars = new List<Car>();

            if (!File.Exists(FilePath))
                return cars; //Returnerer en tom liste, hvis filen ikke findes

            foreach (var line in File.ReadAllLines(FilePath))
            {
                if (!string.IsNullOrWhiteSpace(line))
                {
                    cars.Add(Car.FromString(line));
                }
            }
            return cars;
        }

        public Car? GetCar(string licensePlate)
        {
            return GetAllCars().FirstOrDefault(c => c.LicensePlate.Equals(licensePlate, StringComparison.OrdinalIgnoreCase));
        }


        public void AddCar(Car car)
        {
            var cars = GetAllCars().ToList();

            if (cars.Any(c => c.LicensePlate.Equals(car.LicensePlate, StringComparison.OrdinalIgnoreCase)))
                throw new InvalidOperationException("En bil med denne nummerplade findes allerede");

            using (var sw = File.AppendText(FilePath))
            {
                sw.WriteLine(car.ToString());
            }
        }

        public void UpdateCar(Car car)
        {
            var cars = GetAllCars().ToList();
            var existingCar = cars.FirstOrDefault(c => c.LicensePlate.Equals(car.LicensePlate, StringComparison.OrdinalIgnoreCase));

            if (existingCar == null)
                throw new InvalidOperationException("Car not found.");

            cars.Remove(existingCar);
            cars.Add(car);

            SaveAllCars(cars);
        }

        public void DeleteCar(string licensePlate)
        {
            var cars = GetAllCars().ToList();
            var carToDelete = cars.FirstOrDefault(c => c.LicensePlate.Equals(licensePlate, StringComparison.OrdinalIgnoreCase));

            if (carToDelete != null)
            {
                cars.Remove(carToDelete);
                SaveAllCars(cars);
            }
        }

        private void SaveAllCars(List<Car> cars)
        {
            using (var sw = new StreamWriter(FilePath, false))
            {
                foreach (var car in cars)
                {
                    sw.WriteLine(car.ToString());
                }
            }
        }

    }
}

