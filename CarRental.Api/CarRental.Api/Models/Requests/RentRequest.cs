using System;

namespace CarRental.Api.Models.Requests
{
    public class RentRequest
    {
        public string CarPlateNumber { get; set; }

        public string CustomerGivenName { get; set; }

        public string CustomerFamilyName { get; set; }

        public DateTime CustomerDateOfBirth { get; set; }
    }
}
