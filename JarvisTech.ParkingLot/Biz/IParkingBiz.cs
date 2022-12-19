using JarvisTech.ParkingLot.Common.Models;
using System.Net;

namespace JarvisTech.ParkingLot.Biz
{
    public interface IParkingBiz
    {
        Task<(HttpStatusCode statusCode, object? parkingTicket)> ParkVehicle(string vehicleType);


        Task<(HttpStatusCode statusCode, object? result)> UnParkVehicle(string vehicleType, string ticketNumber);
    }
}
