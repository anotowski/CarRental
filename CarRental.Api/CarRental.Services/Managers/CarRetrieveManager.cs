using System;
using System.Threading.Tasks;
using CarRental.Database.Models;
using CarRental.Database.Repositories.Interfaces;
using CarRental.Services.Managers.Interfaces;

namespace CarRental.Services.Managers
{
    public class CarRetrieveManager : ICarRetrieveManager
    {
        private readonly ICarRepository _carRepository;

        public CarRetrieveManager(ICarRepository carRepository)
        {
            _carRepository = carRepository;
        }

        public async Task<Car> TryGetCarByPlateNumber(string plateNumber)
        {
            var car = await _carRepository.GetCarByPlateNumber(plateNumber);

            if (car == null)
            {
                throw new InvalidOperationException($"Couldn't find car with plate number: '{plateNumber}'");
            }

            if (!car.IsAvailable)
            {
                throw new InvalidOperationException($"Given car with plate number: '{plateNumber}' is not available.");
            }

            return car;
        }
    }
}
