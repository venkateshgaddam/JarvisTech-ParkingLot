using JarvisTech.ParkingLot.Common.Models;
using System;
using System.Threading.Tasks;

namespace JarvisTech.ParkingLot.Repository
{
    public interface IRepository
    {
        Task<ParkingLotDetails> GetParkingLotDetailsAsync(int parkingLotId);

        Task<int> GetMotorCycleAvailableSpotsAsync(int parkingLotId);

        Task<int> GetCarAvailableSpotsAsync(int parkingLotId);

        Task<int> GetTruckAvailableSpots(int parkingLotId);

        Task<Spot> GetParkingSpotAsync(int parkingLotId, string vehicleType);

        Task<Spot> GetCurrentParkingSpotAsync(int parkingLotId, int spotId, string vehicleType);

        Task<bool> ReserveParkingSpotAsync(int parkingLotId, Spot spot);

        Task<bool> ReleaseParkingSpotAsync(int parkingLotId, Spot spot);


        Task<ParkingTicket> GenerateParkingTicketAsync(int spotId, int parkingLotId, DateTime timeOfEntry);

        Task<ParkingTicket> GetParkingTicketAsync(string ticketNumber);
    }
}
