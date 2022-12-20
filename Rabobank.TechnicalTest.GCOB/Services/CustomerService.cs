using Microsoft.Extensions.Logging;
using Rabobank.TechnicalTest.GCOB.Controllers;
using Rabobank.TechnicalTest.GCOB.Dtos;
using Rabobank.TechnicalTest.GCOB.Repositories;
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
        private readonly ICustomerRepository _customerRepository;

        public CustomerService(
            ILogger<CustomerService> logger,
            ICustomerRepository customerRepository
            )
        {
            _logger = logger;
            _customerRepository = customerRepository;
        }

        public async Task<Customer> GetCustomer(int customerId)
        {
            var customer = await _customerRepository.GetAsync(customerId);

            return MapToCustomer(customer);
           
        }

        public async Task<Customer> CreateCustomer(CustomerDto customer)
        {
            var newId = await _customerRepository.GenerateIdentityAsync();

            customer.Id = newId;

            await _customerRepository.InsertAsync(customer);

            return MapToCustomer(customer);
        }


        private Customer MapToCustomer(CustomerDto customerDto)
        {
            return new Customer
            {
                Id = customerDto.Id,
                FullName = $"{customerDto.FirstName} {customerDto.LastName}",
            };
        }
    }
}
