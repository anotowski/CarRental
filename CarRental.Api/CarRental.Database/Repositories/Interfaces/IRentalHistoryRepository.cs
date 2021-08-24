using System.Threading.Tasks;
using CarRental.Database.Models;

namespace CarRental.Database.Repositories.Interfaces
{
    public interface IRentalHistoryRepository
    {
        Task AddRentalHistory(RentalHistory rentalHistory);

        Task<RentalHistory> GetRentalHistoryByBookingNumber(string bookingNumber);

        Task UpdateRentalHistory(RentalHistory rentalHistory);
    }
}
