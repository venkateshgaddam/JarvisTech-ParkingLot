using Amazon.DynamoDBv2.DataModel;
using System.Collections.Generic;

namespace JarvisTech.ParkingLot.Common.Models
{
    [DynamoDBTable("ParkingSpot")]
    public class ParkingSpot
    {
        [DynamoDBHashKey]
        public int ParkingLotId { get; set; }
        public List<Spot> Spots { get; set; }
    }

    public class Spot
    {
        public int Id { get; set; }
        public bool IsReserved { get; set; }
        public string Type { get; set; }
    }
}
