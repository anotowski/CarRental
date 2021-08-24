using System.Collections.Generic;
using System.Threading.Tasks;
using CarRental.Database.Models;
using CarRental.Database.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace CarRental.Database.Repositories
{
    public class CarRepository : ICarRepository
    {
        private readonly CarRentalContext _carRentalContext;

        public CarRepository(CarRentalContext carRentalContext)
        {
            _carRentalContext = carRentalContext;
        }

        public Task<List<Car>> GetAllCarsList()
        {
            return _carRentalContext.Cars
                .ToListAsync();
        }

        public Task<Car> GetCarByPlateNumber(string plateNumber)
        {
            return _carRentalContext.Cars
                .Include(x => x.RentalHistories)
                .FirstOrDefaultAsync(x => x.PlateNumber.Equals(plateNumber));
        }

        public async Task UpdateCarStatus(Car car)
        {
            _carRentalContext.Update(car);
            await _carRentalContext.SaveChangesAsync();
        }
    }
}
