using JarvisTech.ParkingLot.Common.Models;

namespace JarvisTech.ParkingLot.Services
{
    public interface IParkingAssignmentService
    {
        Task<int> GetAvailableParkingSpots(int parkingLotId);

        Task<Spot> GetLatestAvailableSpot(int parkingLotId);

        Task<Spot> GetCurrentParkingSpotDetails(int parkingLotId, int spotId);

        Task<bool> ReserveParkingSpot(int parkingLotId, Spot parkingSpot);

        Task<bool> ReleaseParkingSpot(int parkingLotId, Spot parkingSpot);
    }
}
