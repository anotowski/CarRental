using System;
using System.Linq;
using System.Threading.Tasks;
using AutoFixture;
using CarRental.Database.Models;
using CarRental.Database.Repositories.Interfaces;
using CarRental.Services.Managers;
using CarRental.Test.Extensions;
using Moq;
using Xunit;

namespace CarRental.Services.UnitTests.Managers.CarRetrieveManagerTests
{
    public class TryGetCarByPlateNumberPassTests
    {
        private readonly Fixture _fixture;
        private readonly Mock<ICarRepository> _carRepositoryMock;
        private readonly string _carPlateNumber;
        private readonly Car _car;

        public TryGetCarByPlateNumberPassTests()
        {
            _fixture = new Fixture();
            _carRepositoryMock = _fixture.FreezeMoq<ICarRepository>();
            _carPlateNumber = _fixture.Create<string>();

            _fixture.Inject(Enumerable.Empty<RentalHistory>());
            _car = _fixture.Build<Car>()
                .With(x => x.IsAvailable, true)
                .Create();

            _carRepositoryMock
                .Setup(x => x.GetCarByPlateNumber(It.Is<string>(x => x.Equals(_carPlateNumber))))
                .ReturnsAsync(_car);
        }

        [Fact]
        public async Task TryGetCarByPlateNumber_WhenHappyPath_AssertRepositoryIsCalled()
        {
            // Arrange
            var manager = _fixture.Create<CarRetrieveManager>();

            // Act
            var result = await manager.TryGetCarByPlateNumber(_carPlateNumber);

            // Assert
            _carRepositoryMock
                .Verify(x=>x.GetCarByPlateNumber(It.Is<string>(x=> x.Equals(_carPlateNumber))),
                    Times.Once);
        }

        [Fact]
        public async Task TryGetCarByPlateNumber_WhenHappyPath_AssertCorrectResultIsReturned()
        {
            // Arrange
            var manager = _fixture.Create<CarRetrieveManager>();

            // Act
            var result = await manager.TryGetCarByPlateNumber(_carPlateNumber);

            // Assert
            Assert.Equal(_car, result);
        }
    }
}
