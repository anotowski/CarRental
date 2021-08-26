using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoFixture;
using CarRental.Database.Models;
using CarRental.Database.Repositories.Interfaces;
using CarRental.Test.Extensions;
using Moq;
using Xunit;

namespace CarRental.Services.UnitTests.CarRentalServiceTests
{
    public class ListCarsPassTests
    {
        private readonly Fixture _fixture;
        private readonly Mock<ICarRepository> _carRentalRepositoryMock;

        public ListCarsPassTests()
        {
            _fixture = new Fixture();
            _carRentalRepositoryMock = _fixture.FreezeMoq<ICarRepository>();
        }

        [Fact]
        public async Task ListCars_WhenHappyPath_AssertCorrectCarIsReturned()
        {
            // Arrange
            var singleCarList = _fixture.Build<Car>()
                .With(x=>x.RentalHistories, (ICollection<RentalHistory>)null)
                .CreateMany(1)
                .ToList();

            _carRentalRepositoryMock
                .Setup(x => x.GetAllCarsList())
                .ReturnsAsync(singleCarList);

            var service = _fixture.Create<CarRentalService>();

            // Act
            var result = await service.ListCars();

            // Assert
            Assert.Equal(singleCarList.Single().ModelName, result.Single().ModelName);
            Assert.Equal(singleCarList.Single().BaseDayRentalFee, result.Single().DailyFee);
            Assert.Equal(singleCarList.Single().Brand, result.Single().Brand);
            Assert.Equal(singleCarList.Single().IsAvailable, result.Single().IsAvailable);
            Assert.Equal(singleCarList.Single().KilometerFee, result.Single().KilometerFee);
            Assert.Equal(singleCarList.Single().PlateNumber, result.Single().PlateNumber);
            Assert.Equal(singleCarList.Single().Category.ToString(), result.Single().Category);
        }

        [Fact]
        public async Task ListCars_WhenHappyPath_AssertNumberOfReturnedCarsIsCorrect()
        {
            // Arrange
            var cars = _fixture.Build<Car>()
                .With(x => x.RentalHistories, (ICollection<RentalHistory>)null)
                .CreateMany()
                .ToList();

            _carRentalRepositoryMock
                .Setup(x => x.GetAllCarsList())
                .ReturnsAsync(cars);

            var service = _fixture.Create<CarRentalService>();

            // Act
            var result = await service.ListCars();

            // Assert
            Assert.Equal(3, result.Count);
        }
    }
}
