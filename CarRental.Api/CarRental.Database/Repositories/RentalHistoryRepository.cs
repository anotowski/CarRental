using System.Threading.Tasks;
using CarRental.Database.Models;
using CarRental.Database.Repositories.Interfaces;

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
    }
}
