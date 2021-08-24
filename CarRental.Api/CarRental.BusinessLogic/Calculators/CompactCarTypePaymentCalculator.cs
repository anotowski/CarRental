using CarRental.BusinessLogic.Calculators.Interfaces;

namespace CarRental.BusinessLogic.Calculators
{
    public class CompactCarTypePaymentCalculator : IPaymentCalculator
    {
        public decimal Calculate(int numberOfDays,
            decimal dailyFee,
            decimal kilometerFee,
            int numberOfKilometers) =>
            numberOfDays * dailyFee;
    }
}
