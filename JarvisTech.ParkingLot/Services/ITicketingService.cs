using JarvisTech.ParkingLot.Common.Models;

namespace JarvisTech.ParkingLot.Services
{
    public interface ITicketingService
    {
        Task<ParkingTicket> GenerateParkingTicket(int parkingSpotId, int parkingLotId, DateTime timeofEntry);

        Task<ParkingReceipt> GenerateParkingRecepit(ParkingTicket parkingTicket, string vehicleType);

        Task<ParkingTicket> GetParkingTicket(string TicketNumber);
    }
}
