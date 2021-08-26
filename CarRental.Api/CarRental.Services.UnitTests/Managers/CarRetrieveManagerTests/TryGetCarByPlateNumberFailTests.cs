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
    public class TryGetCarByPlateNumberFailTests
    {
        private readonly Fixture _fixture;
        private readonly Mock<ICarRepository> _carRepositoryMock;


        public TryGetCarByPlateNumberFailTests()
        {
            _fixture = new Fixture();
            _carRepositoryMock = _fixture.FreezeMoq<ICarRepository>();
        }

        [Fact]
        public async Task TryGetCarByPlateNumberMethod_WhenCarDoesNotExist_ThrownExceptionIsCorrect()
        {
            // Arrange
            _carRepositoryMock
                .Setup(x => x.GetCarByPlateNumber(It.IsAny<string>()))
                .ReturnsAsync((Car)null);

            var plateNumber = _fixture.Create<string>();

            var expectedMessage = $"Couldn't find car with plate number: '{plateNumber}'";
            var manager = _fixture.Create<CarRetrieveManager>();

            // Act
            var exception = await Record.ExceptionAsync(() => manager.TryGetCarByPlateNumber(plateNumber));

            // Assert
            Assert.Equal(expectedMessage, exception.Message);
        }

        [Fact]
        public async Task TryGetCarByPlateNumberMethod_WhenCarIsNotAvailable_ThrownExceptionIsCorrect()
        {
            // Arrange
            _fixture.Inject(Enumerable.Empty<RentalHistory>()); // https://github.com/AutoFixture/AutoFixture/blob/master/Src/AutoFixture/OmitOnRecursionBehavior.cs
            var car = _fixture.Build<Car>()
                .With(x => x.IsAvailable, false)
                .Create();

            _carRepositoryMock
                .Setup(x => x.GetCarByPlateNumber(It.IsAny<string>()))
                .ReturnsAsync(car);

            var plateNumber = _fixture.Create<string>();

            var expectedMessage = $"Given car with plate number: '{plateNumber}' is not available.";
            var manager = _fixture.Create<CarRetrieveManager>();

            // Act
            var exception = await Record.ExceptionAsync(() => manager.TryGetCarByPlateNumber(plateNumber));

            // Assert
            Assert.Equal(expectedMessage, exception.Message);
        }
    }
}
