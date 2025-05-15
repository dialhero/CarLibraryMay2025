using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarLibrary
{
    public abstract class Car : IDrivable
    {
        public string Brand { get; set; }
        public string Model { get; set; }
        public string LicensePlate { get; set; }
        public int Odometer { get; set; }
        public bool IsEngineRunning { get; set; }



        public abstract void Drive(double km);
        public abstract bool CanDrive(double km);



        public void StartEngine() => IsEngineRunning = true;
        public void StopEngine() => IsEngineRunning = false;

        public override string ToString()

        {
            return $"{Brand},{Model},{LicensePlate},{Odometer}";
        }

        
        
    }
}
