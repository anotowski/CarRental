using System;
using System.Globalization;
using System.Threading;
using System.Threading.Tasks;
using AutoFixture;
using AutoFixture.AutoMoq;
using Xunit;

namespace CarRental.Services.UnitTests.RentServiceTests
{
    public class RentCarFailTests
    {
        private const int MaximumAge = 100;
        private const int MinimumAge = 15;

        private readonly Fixture _fixture;
        private readonly DateTime _customerBirthDate;
        private readonly string _plateNumber;
        private readonly string _customerEmail;

        public RentCarFailTests()
        {
            _fixture = new Fixture();
            _fixture.Customize(new AutoMoqCustomization());

            _plateNumber = _fixture.Create<string>();
            _customerEmail = _fixture.Create<string>();
            _customerBirthDate = _fixture.Create<DateTime>();
        }

        [Theory]
        [InlineData("")]
        [InlineData("  ")]
        [InlineData(null)]
        public async Task RentCar_WhenPlateNumberIsEmpty_AssertThrownExceptionIsCorrect(string plateNumber)
        {
            // Arrange
            Thread.CurrentThread.CurrentUICulture = new CultureInfo("en-Us");
            var expectedMessage = "Value cannot be null. (Parameter 'plateNumber')";

            var service = _fixture.Create<RentService>();

            // Act
            var exception =
                await Record.ExceptionAsync(() => service.RentCar(plateNumber, _customerEmail, _customerBirthDate));

            // Assert
            Assert.Equal(expectedMessage, exception.Message);
        }

        [Theory]
        [InlineData("")]
        [InlineData("  ")]
        [InlineData(null)]
        public async Task RentCar_WhenCustomerEmailIsEmpty_AssertThrownExceptionIsCorrect(string customerEmail)
        {
            // Arrange
            Thread.CurrentThread.CurrentUICulture = new CultureInfo("en-Us");
            var expectedMessage = "Value cannot be null. (Parameter 'customerEmail')";

            var service = _fixture.Create<RentService>();

            // Act
            var exception =
                await Record.ExceptionAsync(() => service.RentCar(_plateNumber, customerEmail, _customerBirthDate));

            // Assert
            Assert.Equal(expectedMessage, exception.Message);
        }

        [Fact]
        public async Task RentCar_WhenCustomerBirthDateIsLessThanExpected_AssertThrownExceptionIsCorrect()
        {
            // Arrange
            var customerDateOfBirth = DateTime.UtcNow.AddYears(-MaximumAge);
            var expectedMessage = $"Given customer date of birth '{customerDateOfBirth}' is out of range.";

            var service = _fixture.Create<RentService>();

            // Act
            var exception =
                await Record.ExceptionAsync(() => service.RentCar(_plateNumber, _customerEmail, customerDateOfBirth));

            // Assert
            Assert.Equal(expectedMessage, exception.Message);
        }

        [Fact]
        public async Task RentCar_WhenCustomerBirthDateIsGreaterThanExpected_AssertThrownExceptionIsCorrect()
        {
            // Arrange
            var customerDateOfBirth = DateTime.UtcNow.AddYears(-MinimumAge);
            var expectedMessage = $"Given customer date of birth '{customerDateOfBirth}' is out of range.";

            var service = _fixture.Create<RentService>();

            // Act
            var exception =
                await Record.ExceptionAsync(() => service.RentCar(_plateNumber, _customerEmail, customerDateOfBirth));

            // Assert
            Assert.Equal(expectedMessage, exception.Message);
        }
    }
}
