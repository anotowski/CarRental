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
    public class ReturnCarPassTests
    {
        private readonly Fixture _fixture;
        private readonly Mock<IRentalHistoryRepository> _rentalHisotryRepositoryMock;
        private readonly Mock<ICarRepository> _carRepositoryMock;
        private readonly RentalHistory _rentalHistory;

        public ReturnCarPassTests()
        {
            _fixture = new Fixture();
            _rentalHisotryRepositoryMock = _fixture.FreezeMoq<IRentalHistoryRepository>();
            _carRepositoryMock = _fixture.FreezeMoq<ICarRepository>();

            var car = _fixture.Build<Car>()
                .With(x => x.IsAvailable, false)
                .With(x => x.RentalHistories, (ICollection<RentalHistory>) null)
                .Create();

             _rentalHistory = _fixture.Build<RentalHistory>()
                .With(x => x.Car, car)
                .Create();
        }

        [Fact]
        public async Task ReturnCar_WhenHappyPath_AssertRentalHistoryIsUpdated()
        {
            // Arrange
            var manager = _fixture.Create<RentalHistoryManager>();

            // Act
            await manager.ReturnCar(_rentalHistory);

            // Assert
            _rentalHisotryRepositoryMock
                .Verify(x=>x.UpdateRentalHistory(It.Is<RentalHistory>(r=>r.Equals(_rentalHistory))),
                    Times.Once);
        }

        [Fact]
        public async Task ReturnCar_WhenHappyPath_AssertCarIsUpdated()
        {
            // Arrange
            var manager = _fixture.Create<RentalHistoryManager>();

            // Act
            await manager.ReturnCar(_rentalHistory);

            // Assert
           _carRepositoryMock
               .Verify(x=>x.UpdateCarStatus(It.Is<Car>(c => c.IsAvailable == true && c.Equals(_rentalHistory.Car))),
                   Times.Once);
        }
    }
}
