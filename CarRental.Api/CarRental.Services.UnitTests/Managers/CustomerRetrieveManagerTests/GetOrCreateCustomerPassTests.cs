using System;
using System.Threading.Tasks;
using AutoFixture;
using CarRental.Database.Models;
using CarRental.Database.Repositories.Interfaces;
using CarRental.Services.Managers;
using CarRental.Test.Extensions;
using Moq;
using Xunit;

namespace CarRental.Services.UnitTests.Managers.CustomerRetrieveManagerTests
{
    public class GetOrCreateCustomerPassTests
    {
        private readonly Fixture _fixture;
        private readonly Mock<ICustomerRepository> _customerRepositoryMock;

        public GetOrCreateCustomerPassTests()
        {
            _fixture = new Fixture();
            _customerRepositoryMock = _fixture.FreezeMoq<ICustomerRepository>();
        }

        [Fact]
        public async Task GetOrCreateCustomer_WhenCustomerExists_AssertCorrectCustomerIsReturned()
        {
            // Arrange
            var email = _fixture.Create<string>();
            var birthDate = _fixture.Create<DateTime>();

            var customer = _fixture.Build<Customer>()
                .With(x => x.Email, email)
                .With(x => x.BirthDate, birthDate)
                .Create();

            _customerRepositoryMock
                .Setup(x => x.GetCustomerByEmail(It.Is<string>(e => e.Equals(email))))
                .ReturnsAsync(customer);

            var manager = _fixture.Create<CustomerRetrieveManager>();

            // Act
            var result = await manager.GetOrCreateCustomer(email, birthDate);

            // Assert
            Assert.Equal(customer, result);
        }

        [Fact]
        public async Task GetOrCreateCustomer_WhenCustomerDoesNotExist_AssertCorrectCustomerIsCreated()
        {
            // Arrange
            var email = _fixture.Create<string>();
            var birthDate = _fixture.Create<DateTime>();

            _customerRepositoryMock
                .Setup(x => x.GetCustomerByEmail(It.Is<string>(e => e.Equals(email))))
                .ReturnsAsync((Customer)null);

            var manager = _fixture.Create<CustomerRetrieveManager>();

            // Act
            var result = await manager.GetOrCreateCustomer(email, birthDate);

            // Assert
            _customerRepositoryMock
                .Verify(x=> x.CreateCustomer(It.Is<Customer>(c => c.Email.Equals(email) && c.BirthDate.Equals(birthDate))),
                    Times.Once);
        }

        [Fact]
        public async Task GetOrCreateCustomer_WhenCustomerDoesNotExist_AssertCorrectCustomerIsRetrieved()
        {
            // Arrange
            var email = _fixture.Create<string>();
            var birthDate = _fixture.Create<DateTime>();

            _customerRepositoryMock
                .Setup(x => x.GetCustomerByEmail(It.Is<string>(e => e.Equals(email))))
                .ReturnsAsync((Customer)null);

            var manager = _fixture.Create<CustomerRetrieveManager>();

            // Act
            var result = await manager.GetOrCreateCustomer(email, birthDate);

            // Assert
            _customerRepositoryMock
                .Verify(x => x.GetCustomerByEmail(It.Is<string>(e => e.Equals(email))),
                    Times.Once);
        }
    }
}
