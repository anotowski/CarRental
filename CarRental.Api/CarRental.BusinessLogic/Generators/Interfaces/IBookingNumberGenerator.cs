namespace CarRental.BusinessLogic.Generators.Interfaces
{
    public interface IBookingNumberGenerator
    {
        string Generate(string plateNumber);
    }
}
