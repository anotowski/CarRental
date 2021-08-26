using System.Collections.Generic;
using System.Threading.Tasks;
using AutoFixture;
using CarRental.Database.Models;
using CarRental.Database.Repositories.Interfaces;
using CarRental.Services.Managers;
using CarRental.Test.Extensions;
using Moq;
using Xunit;

namespace CarRental.Services.UnitTests.Managers.RentalHistoryManagerTests
{
    public class RentCarPassTests
    {
        private readonly Fixture _fixture;
        private readonly Mock<IRentalHistoryRepository> _rentalRepositoryMock;
        private readonly Mock<ICarRepository> _carRepositoryMock;

        public RentCarPassTests()
        {
            _fixture = new Fixture();
            _rentalRepositoryMock = _fixture.FreezeMoq<IRentalHistoryRepository>();
            _carRepositoryMock = _fixture.FreezeMoq<ICarRepository>();
        }

        [Fact]
        public async Task RentCar_WhenHappyPath_AssertRentalHistoryIsAdded()
        {
            // Arrange
            var car = _fixture.Build<Car>()
                .With(x => x.RentalHistories, (ICollection<RentalHistory>)null)
                .With(x => x.IsAvailable, true)
                .Create();

            var rentalHistory = _fixture.Build<RentalHistory>()
                .With(x => x.Car, car)
                .Create();

            var manager = _fixture.Create<RentalHistoryManager>();

            // Act
            await manager.RentCar(rentalHistory);

            // Assert
            _rentalRepositoryMock
                .Verify(x => x.AddRentalHistory(It.Is<RentalHistory>(r => r.Equals(rentalHistory))),
                    Times.Once);
        }

        [Fact]
        public async Task RentCar_WhenHappyPath_AssertCarStatusIsUpdated()
        {
            // Arrange
            var car = _fixture.Build<Car>()
                .With(x => x.RentalHistories, (ICollection<RentalHistory>)null)
                .With(x => x.IsAvailable, true)
                .Create();

            var rentalHistory = _fixture.Build<RentalHistory>()
                .With(x => x.Car, car)
                .Create();

            var manager = _fixture.Create<RentalHistoryManager>();

            // Act
            await manager.RentCar(rentalHistory);

            // Assert
            _carRepositoryMock
                .Verify(x => x.UpdateCarStatus(It.Is<Car>(c => c.IsAvailable == false && c.Equals(car))),
                    Times.Once);
        }
    }
}
