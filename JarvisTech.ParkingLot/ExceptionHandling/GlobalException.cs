namespace JarvisTech.ParkingLot.ExceptionHandling
{
    public class GlobalException : Exception
    {
    }

    public class InvalidVehicleException : Exception
    {
        public InvalidVehicleException(string message) : base(message)
        {

        }
    }
}
