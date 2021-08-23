using System.Collections.Generic;
using System.Threading.Tasks;
using CarRental.Database.Models;

namespace CarRental.Database.Repositories.Interfaces
{
    public interface ICarRentalRepository
    {
        Task<List<Car>> GetAllCarsList();

        Task<Car> GetCarByPlateNumber(string plateNumber);

        Task AddRentalHistory(RentalHistory rentalHistory);

        Task UpdateCarStatus(Car car);
    }
}
