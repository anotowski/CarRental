namespace CarRental.BusinessLogic.Calculators.Interfaces
{
    public interface IPaymentCalculator
    {
        decimal Calculate(int numberOfDays, decimal dailyFee, decimal kilometerFee, int numberOfKilometers);
    }
}
