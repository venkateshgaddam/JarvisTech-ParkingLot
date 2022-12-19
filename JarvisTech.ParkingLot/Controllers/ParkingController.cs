using JarvisTech.ParkingLot.Biz;
using JarvisTech.ParkingLot.Common.Models;
using Microsoft.AspNetCore.Mvc;

namespace JarvisTech.ParkingLot.Controllers
{
    [Route("aws/[controller]")]
    [ApiController]
    public class ParkingController : ControllerBase
    {
        private readonly IParkingBiz _parkingBiz;
        IConfiguration
              _configuration;
        public ParkingController(IParkingBiz parkingBiz, IConfiguration configuration)
        {
            _parkingBiz = parkingBiz;
            _configuration = configuration;
        }



        [HttpPost("Park/{vehicleType}")]
        public async Task<IActionResult> ParkVehicle(string vehicleType)
        {
            var result = await _parkingBiz.ParkVehicle(vehicleType);

            if (result.statusCode == System.Net.HttpStatusCode.OK)
            {
                return Ok(result.parkingTicket);
            }
            else if (result.statusCode == System.Net.HttpStatusCode.NotFound)
            {
                return NotFound("No Spots Available");
            }
            else
            {
                return BadRequest("Something is wrong!! Please try again Later");
            }
        }

        [HttpPost("UnPark/{TicketNumber}")]
        public async Task<IActionResult> TicketNumberVehicle(string vehicleType, string TicketNumber)
        {
            var (statusCode, result) = await _parkingBiz.UnParkVehicle(vehicleType, TicketNumber);

            if (statusCode == System.Net.HttpStatusCode.OK)
            {
                return Ok(result);
            }
            else
            {
                return BadRequest($"{vehicleType} {TicketNumber}");
            }
        }

    }
}
