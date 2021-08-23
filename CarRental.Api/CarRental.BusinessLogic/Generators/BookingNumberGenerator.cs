using System;
using CarRental.BusinessLogic.Generators.Interfaces;

namespace CarRental.BusinessLogic.Generators
{
    public class BookingNumberGenerator : IBookingNumberGenerator
    {
        private const string _bookingNumberTemplate = "{0}/{1}";

        public string Generate(string plateNumber) => 
            string.Format(_bookingNumberTemplate,
                plateNumber.ToLower(),
                DateTime.UtcNow.ToString("dd-MM-yyyy-hh-mm"));
    }
}
