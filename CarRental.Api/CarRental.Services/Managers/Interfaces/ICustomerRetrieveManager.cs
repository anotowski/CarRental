using System;
using System.Threading.Tasks;
using CarRental.Database.Models;

namespace CarRental.Services.Managers.Interfaces
{
    public interface ICustomerRetrieveManager
    {
        Task<Customer> GetOrCreateCustomer(string email, DateTime birthDate);
    }
}
