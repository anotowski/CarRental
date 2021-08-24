﻿using System.Threading.Tasks;
using CarRental.Database.Models;

namespace CarRental.Services.Managers.Interfaces
{
    public interface IRentalHistoryManager
    {
        Task<RentalHistory> GetRentalHistoryByBookingNumber(string bookingNumber);

        Task RentCar(RentalHistory rentalHistory, Car car);

        Task ReturnCar(RentalHistory rentalHistory);
    }
}
