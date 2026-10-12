using Floristo.Domain.Carts.Exceptions;
using Floristo.Domain.Carts.ValueObjects.CartProducts;
using Floristo.Domain.Products.Entities;
using Floristo.Domain.Products.ValueObjects;

namespace Floristo.Domain.Carts.Entities;

public class CartProduct
{
    public ProductId ProductId { get; private set; }
    private CartProductQuantity _quantity;

    private CartProduct()
    {
    }

    internal CartProduct(ProductId productId, CartProductQuantity quantity)
    {
        ProductId = productId;
        _quantity = quantity;
    }

    public void IncreaseQuantity()
        => _quantity++;

    public void SetQuantity(CartProductQuantity quantity)
    {
        if (quantity < 0)
        {
            throw new InvalidCartProductQuantityException();
        }

        _quantity = new(quantity);
    }
}