using System;
using CarRental.BusinessLogic.Interfaces;
using CarRental.Database.Models;
using CarRental.Services.Helpers.Interfaces;

namespace CarRental.Services.Helpers
{
    public class RentalHistoryPaymentCalculator : IRentalHistoryPaymentCalculator
    {
        private readonly IPaymentCalculatorFactory _paymentCalculatorFactory;

        public RentalHistoryPaymentCalculator(IPaymentCalculatorFactory paymentCalculatorFactory)
        {
            _paymentCalculatorFactory = paymentCalculatorFactory;
        }

        public decimal CalculatePayment(RentalHistory rentalHistoryToCalculatePayment,
            DateTime returnDate,
            int currentCarMileage)
        {
            var paymentCalculator = _paymentCalculatorFactory.Create(rentalHistoryToCalculatePayment.Car.Category);

            var numberOfDays = (int)(returnDate - rentalHistoryToCalculatePayment.RentStartDate).TotalDays;
            var numberOfKilometers = currentCarMileage - rentalHistoryToCalculatePayment.MileageOnRentalStart;

            var payment = paymentCalculator.Calculate(numberOfDays, 
                rentalHistoryToCalculatePayment.Car.BaseDayRentalFee,
                rentalHistoryToCalculatePayment.Car.KilometerFee, 
                numberOfKilometers);

            return payment;
        }
    }
}
