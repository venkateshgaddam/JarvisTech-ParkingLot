using System;
using System.Collections.Generic;
using System.Net;
using System.Text;

namespace JarvisTech.ParkingLot.Common.Models
{
    public class ParkingResult
    {
        public string ErrorMessage { get; set; }

        public HttpStatusCode HttpStatusCode { get; set; }

        public ParkingTicket ParkingTicket { get; set; }
    }
}
