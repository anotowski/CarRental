using System.Threading.Tasks;
using AutoFixture;
using AutoFixture.AutoMoq;
using CarRental.Database.Models;
using CarRental.Database.Repositories.Interfaces;
using CarRental.Services.Managers;
using CarRental.Test.Extensions;
using Moq;
using Xunit;

namespace CarRental.Services.UnitTests.Managers.RentalHistoryManagerTests
{
    public class GetRentalHistoryByBookingNumberFailTests
    {
        private readonly Fixture _fixture;
        private readonly Mock<IRentalHistoryRepository> _rentalHistoryRepositoryMock;

        public GetRentalHistoryByBookingNumberFailTests()
        {
            _fixture = new Fixture();
            _fixture.Customize(new AutoMoqCustomization());
            _rentalHistoryRepositoryMock = _fixture.FreezeMoq<IRentalHistoryRepository>();
        }

        [Fact]
        public async Task GetRentalHistoryByBookingNumber_WhenRentalHistoryDoesNotExist_AssertThrownExceptionIsCorrect()
        {
            // Arrange
            var bookingNumber = _fixture.Create<string>();

            _rentalHistoryRepositoryMock
                .Setup(x => x.GetRentalHistoryByBookingNumber(It.IsAny<string>()))
                .ReturnsAsync((RentalHistory) null);

            var manager = _fixture.Create<RentalHistoryManager>();
            var expectedMessage = $"Couldn't find a rent with a booking number: '{bookingNumber}'";

            // Act
            var exception = await Record.ExceptionAsync(() => manager.GetRentalHistoryByBookingNumber(bookingNumber));

            // Assert
            Assert.Equal(expectedMessage, exception.Message);
        }
    }
}
