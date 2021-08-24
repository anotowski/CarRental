using System.Threading.Tasks;
using CarRental.Database.Models;

namespace CarRental.Services.Managers.Interfaces
{
    public interface ICarRetrieveManager
    {
        Task<Car> TryGetCarByPlateNumber(string plateNumber);
    }
}
