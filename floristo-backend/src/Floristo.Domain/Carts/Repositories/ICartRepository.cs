using Floristo.Domain.Carts.Entities;
using Floristo.Domain.Customers.ValueObjects;

namespace Floristo.Domain.Carts.Repositories;

public interface ICartRepository
{
    Task<Cart?> GetByCustomerAsync(CustomerId id);
    Task AddAsync(Cart cart);
    Task UpdateAsync(Cart cart);
}