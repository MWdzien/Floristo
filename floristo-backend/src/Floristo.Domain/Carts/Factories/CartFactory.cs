using Floristo.Domain.Carts.Entities;
using Floristo.Domain.Carts.ValueObjects.Carts;
using Floristo.Domain.Customers.ValueObjects;

namespace Floristo.Domain.Carts.Factories;

public sealed class CartFactory : ICartFactory
{
    public Cart Create(CartId cartId, CustomerId customerId, CartCreatedAt createdAt)
        => new(cartId, customerId, createdAt);

    public Cart Restore(CartId cartId, CustomerId customerId, CartCreatedAt createdAt, List<CartProduct> products)
        => new(cartId, customerId, createdAt, products);
}