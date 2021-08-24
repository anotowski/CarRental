using System.Collections.Generic;
using System.Threading.Tasks;
using CarRental.Database.Models;

namespace CarRental.Database.Repositories.Interfaces
{
    public interface ICarRepository
    {
        Task<List<Car>> GetAllCarsList();

        Task<Car> GetCarByPlateNumber(string plateNumber);

        Task UpdateCarStatus(Car car);
    }
}
