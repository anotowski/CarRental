namespace CarRental.Api.Models.Responses
{
    public class CarInformationResponse
    {
        public string PlateNumber { get; set; }

        public string Category { get; set; }

        public string Brand { get; set; }

        public string ModelName { get; set; }

        public decimal DailyFee { get; set; }

        public decimal KilometerFee { get; set; }

        public bool IsAvailable { get; set; }
    }
}
