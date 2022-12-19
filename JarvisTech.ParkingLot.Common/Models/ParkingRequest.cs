using System;

namespace JarvisTech.ParkingLot.Common.Models
{

    public abstract class ParkingRequestBuilder
    {
        public ParkingRequest _ParkingRequest { get; set; }

        public ParkingRequest CreateParkingRequest()
        {
            _ParkingRequest = new ParkingRequest();
            return _ParkingRequest;
        }
        public abstract void SetVehicleType();
        public abstract void SetVehicleNumber(string vehicleNumber);

        public void SetTimeofEntry()
        {
            _ParkingRequest.TimeOfEntry = DateTime.Now;
        }
    }


    public class MotorCycleParkingRequest : ParkingRequestBuilder
    {
        public override void SetVehicleNumber(string vehicleNumber)
        {
            _ParkingRequest.VehicleNumber = vehicleNumber;
        }

        public override void SetVehicleType()
        {
            _ParkingRequest.vehicleType = VehicleType.MotorCycle;
        }
    }

    public class CarParkingRequest : ParkingRequestBuilder
    {
        public override void SetVehicleNumber(string vehicleNumber)
        {
            _ParkingRequest.VehicleNumber = vehicleNumber;
        }

        public override void SetVehicleType()
        {
            _ParkingRequest.vehicleType = VehicleType.Car;
        }
    }

    public class TruckParkingRequest : ParkingRequestBuilder
    {
        public override void SetVehicleNumber(string vehicleNumber)
        {
            _ParkingRequest.VehicleNumber = vehicleNumber;
        }

        public override void SetVehicleType()
        {
            _ParkingRequest.vehicleType = VehicleType.truck;
        }
    }


    public class ParkingRequest
    {
        public VehicleType vehicleType { get; set; }

        public string VehicleNumber { get; set; }

        public DateTime TimeOfEntry { get; set; }

    }
}
