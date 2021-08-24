using CarRental.BusinessLogic.Calculators.Interfaces;
using CarRental.Database.Models;

namespace CarRental.BusinessLogic.Interfaces
{
    public interface IPaymentCalculatorFactory
    {
        IPaymentCalculator Create(CategoryType carCategoryType);
    }
}
