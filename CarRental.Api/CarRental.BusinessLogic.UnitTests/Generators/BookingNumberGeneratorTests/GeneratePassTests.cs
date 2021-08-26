using System;
using AutoFixture;
using CarRental.BusinessLogic.Generators;
using Xunit;

namespace CarRental.BusinessLogic.UnitTests.Generators.BookingNumberGeneratorTests
{
    public class GeneratePassTests
    {
        private readonly IFixture _fixture;

        public GeneratePassTests()
        {
            _fixture = new Fixture();
        }

        [Fact]
        public void Generate_WhenHappyPath_AssertResultIsCorrect()
        {
            // Arrange
            var plateNumber = _fixture.Create<string>();
            var expectedResult = $"{plateNumber}/{DateTime.UtcNow:dd-MM-yyyy-hh-mm}";

            var generator = _fixture.Create<BookingNumberGenerator>();

            // Act
            var result = generator.Generate(plateNumber);

            // Assert
            Assert.Equal(expectedResult, result);
        }
    }
}
