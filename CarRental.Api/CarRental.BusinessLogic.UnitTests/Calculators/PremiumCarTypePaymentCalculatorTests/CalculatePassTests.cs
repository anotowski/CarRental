using AutoFixture;
using CarRental.BusinessLogic.Calculators;
using Xunit;

namespace CarRental.BusinessLogic.UnitTests.Calculators.PremiumCarTypePaymentCalculatorTests
{
    public class CalculatePassTests
    {
        private readonly IFixture _fixture;

        public CalculatePassTests()
        {
            _fixture = new Fixture();
        }

        [Fact]
        public void PremiumCarCategoryKilometerFeeMultiplier_GetObjectValue_AssertValueIsCorrect()
        {
            // Arrange
            var expectedValue = 1.2m;

            // Act
            // Assert
            Assert.Equal(expectedValue, PremiumCarTypePaymentCalculator.PremiumCarCategoryKilometerFeeMultiplier);
        }

        [Theory]
        [ClassData(typeof(PremiumCarTypeTestData))]
        public void Calculate_WhenHappyPath_AssertResultIsCorrect(int numberOfDays,
            decimal dailyFee,
            decimal kilometerFee,
            int numberOfKilometers,
            decimal expectedResult)
        {
            // Arrange
            var calculator = _fixture.Create<PremiumCarTypePaymentCalculator>();

            // Act
            var result = calculator.Calculate(numberOfDays, dailyFee, kilometerFee, numberOfKilometers);

            // Assert
            Assert.Equal(expectedResult, result);
        }
    }

    public class PremiumCarTypeTestData : TheoryData<int, decimal, decimal, int, decimal>
    {
        public PremiumCarTypeTestData()
        {
            Add(1, 2.0m, 3.0m, 4, 6.8m);
            Add(1, 0.0m, 2.0m, 1, 1.2m);
            Add(3, 2.5m, 0.0m, 0, 7.5m);
            Add(10, 125.99m, 2.0m, 120, 1403.90m);
        }
    }
}
