using AutoFixture;
using CarRental.BusinessLogic.Calculators;
using Xunit;

namespace CarRental.BusinessLogic.UnitTests.Calculators.CompactCarTypePaymentCalculatorTests
{
    public class CalculatePassTests
    {
        private IFixture _fixture;

        public CalculatePassTests()
        {
            _fixture = new Fixture();
        }

        [Theory]
        [ClassData(typeof(CompactCarTypeTestData))]
        public void Calculate_WhenHappyPath_AssertResultIsCorrect(int numberOfDays,
            decimal dailyFee,
            decimal kilometerFee,
            int numberOfKilometers,
            decimal expectedResult)
        {
            // Arrange
            var calculator = _fixture.Create<CompactCarTypePaymentCalculator>();

            // Act
            var result = calculator.Calculate(numberOfDays, dailyFee, kilometerFee, numberOfKilometers);

            // Assert
            Assert.Equal(expectedResult, result);
        }
    }

    public class CompactCarTypeTestData : TheoryData<int, decimal, decimal, int, decimal>
    {
        public CompactCarTypeTestData()
        {
            Add(1, 2.0m, 3.0m, 4, 2.0m);
            Add(1, 0.0m, 2.0m, 1, 0.0m);
            Add(3, 2.5m, 0.0m, 0, 7.5m);
            Add(10, 125.99m, 2.0m, 120, 1259.9m);
        }
    }
}
