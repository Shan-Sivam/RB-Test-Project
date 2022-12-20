using Rabobank.TechnicalTest.GCOB.Dtos;
using System;
using System.Collections.Generic;

namespace Rabobank.TechnicalTest.GCOB.Tests.Fixtures
{
    public static class CustomerFixture
    {
        public static Customer GetTestCustomer() =>
            new Customer
            {
                Id = 1,
                FullName = "Test Customer 1",
                City = "London",
                Street = "Test Street 1",
                Country = "United Kingdom",
                Postcode = "NR4 6HH"
            };

        public static CustomerDto GetTestNewCustomer() => 
            new CustomerDto
            {
                FirstName = "New",
                LastName = "Customer",
                AddressId = 1,
            };
    }
}
