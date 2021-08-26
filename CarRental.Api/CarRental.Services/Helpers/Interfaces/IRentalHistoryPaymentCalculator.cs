using System;
using CarRental.Database.Models;

namespace CarRental.Services.Helpers.Interfaces
{
    public interface IRentalHistoryPaymentCalculator
    {
        decimal CalculatePayment(RentalHistory rentalHistoryToCalculatePayment,
            DateTime returnDate,
            int currentCarMileage);
    }
}
