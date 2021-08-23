using System;

namespace CarRental.Api.Models.Requests
{
    public class RentRequest
    {
        public string CarPlateNumber { get; set; }

        public string CustomerEmail { get; set; }

        public DateTime CustomerDateOfBirth { get; set; }
    }
}
