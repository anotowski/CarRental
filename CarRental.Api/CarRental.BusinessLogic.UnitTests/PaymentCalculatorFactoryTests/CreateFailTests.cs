using AutoFixture;
using CarRental.Database.Models;
using Xunit;

namespace CarRental.BusinessLogic.UnitTests.PaymentCalculatorFactoryTests
{
    public class CreateFailTests
    {
        private readonly Fixture _fixture;
        
        public CreateFailTests()
        {
            _fixture = new Fixture();
        }

        [Fact]
        public void Create_WhenInvalidTypeIsPassed_AssertExceptionIsThrown()
        {
            // Arrange
            var factory = _fixture.Create<PaymentCalculatorFactory>();
            var categoryType = CategoryType.Unknown;

            var expectedMessage = $"Given category type: '{categoryType}' is not supported for billing.";

            // Act
            var exception = Record.Exception(() => factory.Create(categoryType));

            // Assert
            Assert.Equal(expectedMessage, exception.Message);
        }

    }
}
