namespace CarLibrary
{
    public interface IDrivable
    {
        void StartEngine();

        void StopEngine();

        void Drive(double km);

        bool CanDrive(double km);
    }
}
