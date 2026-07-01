using Floristo.Domain.Customers.Entities;
using Floristo.Domain.Customers.ValueObjects;

namespace Floristo.Domain.Customers.Repositories;

public interface ICustomerRepository
{
    Task<Customer?> GetAsync(CustomerId id);
    Task AddAsync(Customer customer);
    Task UpdateAsync(Customer customer);
}