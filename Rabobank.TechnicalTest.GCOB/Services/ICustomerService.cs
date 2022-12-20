using Rabobank.TechnicalTest.GCOB.Dtos;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Rabobank.TechnicalTest.GCOB.Services
{
    public interface ICustomerService
    {
        Task<Customer> CreateCustomer(CustomerDto customer);

        Task<Customer> GetCustomer(int customerId);
    }
}