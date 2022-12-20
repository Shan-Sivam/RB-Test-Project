using FluentAssertions;
using Microsoft.Extensions.Logging;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using Rabobank.TechnicalTest.GCOB.Controllers;
using Rabobank.TechnicalTest.GCOB.Dtos;
using Rabobank.TechnicalTest.GCOB.Services;
using Rabobank.TechnicalTest.GCOB.Tests.Fixtures;
using System.Threading.Tasks;

namespace Rabobank.TechnicalTest.GCOB.Tests.Services
{
    [TestClass]
    public class CustomerControllerTest
    {

        private ILogger<CustomerController> _logger;
        private Mock<ICustomerService> _mockCustomerService;

        [TestInitialize]
        public void Initialize()
        {
            _logger = Mock.Of<ILogger<CustomerController>>();
            _mockCustomerService = new Mock<ICustomerService>();
        }


        [TestMethod]
        public async Task GivenHaveACustomer_AndICallAServiceToGetOneCustomer_ThenItCallsExactlyOnce()
        {
            // Arrange
            _mockCustomerService
                .Setup(service => service.GetCustomer(It.IsAny<int>()))
                .ReturnsAsync(new Customer());

            var sut = new CustomerController(_logger, _mockCustomerService.Object);

            // Act
            var result = await sut.Get(0);

            // Assert
            _mockCustomerService.Verify(
                service => service.GetCustomer(It.IsAny<int>()),
                Times.Once()
               );
        }
        
        [TestMethod]
        public async Task GivenHaveACustomer_AndICallAServiceToGetOneCustomer_ThenItReturnsTheCustomer()
        {
            // Arrange

            var testCustomer = CustomerFixture.GetTestCustomer();

            _mockCustomerService
                .Setup(service => service.GetCustomer(It.IsAny<int>()))
                .ReturnsAsync(testCustomer);

            var sut = new CustomerController(_logger, _mockCustomerService.Object);

            // Act
            var result = await sut.Get(0);

            // Assert
            result.Should().NotBeNull();
            result.Should().BeSameAs(testCustomer);
        }

        [TestMethod]
        public async Task GivenANewCustomer_AndICallAServiceToCreateOne_ThenItCallsExactlyOnce()
        {
            // Arrange
            _mockCustomerService
                .Setup(service => service.CreateCustomer(It.IsAny<CustomerDto>()))
                .ReturnsAsync(new Customer());

            var sut = new CustomerController(_logger, _mockCustomerService.Object);

            // Act
            var result = await sut.Post(new CustomerDto());

            // Assert
            _mockCustomerService.Verify(
                service => service.CreateCustomer(It.IsAny<CustomerDto>()),
                Times.Once()
               );
        }

        [TestMethod]
        public async Task GivenANewCustomer_AndICallAServiceToCreateOne_ThenItReturnsANewCustomer()
        {
            // Arrange

            var customer = CustomerFixture.GetTestNewCustomer();
            var newId = 1;

            _mockCustomerService
                .Setup(service => service.CreateCustomer(It.IsAny<CustomerDto>()))
                .ReturnsAsync(
                new Customer { Id = newId, FullName = $"{customer.FirstName} {customer.LastName}"}
                );

            var sut = new CustomerController(_logger, _mockCustomerService.Object);

            // Act
            var result = await sut.Post(customer);

            // Assert
            result.Should().NotBeNull();
            result.Id.Should().Be(newId);
        }
    }
}