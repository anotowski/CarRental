using System;
using CarRental.BusinessLogic.Calculators;
using CarRental.BusinessLogic.Calculators.Interfaces;
using CarRental.BusinessLogic.Interfaces;
using CarRental.Database.Models;

namespace CarRental.BusinessLogic
{
    public class PaymentCalculatorFactory : IPaymentCalculatorFactory
    {
        public IPaymentCalculator Create(CategoryType carCategoryType)
        {
            switch (carCategoryType)
            {
                case CategoryType.Compact:
                    return new CompactCarTypePaymentCalculator();

                case CategoryType.Premium:
                    return new PremiumCarTypePaymentCalculator();

                case CategoryType.Minivan:
                    return new MinivanCarTypePaymentCalculator();

                default:
                    throw new InvalidOperationException(
                        $"Given category type: '{carCategoryType}' is not supported for billing.");
            }
        }
    }
}
