using Amazon.DynamoDBv2.DataModel;
using System;
using System.Collections.Generic;
using System.Text;

namespace JarvisTech.ParkingLot.Common.Models
{
    [DynamoDBTable("ParkingLot")]
    public class ParkingLotDetails
    {
        [DynamoDBHashKey]
        public int ParkingLotId { get; set; }
        public string FeeModel { get; set; }
        public string ParkingLotName { get; set; }
    }

    //public class Spot
    //{
    //    public int Available { get; set; }
    //    public int Reserved { get; set; }
    //    public int Total { get; set; }
    //    public string VehicleType { get; set; }
    //}
}
