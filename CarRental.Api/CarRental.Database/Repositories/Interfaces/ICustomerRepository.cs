using System.Threading.Tasks;
using CarRental.Database.Models;

namespace CarRental.Database.Repositories.Interfaces
{
    public interface ICustomerRepository
    {
        Task<Customer> GetCustomerByEmail(string customerEmail);

        Task CreateCustomer(Customer customer);
    }
}
