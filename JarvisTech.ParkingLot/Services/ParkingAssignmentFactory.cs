using JarvisTech.ParkingLot.Common.Models;
using JarvisTech.ParkingLot.ExceptionHandling;

namespace JarvisTech.ParkingLot.Services
{
    public abstract class AssignmentFactory
    {
        public abstract IParkingAssignmentService GetAssignmentService(string vehicleType);
    }

    public class ConcreteParkingAssignmentFactory : AssignmentFactory
    {
        private readonly IServiceProvider _serviceProvider;

        public ConcreteParkingAssignmentFactory(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider ?? throw new ArgumentNullException(nameof(_serviceProvider));
        }

        public override IParkingAssignmentService GetAssignmentService(string vehicleType)
        {
            IParkingAssignmentService parkingAssignmentService;
            switch (vehicleType)
            {
                case "motorcycle" or "scooter":
                    parkingAssignmentService = new MotorCylecParkingAssignmentService(_serviceProvider);
                    return parkingAssignmentService;
                case "car" or "electric suv" or "suv":
                    parkingAssignmentService = new CarParkingAssignmentService(_serviceProvider);
                    return parkingAssignmentService;
                case "truck" or "bus":
                    parkingAssignmentService = new TruckParkingAssignmentService(_serviceProvider);
                    return parkingAssignmentService;
                default:
                    throw new InvalidVehicleException("This Vehicle is Not allowed.");
            }
        }
    }
}
