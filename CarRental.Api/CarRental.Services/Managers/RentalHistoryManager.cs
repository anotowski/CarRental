using System;
using System.Threading.Tasks;
using CarRental.Database.Models;
using CarRental.Database.Repositories.Interfaces;
using CarRental.Services.Managers.Interfaces;

namespace CarRental.Services.Managers
{
    public class RentalHistoryManager : IRentalHistoryManager
    {
        private readonly IRentalHistoryRepository _rentalHistoryRepository;
        private readonly ICarRepository _carRepository;

        public RentalHistoryManager(IRentalHistoryRepository rentalHistoryRepository,
            ICarRepository carRepository)
        {
            _rentalHistoryRepository = rentalHistoryRepository;
            _carRepository = carRepository;
        }

        public async Task<RentalHistory> GetRentalHistoryByBookingNumber(string bookingNumber)
        {
            var rentalHistory = await _rentalHistoryRepository
                .GetRentalHistoryByBookingNumber(bookingNumber);

            if (rentalHistory == null)
            {
                throw new InvalidOperationException($"Couldn't find a rent with a booking number: '{bookingNumber}'");
            }

            return rentalHistory;
        }

        public async Task RentCar(RentalHistory rentalHistory)
        {
            await _rentalHistoryRepository.AddRentalHistory(rentalHistory);

            rentalHistory.Car.IsAvailable = false;
            await _carRepository.UpdateCarStatus(rentalHistory.Car);
        }

        public async Task ReturnCar(RentalHistory rentalHistory)
        {
            await _rentalHistoryRepository.UpdateRentalHistory(rentalHistory);

            rentalHistory.Car.IsAvailable = true;
            await _carRepository.UpdateCarStatus(rentalHistory.Car);
        }
    }
}
