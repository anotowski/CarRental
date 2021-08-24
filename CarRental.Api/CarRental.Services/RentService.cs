using System;
using System.Linq;
using System.Threading.Tasks;
using CarRental.BusinessLogic.Generators.Interfaces;
using CarRental.Database.Models;
using CarRental.Services.Interfaces;
using CarRental.Services.Managers.Interfaces;

namespace CarRental.Services
{
    public class RentService : IRentService
    {
        private readonly ICustomerRetrieveManager _customerRetrieveManager;
        private readonly ICarRetrieveManager _carRetrieveManager;
        private readonly IRentalHistoryManager _rentalHistoryManager;
        private readonly IBookingNumberGenerator _bookingNumberGenerator;

        public RentService(ICustomerRetrieveManager customerRetrieveManager,
            ICarRetrieveManager carRetrieveManager,
            IRentalHistoryManager rentalHistoryManager,
            IBookingNumberGenerator bookingNumberGenerator)
        {
            _customerRetrieveManager = customerRetrieveManager;
            _carRetrieveManager = carRetrieveManager;
            _rentalHistoryManager = rentalHistoryManager;
            _bookingNumberGenerator = bookingNumberGenerator;
        }

        public async Task<string> RentCar(string plateNumber, 
            string customerEmail,
            DateTime customerDateOfBirth)
        {
            ValidateInputParameters(plateNumber, customerEmail, customerDateOfBirth);

            var car = await _carRetrieveManager.TryGetCarByPlateNumber(plateNumber);

            var customer = await _customerRetrieveManager.GetOrCreateCustomer(customerEmail, customerDateOfBirth);

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

            await _rentalHistoryManager.RentCar(rentalHistory, car);

            return rentalHistory.BookingNumber;
        }

        private void ValidateInputParameters(string plateNumber,
            string customerEmail,
            DateTime customerDateOfBirth)
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
