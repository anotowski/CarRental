﻿using System;
using System.Linq;
using System.Threading.Tasks;
using CarRental.BusinessLogic.Generators.Interfaces;
using CarRental.Database.Models;
using CarRental.Database.Repositories.Interfaces;
using CarRental.Services.Interfaces;

namespace CarRental.Services
{
    public class RentService : IRentService
    {
        private readonly ICarRentalRepository _carRentalRepository;
        private readonly ICustomerRepository _customerRepository;
        private readonly IBookingNumberGenerator _bookingNumberGenerator;

        public RentService(ICarRentalRepository carRentalRepository,
            ICustomerRepository customerRepository,
            IBookingNumberGenerator bookingNumberGenerator)
        {
            _carRentalRepository = carRentalRepository;
            _customerRepository = customerRepository;
            _bookingNumberGenerator = bookingNumberGenerator;
        }

        public async Task<string> RentCar(string plateNumber, string customerEmail, DateTime customerDateOfBirth)
        {
            ValidateInputParameters(plateNumber, customerEmail, customerDateOfBirth);

            var car = await _carRentalRepository.GetCarByPlateNumber(plateNumber);

            if (car == null)
            {
                throw new InvalidOperationException($"Couldn't find car with plate number: '{plateNumber}'");
            }

            if (!car.IsAvailable)
            {
                throw new InvalidOperationException($"Given car with plate number: '{plateNumber}' is not available.");
            }

            var customer = await _customerRepository.GetOrCreateCustomer(customerEmail, customerDateOfBirth);

            var lastExistingRentalHistory = car.RentalHistories.OrderByDescending(x => x.Id).FirstOrDefault();

            var rentalHistory = new RentalHistory()
            {
                BookingNumber = _bookingNumberGenerator.Generate(plateNumber),
                CarId = car.Id,
                Customer = customer,
                MileageOnRentalStart = lastExistingRentalHistory?.MileageOnRentalEnd ?? 1,
                MileageOnRentalEnd = null,
                RentStartDate = DateTime.UtcNow,
                RentEndDate = null
            };

            await _carRentalRepository.AddRentalHistory(rentalHistory);
            car.IsAvailable = false;

            await _carRentalRepository.UpdateCarStatus(car);
            return rentalHistory.BookingNumber;
        }

        private void ValidateInputParameters(string plateNumber, string customerEmail, DateTime customerDateOfBirth)
        {
            if (string.IsNullOrWhiteSpace(plateNumber))
            {
                throw new ArgumentNullException(nameof(plateNumber));
            }

            if (string.IsNullOrWhiteSpace(customerEmail))
            {
                throw new ArgumentNullException(nameof(customerEmail));
            }

            if (customerDateOfBirth < DateTime.UtcNow.AddYears(-99) || customerDateOfBirth > DateTime.UtcNow)
            {
                throw new InvalidOperationException($"Given customer date of birth '{customerDateOfBirth}' is out of range.");
            }
        }
    }
}
