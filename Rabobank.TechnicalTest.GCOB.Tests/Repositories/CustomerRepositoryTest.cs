using FluentAssertions;
using Microsoft.Extensions.Logging;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using Rabobank.TechnicalTest.GCOB.Repositories;
using Rabobank.TechnicalTest.GCOB.Tests.Fixtures;
using System;
using System.Threading.Tasks;

namespace Rabobank.TechnicalTest.GCOB.Tests.Services
{
    [TestClass]
    public class CustomerRepositoryTest
    {
        private ILogger<InMemoryCustomerRepository> _logger;

        [TestInitialize]
        public void Initialize()
        {
            _logger = Mock.Of<ILogger<InMemoryCustomerRepository>>();
        }

        [TestMethod]
        public async Task GivenHaveACustomer_AndIGetNoCustomerFromTheDB_ThenGenerateIdentity_ShouldReturn_1()
        {
            // Arrange
            var sut = new InMemoryCustomerRepository(_logger);

            // Act
            var result = await sut.GenerateIdentityAsync();

            // Assert
            result.Should().Be(1);
        }

        [TestMethod]
        public async Task GivenHaveACustomer_AndIGetTheCustomerFromTheDB_ThenGenerateIdentityShould_ReturnNextValue()
        {
            // Arrange
            var newCustomer = CustomerFixture.GetTestNewCustomer();
            newCustomer.Id = 1;
            var sut = new InMemoryCustomerRepository(_logger);
            sut.Customers.TryAdd(newCustomer.Id, newCustomer);

            // Act
            var result = await sut.GenerateIdentityAsync();

            // Assert
            result.Should().Be(newCustomer.Id + 1);
        }


        [TestMethod]
        public async Task GivenHaveACustomer_AndIAddNewCustomer_ThenInsertAsyncShould_AddNewOne()
        {
            // Arrange
            var sut = new InMemoryCustomerRepository(_logger);

            var customer = CustomerFixture.GetTestNewCustomer();
            customer.Id = 1;

            

            // Act
            await sut.InsertAsync(customer);

            // Assert
            var totalCustomers = sut.Customers.Count;
            totalCustomers.Should().Be(1);
        }

        [TestMethod]
        [ExpectedException(typeof(Exception))]
        public async Task GivenHaveACustomer_AndIAddAnExistingCustomer_ThenInsertAsyncShould_ThrowException()
        {
            // Arrange
            var sut = new InMemoryCustomerRepository(_logger);
            var customer = CustomerFixture.GetTestNewCustomer();
            customer.Id = 1;
            
            sut.Customers.TryAdd(customer.Id, customer);

            // Act
            await sut.InsertAsync(customer);
        }

        [TestMethod]
        [ExpectedException(typeof(Exception))]
        public async Task GivenHaveACustomer_AndIHaveNoCustomerInTheDB_ThenGetAsyncShould_ThrowException()
        {
            // Arrange
            var sut = new InMemoryCustomerRepository(_logger);

            await sut.GetAsync(1);
        }


        [TestMethod]
        public async Task GivenHaveACustomer_AndIGetTheCustomerFromTheDB_ThenTheCustomerIsRetrieved()
        {
            // Arrange
            var sut = new InMemoryCustomerRepository(_logger);
            var customer = CustomerFixture.GetTestNewCustomer();
            customer.Id = 1;

            sut.Customers.TryAdd(customer.Id, customer);

            // Act
            var result = await sut.GetAsync(customer.Id);

            // Assert
            result.Should().BeSameAs(customer);
        }

        [TestMethod]
        [ExpectedException(typeof(Exception))]
        public async Task GivenHaveACustomer_AndIUpdateTheCustomer_ThenUpdateAsyncShoud_ThrowException()
        {
            // Arrange
            var sut = new InMemoryCustomerRepository(_logger);
            var customer = CustomerFixture.GetTestNewCustomer();
            customer.Id = 1;

            // Act
            await sut.UpdateAsync(customer);
        }

        [TestMethod]
        public async Task GivenHaveACustomer_AndIUpdateTheCustomer_ThenUpdateAsyncShoud_UpdatTheCustomer()
        {
            // Arrange
            var sut = new InMemoryCustomerRepository(_logger);
            var customer = CustomerFixture.GetTestNewCustomer();
            customer.Id = 1;

            sut.Customers.TryAdd(customer.Id, customer);

            customer.FirstName = "Changed";

            // Act
             await sut.UpdateAsync(customer);

            // Assert
            var changedCustomer = sut.Customers[customer.Id];
            
            changedCustomer.FirstName.Should().Be(customer.FirstName);
        }
    }
}
