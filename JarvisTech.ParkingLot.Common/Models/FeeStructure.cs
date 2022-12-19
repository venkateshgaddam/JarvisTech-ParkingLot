using System;
using System.Collections.Generic;
using System.Text;

namespace JarvisTech.ParkingLot.Common.Models
{
    public class FeeStructure
    {
        public List<FeeModel> FeeModels { get; set; }
    }
    public class Airport
    {
        public Motorcycle Motorcycle { get; set; }
        public Car Car { get; set; }
    }

    public class Bus
    {
        public List<Interval> Interval { get; set; }
    }

    public class Car
    {
        public List<Interval> Interval { get; set; }
    }

    public class FeeModel
    {
        public Mall Mall { get; set; }
        public Stadium Stadium { get; set; }
        public Airport Airport { get; set; }
    }

    public class Interval
    {
        public int FromHrs { get; set; }
        public int ToHrs { get; set; }
        public string PriceType { get; set; }
        public int Fee { get; set; }
    }

    public class Mall
    {
        public Motorcycle Motorcycle { get; set; }
        public Car Car { get; set; }
        public SUV SUV { get; set; }
        public Bus Bus { get; set; }
        public Truck Truck { get; set; }
    }

    public class Motorcycle
    {
        public List<Interval> Interval { get; set; }
    }

    public class Stadium
    {
        public Motorcycle Motorcycle { get; set; }
        public Car Car { get; set; }
    }

    public class SUV
    {
        public List<Interval> Interval { get; set; }
    }

    public class Truck
    {
        public List<Interval> Interval { get; set; }
    }
}
