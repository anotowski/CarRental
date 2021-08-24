using CarRental.BusinessLogic.Calculators.Interfaces;

namespace CarRental.BusinessLogic.Calculators
{
    public class PremiumCarTypePaymentCalculator : IPaymentCalculator
    {
        public const decimal PremiumCarCategoryKilometerFeeMultiplier = 1.2m;

        public decimal Calculate(int numberOfDays, 
            decimal dailyFee,
            decimal kilometerFee,
            int numberOfKilometers) =>
            numberOfDays * dailyFee + PremiumCarCategoryKilometerFeeMultiplier * numberOfKilometers;
    }
}
