using Floristo.Domain.Customers.ValueObjects;

namespace Floristo.Domain.Customers.Repositories;

public interface ICustomerRepository
{
    Task<Entities.Customer?> GetAsync(CustomerId id);
    Task AddAsync(Entities.Customer customer);
    Task UpdateAsync(Entities.Customer customer);
    Task DeleteAsync(Entities.Customer customer);
    
    
}