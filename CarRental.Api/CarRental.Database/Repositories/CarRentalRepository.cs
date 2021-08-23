using System.Collections.Generic;
using System.Threading.Tasks;
using CarRental.Database.Models;
using CarRental.Database.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace CarRental.Database.Repositories
{
    public class CarRentalRepository : ICarRentalRepository
    {
        private readonly CarRentalContext _carRentalContext;

        public CarRentalRepository(CarRentalContext carRentalContext)
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

        public async Task AddRentalHistory()
        {

        }
    }
}
