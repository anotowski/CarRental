using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CarRental.Application.Dto.Models;
using CarRental.Database.Repositories.Interfaces;
using CarRental.Services.Interfaces;

namespace CarRental.Services
{
    public class CarRentalService : ICarRentalService
    {
        private readonly ICarRentalRepository _carRentalRepository;

        public CarRentalService(ICarRentalRepository carRentalRepository)
        {
            _carRentalRepository = carRentalRepository;
        }

        public async Task<List<CarInformationAsyncDto>> ListCars()
        {
            var cars = await _carRentalRepository
                .GetAllCarsList();

            return cars.Select(
                    entity => new CarInformationAsyncDto()
                    {
                        ModelName = entity.ModelName,
                        Brand = entity.Brand,
                        Category = entity.Category.ToString(),
                        DailyFee = entity.BaseDayRentalFee,
                        KilometerFee = entity.KilometerFee,
                        PlateNumber = entity.PlateNumber,
                        IsAvailable = entity.IsAvailable,
                    })
                .ToList();
        }
    }
}
