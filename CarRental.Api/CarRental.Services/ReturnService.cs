using System;
using System.Threading.Tasks;
using CarRental.Application.Dto.Models;
using CarRental.Services.Helpers.Interfaces;
using CarRental.Services.Interfaces;
using CarRental.Services.Managers.Interfaces;

namespace CarRental.Services
{
    public class ReturnService : IReturnService
    {
        private readonly IRentalHistoryManager _rentalHistoryManager;
        private readonly IRentalHistoryPaymentCalculator _rentalHistoryPaymentCalculator;

        public ReturnService(IRentalHistoryManager rentalHistoryManager,
            IRentalHistoryPaymentCalculator rentalHistoryPaymentCalculator)
        {
            _rentalHistoryManager = rentalHistoryManager;
            _rentalHistoryPaymentCalculator = rentalHistoryPaymentCalculator;
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

            rentalHistory.MileageOnRentalEnd = currentCarMileage;
            rentalHistory.RentEndDate = dateOfReturn;
            rentalHistory.Car.CurrentMileage = currentCarMileage;

            await _rentalHistoryManager.ReturnCar(rentalHistory);

            var payment =
                _rentalHistoryPaymentCalculator.CalculatePayment(rentalHistory, dateOfReturn, currentCarMileage);

            return new ReturnCarResponseDto()
            {
                Payment = payment
            };
        }

        private void ValidateInputParameters(string bookingNumber,
            DateTime dateOfReturn,
            decimal currentCarMileage)
        {
            if (string.IsNullOrWhiteSpace(bookingNumber))
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
