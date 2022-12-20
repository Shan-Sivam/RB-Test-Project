using FluentAssertions;
using Microsoft.Extensions.Logging;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using Rabobank.TechnicalTest.GCOB.Controllers;
using System.Threading.Tasks;

namespace Rabobank.TechnicalTest.GCOB.Tests.Services
{
    [TestClass]
    public class CustomerControllerTest
    {

        [TestInitialize]
        public void Initialize()
        {

        }


        [TestMethod]
        public async Task GivenHaveACustomer_AndICallAServiceToGetTheCustomer_ThenTheCustomerIsReturned()
        {
            // Arrange
            var logger = Mock.Of<ILogger<CustomerController>>();

            var controller = new CustomerController(logger);

            // Act
            var result = await controller.Get();

            // Assert
            result.Should().HaveCount(0);
        }
    }
}