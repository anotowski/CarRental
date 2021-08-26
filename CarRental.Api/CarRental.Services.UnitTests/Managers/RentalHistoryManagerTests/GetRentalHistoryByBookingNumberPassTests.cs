using System;
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
    public class GetRentalHistoryByBookingNumberPassTests
    {
        private readonly Fixture _fixture;
        private readonly Mock<IRentalHistoryRepository> _rentalHistoryRepositoryMock;

        public GetRentalHistoryByBookingNumberPassTests()
        {
            _fixture = new Fixture();
            _fixture.Customize(new AutoMoqCustomization());
            _rentalHistoryRepositoryMock = _fixture.FreezeMoq<IRentalHistoryRepository>();
        }

        [Fact]
        public async Task GetRentalHistoryByBookingNumber_WhenHappyPath_AssertCorrectRepositoryIsCalled()
        {
            // Arrange
            var bookingNumber = _fixture.Create<string>();
            _fixture.Inject((Car)null);
            var rentalHistory = _fixture.Create<RentalHistory>();

            _rentalHistoryRepositoryMock
                .Setup(x => x.GetRentalHistoryByBookingNumber(It.IsAny<string>()))
                .ReturnsAsync(rentalHistory);
            var manager = _fixture.Create<RentalHistoryManager>();

            // Act
            _ = await manager.GetRentalHistoryByBookingNumber(bookingNumber);

            // Assert
            _rentalHistoryRepositoryMock
                .Verify(x => x.GetRentalHistoryByBookingNumber(It.Is<string>(s => s.Equals(bookingNumber))),
                    Times.Once);
        }

        [Fact]
        public async Task GetRentalHistoryByBookingNumber_WhenHappyPath_AssertResultIsCorrect()
        {
            // Arrange
            var bookingNumber = _fixture.Create<string>();
            _fixture.Inject((Car)null);
            var rentalHistory = _fixture.Create<RentalHistory>();

            _rentalHistoryRepositoryMock
                .Setup(x => x.GetRentalHistoryByBookingNumber(It.IsAny<string>()))
                .ReturnsAsync(rentalHistory);
            var manager = _fixture.Create<RentalHistoryManager>();

            // Act
            var result = await manager.GetRentalHistoryByBookingNumber(bookingNumber);

            // Assert
            Assert.Equal(rentalHistory, result);
        }
    }
}
