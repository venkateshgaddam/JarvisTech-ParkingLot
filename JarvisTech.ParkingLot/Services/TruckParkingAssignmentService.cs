using JarvisTech.ParkingLot.Common.Models;
using JarvisTech.ParkingLot.Repository;

namespace JarvisTech.ParkingLot.Services
{
    public class TruckParkingAssignmentService : IParkingAssignmentService
    {
        private readonly IRepository _repository;
        public TruckParkingAssignmentService(IServiceProvider serviceProvider)
        {
            _repository = serviceProvider.GetRequiredService<IRepository>();
        }

        public async Task<Spot> GetCurrentParkingSpotDetails(int parkingLotId, int spotId)
        {
            var result = await _repository.GetCurrentParkingSpotAsync(parkingLotId, spotId, VehicleType.MotorCycle.ToString());
            return result;
        }


        public async Task<int> GetAvailableParkingSpots(int parkingLotId)
        {
            var result = await _repository.GetTruckAvailableSpots(parkingLotId);
            return result;
        }

        public async Task<bool> ReserveParkingSpot(int parkingLotId, Spot parkingSpot)
        {
            var isReserved = await _repository.ReserveParkingSpotAsync(parkingLotId, parkingSpot);
            return isReserved;
        }

        public async Task<bool> ReleaseParkingSpot(int parkingLotId, Spot parkingSpot)
        {
            var result = await _repository.ReleaseParkingSpotAsync(parkingLotId, parkingSpot);
            return result;
        }

        public async Task<Spot> GetLatestAvailableSpot(int parkingLotId)
        {
            var result = await _repository.GetParkingSpotAsync(parkingLotId, VehicleType.truck.ToString());
            return result;
        }
    }
}
