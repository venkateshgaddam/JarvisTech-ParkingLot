using Amazon.DynamoDBv2.DataModel;
using System;

namespace JarvisTech.ParkingLot.Common.Models
{
    [DynamoDBTable("ParkingTicket")]
    public class ParkingTicket
    {
        [DynamoDBHashKey]
        public string TicketNumber { get; set; }

        public int SpotNumber { get; set; }

        public DateTime EntryDateTime { get; set; }

        public int parkingLotId { get; set; }
    }
}
