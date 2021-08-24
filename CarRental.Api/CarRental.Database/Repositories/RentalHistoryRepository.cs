using System.Threading.Tasks;
using CarRental.Database.Models;
using CarRental.Database.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace CarRental.Database.Repositories
{
    public class RentalHistoryRepository : IRentalHistoryRepository
    {
        private readonly CarRentalContext _carRentalContext;

        public RentalHistoryRepository(CarRentalContext carRentalContext)
        {
            _carRentalContext = carRentalContext;
        }

        public async Task AddRentalHistory(RentalHistory rentalHistory)
        {
            await _carRentalContext.AddAsync(rentalHistory);
            await _carRentalContext.SaveChangesAsync();
        }

        public Task<RentalHistory> GetRentalHistoryByBookingNumber(string bookingNumber)
        {
            return _carRentalContext.RentalHistories
                .Include(x=> x.Car)
                .FirstOrDefaultAsync(x => x.BookingNumber.Equals(bookingNumber.ToLower()));
        }

        public async Task UpdateRentalHistory(RentalHistory rentalHistory)
        {
            _carRentalContext.Update(rentalHistory);
            await _carRentalContext.SaveChangesAsync();
        }
    }
}
