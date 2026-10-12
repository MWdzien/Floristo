using Floristo.Domain.Carts.Entities;
using Floristo.Domain.Carts.ValueObjects.Carts;
using Floristo.Domain.Customers.ValueObjects;

namespace Floristo.Domain.Carts.Factories;

public interface ICartFactory
{
    Cart Create(CartId cartId, CustomerId customerId, CartCreatedAt createdAt);
    Cart Restore(CartId cartId, CustomerId customerId, CartCreatedAt createdAt, List<CartProduct> products);
}