using System;
using System.Threading.Tasks;
using CarRental.Database.Models;
using Microsoft.EntityFrameworkCore;

namespace CarRental.Database.Repositories
{
    public class CustomerRepository
    {
        private readonly CarRentalContext _carRentalContext;

        public CustomerRepository(CarRentalContext carRentalContext)
        {
            _carRentalContext = carRentalContext;
        }

        public Task<Customer> GetCustomerByEmail(string customerEmail)
        {
            return _carRentalContext.Customers
                .FirstOrDefaultAsync(x => x.Email.Equals(customerEmail));
        }

        public async Task<Customer> CreateCustomer(string email, DateTime birthDate)
        {
            var mewCustomer = new Customer()
            {
                BirthDate = birthDate,
                Email = email
            };

            _carRentalContext.Customers.Add(mewCustomer);

            await _carRentalContext.SaveChangesAsync();

            return mewCustomer;
        }
    }
}
