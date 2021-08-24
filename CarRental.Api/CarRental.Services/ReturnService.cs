using System;
using System.Threading.Tasks;
using CarRental.Application.Dto.Models;
using CarRental.BusinessLogic.Interfaces;
using CarRental.Services.Interfaces;
using CarRental.Services.Managers.Interfaces;

namespace CarRental.Services
{
    public class ReturnService : IReturnService
    {
        private readonly IRentalHistoryManager _rentalHistoryManager;
        private readonly IPaymentCalculatorFactory _paymentCalculatorFactory;

        public ReturnService(IRentalHistoryManager rentalHistoryManager,
            IPaymentCalculatorFactory paymentCalculatorFactory)
        {
            _rentalHistoryManager = rentalHistoryManager;
            _paymentCalculatorFactory = paymentCalculatorFactory;
        }

        public async Task<ReturnCarResponseDto> ReturnCar(string bookingNumber,
            DateTime dateOfReturn,
            int currentCarMileage)
        {
            ValidateInputParameters(bookingNumber, dateOfReturn, currentCarMileage);

            var rentalHistory = await _rentalHistoryManager.GetRentalHistoryByBookingNumber(bookingNumber);

            if (rentalHistory.RentStartDate > dateOfReturn)
            {
                throw new InvalidOperationException($"Rental end date cannot be earlier than rental start date.");
            }

            if (rentalHistory.MileageOnRentalStart > currentCarMileage)
            {
                throw new InvalidOperationException("Current car mileage is less than on rental day one");
            }

            var paymentCalculator = _paymentCalculatorFactory.Create(rentalHistory.Car.Category);
            var numberOfDays = (int)(dateOfReturn - rentalHistory.RentStartDate).TotalDays;
            var numberOfKilometers = currentCarMileage - rentalHistory.MileageOnRentalStart;
            var payment = paymentCalculator.Calculate(numberOfDays, rentalHistory.Car.BaseDayRentalFee,
                rentalHistory.Car.KilometerFee, numberOfKilometers);

            rentalHistory.MileageOnRentalEnd = currentCarMileage;
            rentalHistory.RentEndDate = dateOfReturn;
            rentalHistory.Car.IsAvailable = true; // ?

            return new ReturnCarResponseDto()
            {
                Payment = payment
            };
        }

        private void ValidateInputParameters(string bookingNumber,
            DateTime dateOfReturn,
            decimal currentCarMileage)
        {
            if (string.IsNullOrEmpty(bookingNumber))
            {
                throw new ArgumentNullException(nameof(bookingNumber));
            }

            if (dateOfReturn > DateTime.UtcNow)
            {
                throw new ArgumentOutOfRangeException($"Argument: '{nameof(dateOfReturn)}' cannot be in future.");
            }

            if (currentCarMileage == default)
            {
                throw new ArgumentNullException(nameof(currentCarMileage));
            }
        }
    }
}
