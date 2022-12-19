using JarvisTech.ParkingLot.Common.Models;
using JarvisTech.ParkingLot.Services;
using System.Net;

namespace JarvisTech.ParkingLot.Biz
{
    /// <summary>
    /// 
    /// </summary>
    public class ParkingBiz : IParkingBiz
    {
        private IParkingAssignmentService assignmentService;

        private readonly IServiceProvider _serviceProvider;

        private readonly ITicketingService _ticketingService;

        private readonly int _parkingLotId;

        public ParkingBiz(IServiceProvider serviceProvider, ITicketingService ticketingService)
        {
            _serviceProvider = serviceProvider;
            _ticketingService = ticketingService;
            _parkingLotId = serviceProvider.GetService<IConfiguration>().GetValue<int>("ParkingLotDetails:LotId");
        }


        public async Task<(HttpStatusCode, object?)> ParkVehicle(string vehicleType)
        {
            try
            {
                //1. Get ParkingAssginment Service
                var assignmentFactory = new ConcreteParkingAssignmentFactory(_serviceProvider);
                assignmentService = assignmentFactory.GetAssignmentService(vehicleType);

                //2. Check for availableSpots
                var availableSpots = await assignmentService.GetAvailableParkingSpots(_parkingLotId);

                if (availableSpots > 0)
                {
                    // Get the Next Latest Parking Spot
                    var spot = await assignmentService.GetLatestAvailableSpot(_parkingLotId);

                    //3. If spot Exists, Reserve the spot and generate the ticket.
                    var isReserved = await assignmentService.ReserveParkingSpot(_parkingLotId, spot);

                    if (isReserved)
                    {
                        //4.Generate the ParkingTicket

                        var ticket = await _ticketingService.GenerateParkingTicket(spot.Id, _parkingLotId, DateTime.Now);
                        return (HttpStatusCode.OK, ticket);
                    }
                }
                else
                {
                    return (HttpStatusCode.NotFound, "No Parking Spots Available.");
                }
                return (HttpStatusCode.NotFound, null);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return (HttpStatusCode.BadRequest, ex.Message);
            }
        }

        public async Task<(HttpStatusCode, object?)> UnParkVehicle(string vehicleType, string ticketNumber)
        {
            try
            {
                // 1. Get Ticket Details 
                var ticketDetails = await _ticketingService.GetParkingTicket(ticketNumber);

                // 2.Get the current Spot Details and Release the Spot
                int spotId = ticketDetails.SpotNumber;
                int parkingLotId = ticketDetails.parkingLotId;

                var assignmentFactory = new ConcreteParkingAssignmentFactory(_serviceProvider);
                assignmentService = assignmentFactory.GetAssignmentService(vehicleType);
                var spotDetails = await assignmentService.GetCurrentParkingSpotDetails(parkingLotId, spotId);

                // 3.Release the Spot from reserved to Un Reserved.
                await assignmentService.ReleaseParkingSpot(parkingLotId, spotDetails);

                // 4.Generate Fee Receipt
                var receipt = await _ticketingService.GenerateParkingRecepit(ticketDetails, vehicleType);

                if (receipt!=null)
                {
                    return (HttpStatusCode.OK, receipt);
                }
                else
                {
                    return (HttpStatusCode.BadRequest, "Something went Wrong");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return (HttpStatusCode.BadRequest, ex.Message);
            }

            
        }
    }
}
