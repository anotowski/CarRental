using System;
using System.Collections.Generic;
using AutoFixture;
using CarRental.BusinessLogic.Calculators.Interfaces;
using CarRental.BusinessLogic.Interfaces;
using CarRental.Database.Models;
using CarRental.Services.Helpers;
using CarRental.Test.Extensions;
using Moq;
using Xunit;

namespace CarRental.Services.UnitTests.Helpers.RentalHistoryPaymentCalculatorTests
{
    public class CalculatePaymentPassTests
    {
        private readonly Fixture _fixture;
        private readonly Mock<IPaymentCalculatorFactory> _paymentCalculatorFactoryMock;
        private readonly Mock<IPaymentCalculator> _paymentCalculatorMock;
        private readonly decimal _paymentValue;

        public CalculatePaymentPassTests()
        {
            _fixture = new Fixture();

            _paymentValue = 123.0m;
            _paymentCalculatorFactoryMock = _fixture.FreezeMoq<IPaymentCalculatorFactory>();

            _paymentCalculatorMock = _fixture.FreezeMoq<IPaymentCalculator>();
            _paymentCalculatorMock
                .Setup(x => x.Calculate(It.IsAny<int>(),
                It.IsAny<decimal>(),
                It.IsAny<decimal>(),
                It.IsAny<int>()))
            .Returns(_paymentValue);

            _paymentCalculatorFactoryMock
                .Setup(x => x.Create(It.IsAny<CategoryType>()))
                .Returns(_paymentCalculatorMock.Object);
        }

        [Fact]
        public void CalculatePayment_WhenHappyPath_AssertResultIsCorrect()
        {
            // Arrange
            var car = _fixture.Build<Car>()
                .With(x => x.RentalHistories, (ICollection<RentalHistory>) null)
                .With(x=>x.Category, CategoryType.Compact)
                .Create();

            var rentalHistory = _fixture.Build<RentalHistory>()
                .With(x => x.Car, car)
                .Create();

            var returnDate = DateTime.UtcNow;
            var currentMileage = _fixture.Create<int>();

            var calculator = _fixture.Create<RentalHistoryPaymentCalculator>();

            // Act
            var result = calculator.CalculatePayment(rentalHistory, returnDate, currentMileage);

            // Assert
            Assert.Equal(_paymentValue, result);
        }
    }
}
