using Floristo.Domain.Carts.ValueObjects.Carts;
using Floristo.Domain.Products.ValueObjects;
using Floristo.SharedAbstractions.Exceptions;

namespace Floristo.Domain.Carts.Exceptions;

public class CartProductNotFoundException(ProductId productId, CartId cartId) : FloristoException($"Product does not exist in Cart with Id {cartId}")
{
    
}