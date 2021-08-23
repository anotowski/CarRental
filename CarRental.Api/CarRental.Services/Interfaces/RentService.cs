using System;
using System.Threading.Tasks;

namespace CarRental.Services.Interfaces
{
    public interface IRentService
    {
        Task<string> RentCar(string plateNumber, string customerEmail, DateTime customerDateOfBirth);
    }
}
