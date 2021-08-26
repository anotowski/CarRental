using AutoFixture;
using CarRental.BusinessLogic.Calculators;
using Xunit;

namespace CarRental.BusinessLogic.UnitTests.Calculators.MinivanCarTypePaymentCalculatorTests
{
    public class CalculatePassTests
    {
        private readonly IFixture _fixture;

        public CalculatePassTests()
        {
            _fixture = new Fixture();
        }

        [Fact]
        public void MinivanCarTypeDailyFeeMultiplier_GetObjectValue_AssertValueIsCorrect()
        {
            // Arrange
            var expectedValue = 1.7m;

            // Act
            // Assert
            Assert.Equal(expectedValue, MinivanCarTypePaymentCalculator.MinivanCarTypeDailyFeeMultiplier);
        }

        [Fact]
        public void MinivanCarTypeKilometerFeeMultiplier_GetObjectValue_AssertValueIsCorrect()
        {
            // Arrange
            var expectedValue = 1.5m;

            // Act
            // Assert
            Assert.Equal(expectedValue, MinivanCarTypePaymentCalculator.MinivanCarTypeKilometerFeeMultiplier);
        }

        [Theory]
        [ClassData(typeof(MinivanCarTypeTestData))]
        public void Calculate_WhenHappyPath_AssertResultIsCorrect(int numberOfDays,
            decimal dailyFee,
            decimal kilometerFee,
            int numberOfKilometers,
            decimal expectedResult)
        {
            // Arrange
            var calculator = _fixture.Create<MinivanCarTypePaymentCalculator>();

            // Act
            var result = calculator.Calculate(numberOfDays, dailyFee, kilometerFee, numberOfKilometers);

            // Assert
            Assert.Equal(expectedResult, result);
        }
    }

    public class MinivanCarTypeTestData : TheoryData<int, decimal, decimal, int, decimal>
    {
        public MinivanCarTypeTestData()
        {
            Add(1, 2.0m, 3.0m, 4, 21.40m);
            Add(1, 0.0m, 2.0m, 1, 3.0m);
            Add(3, 2.5m, 0.0m, 0, 12.75m);
            Add(10, 125.99m, 2.0m, 120, 2501.83m);
        }
    }
}
