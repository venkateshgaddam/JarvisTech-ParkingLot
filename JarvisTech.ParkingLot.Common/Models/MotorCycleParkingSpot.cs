using System.Collections.Generic;

namespace JarvisTech.ParkingLot.Common.Models
{
    public class MotorCycleParkingSpot : ParkingSpot
    {
        public int _totalSpots { get; set; }
        public MotorCycleParkingSpot()
        {
            _unreservedSpots = new HashSet<int>();
            _reservedSpots= new HashSet<int>();
        }

        public HashSet<int> _reservedSpots { get; }

        public HashSet<int> _unreservedSpots
        {
            get;
        }

        //public override int GetNextParkingSpot()
        //{
        //    return _unreservedSpots.OrderBy(spot => spot).FirstOrDefault();
        //}

        //public override void ReserveParkingSpot(int id)
        //{
        //    var result = _unreservedSpots.FirstOrDefault(a => a == id);

        //    if (result > 0)
        //    {
        //        _unreservedSpots.Remove(id);
        //        _reservedSpots.Add(id);
        //    }
        //}

        //public override void ReleaseParkingSpot(int id)
        //{
        //    var result = _reservedSpots.FirstOrDefault(a => a == id);

        //    if (result > 0)
        //    {
        //        _reservedSpots.Remove(id);
        //        _unreservedSpots.Add(id);
        //    }
        //}
    }
}
