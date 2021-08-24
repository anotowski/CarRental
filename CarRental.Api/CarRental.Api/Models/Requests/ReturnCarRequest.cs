using System;

namespace CarRental.Api.Models.Requests
{
    public class ReturnCarRequest
    {
        public string BookingNumber { get; set; }

        public DateTime ReturnDate { get; set; }

        public int CurrentMileage { get; set; }
    }
}
