using System.Collections.Generic;
using System.Threading.Tasks;
using CarRental.Application.Dto.Models;

namespace CarRental.Services.Interfaces
{
    public interface ICarRentalService
    {
        Task<List<CarInformationAsyncDto>> ListCars();
    }
}
