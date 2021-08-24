using System.Reflection.Metadata.Ecma335;
using CarRental.BusinessLogic.Calculators.Interfaces;

namespace CarRental.BusinessLogic.Calculators
{
    public class MinivanCarTypePaymentCalculator : IPaymentCalculator
    {
        public const decimal MinivanCarTypeDailyFeeMultiplier = 1.7m;
        public const decimal MinivanCarTypeKilometerFeeMultiplier = 1.5m;

        public decimal Calculate(int numberOfDays,
            decimal dailyFee,
            decimal kilometerFee,
            int numberOfKilometers) =>
            dailyFee * numberOfDays * MinivanCarTypeDailyFeeMultiplier +
            kilometerFee * numberOfKilometers * MinivanCarTypeKilometerFeeMultiplier;

    }
}
