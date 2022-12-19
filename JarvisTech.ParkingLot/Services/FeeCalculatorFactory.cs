using JarvisTech.ParkingLot.ExceptionHandling;
using Microsoft.Extensions.Configuration;

namespace JarvisTech.ParkingLot.Services
{

    public abstract class FeeCalculatorFactory
    {
        public abstract FeeCalculator GetFeeCalculatorService(string vehicleType, string feeModelType);
    }

    public class ConcreteFeeCalculator : FeeCalculatorFactory
    {
        private readonly IConfiguration _configuration;

        public ConcreteFeeCalculator(IConfiguration configuration)
        {
            _configuration = configuration ?? throw new ArgumentNullException(nameof(_configuration));
        }

        public override FeeCalculator GetFeeCalculatorService(string vehicleType, string feeModelType)
        {
            FeeCalculator parkingAssignmentService;
            switch (vehicleType)
            {
                case "mall":
                    parkingAssignmentService = new MallParkingFeeCalculator(_configuration);
                    return parkingAssignmentService;
                case "stadium":
                    parkingAssignmentService = new StadiumParkingFeeCalculator(_configuration);
                    return parkingAssignmentService;
                case "airport":
                    parkingAssignmentService = new AirportParkingFeeCalculator(_configuration);
                    return parkingAssignmentService;
                default:
                    throw new InvalidVehicleException("This Vehicle is Not allowed.");
            }
        }
    }
}
