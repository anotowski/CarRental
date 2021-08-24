using System;
using System.Threading.Tasks;
using CarRental.Application.Dto.Models;

namespace CarRental.Services.Interfaces
{
    public interface IReturnService
    {
        Task<ReturnCarResponseDto> ReturnCar(string bookingNumber,
            DateTime dateOfReturn,
            int currentCarMileage);
    }
}
