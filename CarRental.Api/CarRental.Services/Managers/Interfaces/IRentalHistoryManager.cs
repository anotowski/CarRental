using System.Threading.Tasks;
using CarRental.Database.Models;

namespace CarRental.Services.Managers.Interfaces
{
    public interface IRentalHistoryManager
    {
        Task RentCar(RentalHistory rentalHistory, Car car);
    }
}
