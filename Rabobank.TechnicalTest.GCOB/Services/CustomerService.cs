using Microsoft.Extensions.Logging;
using Rabobank.TechnicalTest.GCOB.Controllers;
using Rabobank.TechnicalTest.GCOB.Dtos;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;

namespace Rabobank.TechnicalTest.GCOB.Services
{
    public class CustomerService : ICustomerService
    {
        private readonly ILogger<CustomerService> _logger;

        public CustomerService(ILogger<CustomerService> logger)
        {
            _logger = logger;
        }

        public async Task<IEnumerable<Customer>> GetAllCustomers()
        {
            throw new NotImplementedException();

        }

        public async Task<Customer> GetCustomer(int customerId)
        {
            throw new NotImplementedException();

        }

        public async Task<Customer> CreateCustomer(CustomerDto customer)
        {
            throw new NotImplementedException();
        }
    }
}
