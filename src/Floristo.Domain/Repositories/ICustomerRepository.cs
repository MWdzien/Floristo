using Floristo.Domain.Entities;
using Floristo.Domain.ValueObjects;

namespace Floristo.Domain.Repositories;

public interface ICustomerRepository
{
    Task<Customer> GetCustomerAsync(CustomerId id);
    Task AddCustomerAsync(Customer customer);
    Task UpdateCustomerAsync(Customer customer);
    Task DeleteCustomerAsync(Customer customer);
    
    
}