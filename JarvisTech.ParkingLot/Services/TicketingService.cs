using JarvisTech.ParkingLot.Common.Models;
using JarvisTech.ParkingLot.Repository;
using Microsoft.Extensions.Configuration;

namespace JarvisTech.ParkingLot.Services
{
    /// <summary>
    /// 
    /// </summary>
    public class TicketingService : ITicketingService
    {
        private readonly IRepository _repository;
        private readonly IConfiguration _configuration;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="repository"></param>
        public TicketingService(IRepository repository, IConfiguration configuration)
        {
            _repository = repository;
            _configuration = configuration;
        }

        public async Task<ParkingReceipt> GenerateParkingRecepit(ParkingTicket parkingTicket, string vehicleType)
        {
            //1.Get the Fee Model for the ParkingLot
            DateTime exitTime = DateTime.Now;
            var parkingLotDetails = await _repository.GetParkingLotDetailsAsync(parkingTicket.parkingLotId);

            var duration = exitTime.Subtract(parkingTicket.EntryDateTime).TotalMinutes;

            ConcreteFeeCalculator concreteFeeCalculator = new(_configuration);
            var _feeCalcuatorService = concreteFeeCalculator.GetFeeCalculatorService(vehicleType, parkingLotDetails.FeeModel.ToLower());

            double parkingFee = _feeCalcuatorService.CalculateTotalFee(duration, vehicleType);

            ParkingReceipt parkingReceipt = new ParkingReceipt()
            {
                EntryTime = parkingTicket.EntryDateTime,
                Fee = parkingFee,
                ExitTime = exitTime,
                ReceiptNumber = $"R-{Guid.NewGuid().ToString()[..8]}"
            };

            return parkingReceipt;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="parkingSpotId"></param>
        /// <param name="parkingLotId"></param>
        /// <param name="timeofEntry"></param>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        public async Task<ParkingTicket> GenerateParkingTicket(int parkingSpotId, int parkingLotId, DateTime timeofEntry)
        {
            var ticket = await _repository.GenerateParkingTicketAsync(parkingSpotId, parkingLotId, timeofEntry);
            return ticket;
        }


        public async Task<ParkingTicket> GetParkingTicket(string ticketNumber)
        {
            var ticket = await _repository.GetParkingTicketAsync(ticketNumber);
            return ticket;
        }


    }
}
