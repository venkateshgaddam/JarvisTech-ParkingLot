using Amazon.DynamoDBv2;
using Amazon.DynamoDBv2.DataModel;
using Amazon.DynamoDBv2.DocumentModel;
using JarvisTech.ParkingLot.Common.Models;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace JarvisTech.ParkingLot.Repository
{
    public class DbRepository : IRepository
    {
        private readonly IAmazonDynamoDB dynamoDbClient;
        private readonly DynamoDBContext dynamoDBContext;

        public DbRepository()
        {
            dynamoDbClient = new AmazonDynamoDBClient(Amazon.RegionEndpoint.USEast1);
            dynamoDBContext = new DynamoDBContext(dynamoDbClient);
        }

        public async Task<ParkingLotDetails> GetParkingLotDetailsAsync(int parkingLotId)
        {
            var parkingLotDeails = await dynamoDBContext.LoadAsync<ParkingLotDetails>(parkingLotId);

            return parkingLotDeails;
        }


        public async Task<int> GetMotorCycleAvailableSpotsAsync(int parkingLotId)
        {
            var document = await dynamoDBContext.LoadAsync<ParkingSpot>(parkingLotId);

            var spots = document.Spots.Where(a => a.Type == VehicleType.MotorCycle.ToString() && !a.IsReserved).Count();

            return spots;
        }

        public async Task<int> GetCarAvailableSpotsAsync(int parkingLotId)
        {
            var result = await dynamoDBContext.LoadAsync<ParkingSpot>(parkingLotId);

            var spots = result.Spots.Where(a => a.Type == VehicleType.Car.ToString() && !a.IsReserved).Count();

            return spots;
        }

        public async Task<int> GetTruckAvailableSpots(int parkingLotId)
        {
            var result = await dynamoDBContext.LoadAsync<ParkingSpot>(parkingLotId);

            var spots = result.Spots.Where(a => a.Type == VehicleType.truck.ToString() && !a.IsReserved).Count();

            return spots;
        }

        public async Task<Spot> GetParkingSpotAsync(int parkingLotId, string vehicleType)
        {
            Table parkingSpot = Table.LoadTable(dynamoDbClient, "ParkingSpot");
            var document = await dynamoDBContext.LoadAsync<ParkingSpot>(parkingLotId);
            var spot = document.Spots.Where(a => a.Type.ToLowerInvariant() == vehicleType.ToLowerInvariant() && !a.IsReserved).OrderBy(a => a.Id).FirstOrDefault();
            return spot;
        }


        public async Task<Spot> GetCurrentParkingSpotAsync(int parkingLotId, int spotId, string vehicleType)
        {
            var document = await dynamoDBContext.LoadAsync<ParkingSpot>(parkingLotId);
            var spot = document.Spots.FirstOrDefault(a => a.IsReserved && a.Id == spotId && a.Type.ToLower() == vehicleType.ToLower());
            return spot;
        }

        public async Task<bool> ReserveParkingSpotAsync(int parkingLotId, Spot spot)
        {
            // Update Status.
            Table parkingSpot = Table.LoadTable(dynamoDbClient, "ParkingSpot");
            var document = await dynamoDBContext.LoadAsync<ParkingSpot>(parkingLotId);
            document.Spots.FirstOrDefault(a => a.Type.ToLowerInvariant() == spot.Type.ToLowerInvariant() && !a.IsReserved && a.Id == spot.Id).IsReserved = true;
            await dynamoDBContext.SaveAsync(document);

            //Update the Count

            //return status
            var result = document.Spots.FirstOrDefault(a => a.Id == spot.Id);
            return result.IsReserved;
        }

        public async Task<bool> ReleaseParkingSpotAsync(int parkingLotId, Spot spot)
        {
            // Update Status.
            var document = await dynamoDBContext.LoadAsync<ParkingSpot>(parkingLotId);

            document.Spots.FirstOrDefault(a => a.Type.ToLowerInvariant() == spot.Type.ToLowerInvariant() && a.IsReserved && a.Id == spot.Id).IsReserved = false;


            await dynamoDBContext.SaveAsync(document);

            //Update the Count

            //return status
            var result = document.Spots.FirstOrDefault(a => a.Id == spot.Id);
            return result.IsReserved;
        }


        #region GenerateTicket


        public async Task<ParkingTicket> GenerateParkingTicketAsync(int spotId, int parkingLotId, DateTime timeOfEntry)
        {
            ParkingTicket parkingTicket = new ParkingTicket()
            {
                parkingLotId = parkingLotId,
                EntryDateTime = timeOfEntry,
                SpotNumber = spotId,
                TicketNumber = Guid.NewGuid().ToString()[..8].ToUpper()
            };
            await dynamoDBContext.SaveAsync(parkingTicket);

            var generatedTicket = await dynamoDBContext.LoadAsync<ParkingTicket>(parkingTicket.TicketNumber);

            return generatedTicket;
        }

        public async Task<ParkingTicket> GetParkingTicketAsync(string ticketNumber)
        {
            var generatedTicket = await dynamoDBContext.LoadAsync<ParkingTicket>(ticketNumber);

            return generatedTicket;
        }


        #endregion
    }
}
