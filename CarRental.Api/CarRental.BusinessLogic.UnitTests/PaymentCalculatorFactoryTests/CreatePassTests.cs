using System;
using System.Collections.Generic;
using System.Text;
using AutoFixture;
using CarRental.BusinessLogic.Calculators;
using CarRental.BusinessLogic.Calculators.Interfaces;
using CarRental.Database.Models;
using CarRental.Test.Extensions;
using Xunit;

namespace CarRental.BusinessLogic.UnitTests.PaymentCalculatorFactoryTests
{
    public class CreatePassTests
    {
        private readonly Fixture _fixture;

        public CreatePassTests()
        {
            _fixture = new Fixture();
        }

        [Fact]
        public void Create_WhenCategoryTypeIsCompact_AssertResultIsCorrect()
        {
            // Arrange
            var factory = _fixture.Create<PaymentCalculatorFactory>();

            // Act
            var result = factory.Create(CategoryType.Compact);

            // Assert
            Assert.IsType<CompactCarTypePaymentCalculator>(result);
        }

        [Fact]
        public void Create_WhenCategoryTypeIsPremium_AssertResultIsCorrect()
        {
            // Arrange
            var factory = _fixture.Create<PaymentCalculatorFactory>();

            // Act
            var result = factory.Create(CategoryType.Premium);

            // Assert
            Assert.IsType<PremiumCarTypePaymentCalculator>(result);
        }

        [Fact]
        public void Create_WhenCategoryTypeIsMinivan_AssertResultIsCorrect()
        {
            // Arrange
            var factory = _fixture.Create<PaymentCalculatorFactory>();

            // Act
            var result = factory.Create(CategoryType.Minivan);

            // Assert
            Assert.IsType<MinivanCarTypePaymentCalculator>(result);
        }

        [Theory]
        [MemberData(nameof(EnumExtensions<CategoryType>.ExcludeEnum), new[] { CategoryType.Unknown }, MemberType = typeof(EnumExtensions<CategoryType>))]
        public void Create_WhenCategoryTypeIsCorrect_AssertMethodDoesntThrowException(CategoryType categoryType)
        {
            // Arrange
            var factory = _fixture.Create<PaymentCalculatorFactory>();

            // Act
            var result = Record.Exception(() => factory.Create(categoryType));

            // Assert
            Assert.Null(result);
        }
    }
}
