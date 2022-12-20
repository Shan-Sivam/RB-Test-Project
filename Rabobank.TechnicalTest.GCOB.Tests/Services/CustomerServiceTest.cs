using FluentAssertions;
using Microsoft.Extensions.Logging;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using Rabobank.TechnicalTest.GCOB.Dtos;
using Rabobank.TechnicalTest.GCOB.Repositories;
using Rabobank.TechnicalTest.GCOB.Services;
using Rabobank.TechnicalTest.GCOB.Tests.Fixtures;
using System;
using System.Threading.Tasks;

namespace Rabobank.TechnicalTest.GCOB.Tests.Services
{
    [TestClass]
    public class CustomerServiceTest
    {
        private ILogger<CustomerService> _logger;
        private Mock<ICustomerRepository> _mockCustomerRepository;

        [TestInitialize]
        public void Initialize()
        {
            _logger = Mock.Of<ILogger<CustomerService>>();

            _mockCustomerRepository = new Mock<ICustomerRepository>();
        }

        [TestMethod]
        [ExpectedException(typeof(Exception))]
        public async Task GivenHaveNoCustomer_AndICallAServiceToGetTheCustomer_ThenItThrowsAnException()
        {
            // Arrange
            _mockCustomerRepository
               .Setup(service => service.GetAsync(It.IsAny<int>()))
               .Throws<Exception>();

            var sut = new CustomerService(_logger, _mockCustomerRepository.Object);

            // Act
            await sut.GetCustomer(1);
        }

        [TestMethod]
        public async Task GivenHaveACustomer_AndICallAServiceToGetTheCustomer_ThenTheCustomerIsReturned()
        {
            // Arrange
            var id = 1;
            var newCustomerDto = CustomerFixture.GetTestNewCustomer();
            newCustomerDto.Id = id;

            _mockCustomerRepository
              .Setup(service => service.GetAsync(It.IsAny<int>()))
              .ReturnsAsync(newCustomerDto);

            var sut = new CustomerService(_logger, _mockCustomerRepository.Object);

            // Act
            var result = await sut.GetCustomer(id);

            // Assert
            result.Should().NotBeNull();
            result.Id.Should().Be(id);
        }

        [TestMethod]
        public async Task GivenInsertACustomer_AndICallAServiceToGetTheCustomer_ThenTheCustomerIsIOnSerted_AndTheCustomerIsReturned()
        {
            // Arrange

            var nextId = 1;
            var newCustomerDto = CustomerFixture.GetTestNewCustomer();

            _mockCustomerRepository
                .Setup(service => service.GenerateIdentityAsync())
                .ReturnsAsync(nextId);

            _mockCustomerRepository
                .Setup(service => service.InsertAsync(It.IsAny<CustomerDto>()));
                     

            var sut = new CustomerService(_logger, _mockCustomerRepository.Object);

            // Act
            var result = await sut.CreateCustomer(newCustomerDto);

            result.Id.Should().Be(nextId);
            result.FullName.Should().Be($"{newCustomerDto.FirstName} {newCustomerDto.LastName}");
        }
    }
}