using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using AutoFixture;
using CarRental.BusinessLogic.Generators.Interfaces;
using CarRental.Database.Models;
using CarRental.Services.Managers.Interfaces;
using CarRental.Test.Extensions;
using Moq;
using Xunit;

namespace CarRental.Services.UnitTests.RentServiceTests
{
    public class RentCarPassTests
    {
        private readonly Fixture _fixture;
        private readonly string _plateNumber;
        private readonly string _customerEmail;
        private readonly DateTime _customerBirthDate;
        private readonly string _bookingNumber;

        public RentCarPassTests()
        {
            _fixture = new Fixture();

            _plateNumber = _fixture.Create<string>();
            _customerEmail = _fixture.Create<string>();
            _customerBirthDate = DateTime.UtcNow.AddYears(-20);
            _bookingNumber = _fixture.Create<string>();
        }

        [Fact]
        public async Task RentCar_WhenHappyPath_AssertCorrectRentalHistoryIsAdded()
        {
            // Arrange
            var car = _fixture.Build<Car>()
                .With(x => x.RentalHistories, (ICollection<RentalHistory>) null)
                .With(x => x.PlateNumber, _plateNumber)
                .Create();

            _fixture.FreezeMoq<ICarRetrieveManager>()
                .Setup(x => x.TryGetCarByPlateNumber(It.Is<string>(s => s.Equals(_plateNumber))))
                .ReturnsAsync(car);

            var customer = _fixture.Build<Customer>()
                .With(x => x.Email, _customerEmail)
                .With(x=> x.BirthDate, _customerBirthDate)
                .Create();

            _fixture.FreezeMoq<ICustomerRetrieveManager>()
                .Setup(x => x.GetOrCreateCustomer(It.Is<string>(s => s.Equals(_customerEmail)),
                    It.Is<DateTime>(dt => dt.Equals(_customerBirthDate))))
                .ReturnsAsync(customer);

            _fixture.FreezeMoq<IBookingNumberGenerator>()
                .Setup(x => x.Generate(It.Is<string>(s => s.Equals(_plateNumber))))
                .Returns(_bookingNumber);

            var rentalHistoryManagerMock = _fixture.FreezeMoq<IRentalHistoryManager>();

            var service = _fixture.Create<RentService>();

            // Act
            _ = await service.RentCar(_plateNumber, _customerEmail, _customerBirthDate);

            // Assert
            rentalHistoryManagerMock
                .Verify(x=>x.RentCar(It.Is<RentalHistory>(
                    rh => rh.BookingNumber.Equals(_bookingNumber)
                    && !rh.MileageOnRentalEnd.HasValue
                    && !rh.RentEndDate.HasValue)),
                    Times.Once);
        }
    }
}
