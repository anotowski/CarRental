using System;
using System.Threading.Tasks;
using CarRental.Database.Models;
using CarRental.Database.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace CarRental.Database.Repositories
{
    public class CustomerRepository : ICustomerRepository
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

        public async Task<Customer> GetOrCreateCustomer(string email, DateTime birthDate)
        {
            var customer = await _carRentalContext.Customers
                .FirstOrDefaultAsync(x => x.Email.Equals(email));

            if (customer == null)
            {
                customer = new Customer()
                {
                    BirthDate = birthDate,
                    Email = email
                };

                _carRentalContext.Customers.Add(customer);
            }

            return customer;
        }
    }
}
