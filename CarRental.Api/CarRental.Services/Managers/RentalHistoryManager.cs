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

        public async Task RentCar(RentalHistory rentalHistory, Car car)
        {
            await _rentalHistoryRepository.AddRentalHistory(rentalHistory);

            car.IsAvailable = false;
            await _carRepository.UpdateCarStatus(car);
        }
    }
}
