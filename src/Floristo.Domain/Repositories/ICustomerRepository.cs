using Floristo.Domain.Entities;
using Floristo.Domain.ValueObjects;

namespace Floristo.Domain.Repositories;

public interface ICustomerRepository
{
    Task<Customer?> GetAsync(CustomerId id);
    Task AddAsync(Customer customer);
    Task UpdateAsync(Customer customer);
    Task DeleteAsync(Customer customer);
    
    
}