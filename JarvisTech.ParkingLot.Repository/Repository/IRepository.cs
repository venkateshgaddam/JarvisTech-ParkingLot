using JarvisTech.ParkingLot.Common.Models;
using System;
using System.Threading.Tasks;

namespace JarvisTech.ParkingLot.Repository
{
    public interface IRepository
    {
        Task<ParkingLotDetails> GetParkingLotDetails(int parkingLotId);

        Task<int> GetMotorCycleAvailableSpots(int parkingLotId);

        Task<int> GetCarAvailableSpots(int parkingLotId);

        Task<int> GetTruckAvailableSpots(int parkingLotId);

        Task<Spot> GetParkingSpot(int parkingLotId, string vehicleType);

        Task<Spot> GetCurrentParkingSpot(int parkingLotId, int spotId, string vehicleType);

        Task<bool> ReserveParkingSpot(int parkingLotId, Spot spot);

        Task<bool> ReleaseParkingSpot(int parkingLotId, Spot spot);


        Task<ParkingTicket> GenerateParkingTicketAsync(int spotId, int parkingLotId, DateTime timeOfEntry);

        Task<ParkingTicket> GetParkingTicketAsync(string ticketNumber);
    }
}
