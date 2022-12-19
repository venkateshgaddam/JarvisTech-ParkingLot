using JarvisTech.ParkingLot.Common.Models;

namespace JarvisTech.ParkingLot.Services
{
    public abstract class FeeCalculator
    {
        public abstract double CalculateTotalFee(double durationInMinutes, string vehicleType);
    }


    public class MallParkingFeeCalculator : FeeCalculator
    {
        private IConfiguration _configuration;

        public MallParkingFeeCalculator(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public override double CalculateTotalFee(double durationInMinutes, string vehicleType)
        {
            var feeModelDetails = _configuration.Get<FeeStructure>();
            var fareDetails = feeModelDetails.FeeModels.Select(a => a.Mall).FirstOrDefault();
            switch (vehicleType)
            {
                case "motorcycle" or "scooter":
                    var hours = durationInMinutes / 60;

                    return Convert.ToDouble(Math.Ceiling(hours) * fareDetails.Motorcycle.Interval.FirstOrDefault().Fee);
                case "car" or "electric suv" or "suv":
                    var _chours = durationInMinutes / 60;
                    var _cseconds = durationInMinutes % 60;
                    if (_cseconds != 0)
                        _chours++;

                    return Convert.ToDouble(Math.Ceiling(_chours) * fareDetails.Car.Interval.FirstOrDefault().Fee);
                case "truck" or "bus":

                    var _thours = durationInMinutes / 60;
                    var _tseconds = durationInMinutes % 60;
                    if (_tseconds != 0)
                        _thours++;

                    return Convert.ToDouble(Math.Ceiling(_thours) * fareDetails.Car.Interval.FirstOrDefault().Fee);
                default:
                    throw new Exception("Invalid Vehicle Code");
            }
        }
    }

    public class StadiumParkingFeeCalculator : FeeCalculator
    {
        private IConfiguration _configuration;

        public StadiumParkingFeeCalculator(IConfiguration configuration)
        {
            _configuration = configuration;
        }
        public override double CalculateTotalFee(double durationInMinutes, string vehicleType)
        {
            var feeModelDetails = _configuration.Get<FeeStructure>();
            var fareDetails = feeModelDetails.FeeModels.Select(a => a.Mall).FirstOrDefault();
            double totalFee = 0;    
            switch (vehicleType)
            {
                case "motorcycle" or "scooter":

                    // 30Rs for First 4 Hrs
                    if (durationInMinutes - 240 <= 0)
                    {
                        return fareDetails.Motorcycle.Interval.FirstOrDefault(a => a.FromHrs == 0 && a.ToHrs == 4).Fee;
                    }
                    else
                    {
                        totalFee += fareDetails.Motorcycle.Interval.FirstOrDefault(a => a.FromHrs == 0 && a.ToHrs == 4).Fee;
                        durationInMinutes -= 240;
                    }
                    // from 4-8 hrs 60Rs

                    if (durationInMinutes - 240 <= 0)
                    {
                        return totalFee + fareDetails.Motorcycle.Interval.FirstOrDefault(a => a.FromHrs == 4 && a.ToHrs == 8).Fee;
                    }
                    else
                    {
                        totalFee += fareDetails.Motorcycle.Interval.FirstOrDefault(a => a.FromHrs == 4 && a.ToHrs == 8).Fee;
                        durationInMinutes -= 240;
                    }
                    int hourlyFee = fareDetails.Motorcycle.Interval.FirstOrDefault(a => a.PriceType.ToLower() == "hourly").Fee;

                    double hours = durationInMinutes / 60;
                    return Convert.ToDouble(Math.Ceiling(hours) * hourlyFee);

                case "car" or "electric suv" or "suv":
                    // 30Rs for First 4 Hrs
                    if (durationInMinutes - 240 <= 0)
                    {
                        return fareDetails.Car.Interval.FirstOrDefault(a => a.FromHrs == 0 && a.ToHrs == 4).Fee;
                    }
                    else
                    {
                        totalFee += fareDetails.Car.Interval.FirstOrDefault(a => a.FromHrs == 0 && a.ToHrs == 4).Fee;
                        durationInMinutes -= 240;
                    }
                    // from 4-8 hrs 60Rs

                    if (durationInMinutes - 240 <= 0)
                    {
                        return totalFee + fareDetails.Car.Interval.FirstOrDefault(a => a.FromHrs == 4 && a.ToHrs == 8).Fee;
                    }
                    else
                    {
                        totalFee += fareDetails.Car.Interval.FirstOrDefault(a => a.FromHrs == 4 && a.ToHrs == 8).Fee;
                        durationInMinutes -= 240;
                    }
                    int carhourlyFee = fareDetails.Car.Interval.FirstOrDefault(a => a.PriceType.ToLower() == "hourly").Fee;

                    double _chours = durationInMinutes / 60;
                    return Convert.ToDouble(Math.Ceiling(_chours) * carhourlyFee);
                case "truck" or "bus":
                    // 30Rs for First 4 Hrs
                    if (durationInMinutes - 240 <= 0)
                    {
                        return fareDetails.Car.Interval.FirstOrDefault(a => a.FromHrs == 0 && a.ToHrs == 4).Fee;
                    }
                    else
                    {
                        totalFee += fareDetails.Car.Interval.FirstOrDefault(a => a.FromHrs == 0 && a.ToHrs == 4).Fee;
                        durationInMinutes -= 240;
                    }
                    // from 4-8 hrs 60Rs

                    if (durationInMinutes - 240 <= 0)
                    {
                        return totalFee + fareDetails.Car.Interval.FirstOrDefault(a => a.FromHrs == 4 && a.ToHrs == 8).Fee;
                    }
                    else
                    {
                        totalFee += fareDetails.Car.Interval.FirstOrDefault(a => a.FromHrs == 4 && a.ToHrs == 8).Fee;
                        durationInMinutes -= 240;
                    }
                    int truckhourlyFee = fareDetails.Car.Interval.FirstOrDefault(a => a.PriceType.ToLower() == "hourly").Fee;

                   
                    double _thours = durationInMinutes / 60;
                    var _tseconds = durationInMinutes % 60;
                    if (_tseconds != 0)
                        _thours++;
                    return Convert.ToDouble(Math.Ceiling(_thours) * truckhourlyFee);
                default:
                    throw new Exception("Invalid Vehicle Code");
            }
        }
    }

    public class AirportParkingFeeCalculator : FeeCalculator
    {
        private IConfiguration _configuration;

        public AirportParkingFeeCalculator(IConfiguration configuration)
        {
            _configuration = configuration;
        }
        public override double CalculateTotalFee(double durationInMinutes, string vehicleType)
        {
            var feeModelDetails = _configuration.Get<FeeStructure>();
            var fareDetails = feeModelDetails.FeeModels.Select(a => a.Airport).FirstOrDefault();
            double totalFee = 0;
            switch (vehicleType)
            {
                case "motorcycle" or "scooter":

                    // 30Rs for First 4 Hrs
                    if (durationInMinutes - 240 <= 0)
                    {
                        return fareDetails.Motorcycle.Interval.FirstOrDefault(a => a.FromHrs == 0 && a.ToHrs == 4).Fee;
                    }
                    else
                    {
                        totalFee += fareDetails.Motorcycle.Interval.FirstOrDefault(a => a.FromHrs == 0 && a.ToHrs == 4).Fee;
                        durationInMinutes -= 240;
                    }
                    // from 4-8 hrs 60Rs

                    if (durationInMinutes - 240 <= 0)
                    {
                        return totalFee + fareDetails.Motorcycle.Interval.FirstOrDefault(a => a.FromHrs == 4 && a.ToHrs == 8).Fee;
                    }
                    else
                    {
                        totalFee += fareDetails.Motorcycle.Interval.FirstOrDefault(a => a.FromHrs == 4 && a.ToHrs == 8).Fee;
                        durationInMinutes -= 240;
                    }
                    int hourlyFee = fareDetails.Motorcycle.Interval.FirstOrDefault(a => a.PriceType.ToLower() == "hourly").Fee;

                    double hours = durationInMinutes / 60;
                    return Convert.ToDouble(Math.Ceiling(hours) * hourlyFee);

                case "car" or "electric suv" or "suv":
                    // 30Rs for First 4 Hrs
                    if (durationInMinutes - 240 <= 0)
                    {
                        return fareDetails.Car.Interval.FirstOrDefault(a => a.FromHrs == 0 && a.ToHrs == 4).Fee;
                    }
                    else
                    {
                        totalFee += fareDetails.Car.Interval.FirstOrDefault(a => a.FromHrs == 0 && a.ToHrs == 4).Fee;
                        durationInMinutes -= 240;
                    }
                    // from 4-8 hrs 60Rs

                    if (durationInMinutes - 240 <= 0)
                    {
                        return totalFee + fareDetails.Car.Interval.FirstOrDefault(a => a.FromHrs == 4 && a.ToHrs == 8).Fee;
                    }
                    else
                    {
                        totalFee += fareDetails.Car.Interval.FirstOrDefault(a => a.FromHrs == 4 && a.ToHrs == 8).Fee;
                        durationInMinutes -= 240;
                    }
                    int carhourlyFee = fareDetails.Car.Interval.FirstOrDefault(a => a.PriceType.ToLower() == "hourly").Fee;

                    double _chours = durationInMinutes / 60;
                    return Convert.ToDouble(Math.Ceiling(_chours) * carhourlyFee);
                case "truck" or "bus":
                    // 30Rs for First 4 Hrs
                    if (durationInMinutes - 240 <= 0)
                    {
                        return fareDetails.Car.Interval.FirstOrDefault(a => a.FromHrs == 0 && a.ToHrs == 4).Fee;
                    }
                    else
                    {
                        totalFee += fareDetails.Car.Interval.FirstOrDefault(a => a.FromHrs == 0 && a.ToHrs == 4).Fee;
                        durationInMinutes -= 240;
                    }
                    // from 4-8 hrs 60Rs

                    if (durationInMinutes - 240 <= 0)
                    {
                        return totalFee + fareDetails.Car.Interval.FirstOrDefault(a => a.FromHrs == 4 && a.ToHrs == 8).Fee;
                    }
                    else
                    {
                        totalFee += fareDetails.Car.Interval.FirstOrDefault(a => a.FromHrs == 4 && a.ToHrs == 8).Fee;
                        durationInMinutes -= 240;
                    }
                    int truckhourlyFee = fareDetails.Car.Interval.FirstOrDefault(a => a.PriceType.ToLower() == "hourly").Fee;


                    double _thours = durationInMinutes / 60;
                    var _tseconds = durationInMinutes % 60;
                    if (_tseconds != 0)
                        _thours++;
                    return Convert.ToDouble(Math.Ceiling(_thours) * truckhourlyFee);
                default:
                    throw new Exception("Invalid Vehicle Code");
            }
        }
    }
}
