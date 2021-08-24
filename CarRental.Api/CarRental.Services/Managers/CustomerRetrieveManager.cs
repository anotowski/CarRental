using System;
using System.Threading.Tasks;
using CarRental.Database.Models;
using CarRental.Database.Repositories.Interfaces;
using CarRental.Services.Managers.Interfaces;

namespace CarRental.Services.Managers
{
    public class CustomerRetrieveManager : ICustomerRetrieveManager
    {
        private readonly ICustomerRepository _customerRepository;

        public CustomerRetrieveManager(ICustomerRepository customerRepository)
        {
            _customerRepository = customerRepository;
        }

        public async Task<Customer> GetOrCreateCustomer(string email,
            DateTime birthDate)
        {
            var customer = await _customerRepository.GetCustomerByEmail(email);

            if (customer == null)
            {
                customer = new Customer()
                {
                    BirthDate = birthDate,
                    Email = email
                };

                await _customerRepository.CreateCustomer(customer);
            }

            return customer;
        }
    }
}
