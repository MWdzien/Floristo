using Floristo.Domain.Carts.Exceptions;

namespace Floristo.Domain.Carts.ValueObjects.CartProducts;

public record CartProductQuantity
{
    public int Value { get; }

    public CartProductQuantity(int value)
    {
        if (value < 1)
            throw new InvalidCartProductQuantityException();
        
        Value = value;
    }

    public static implicit operator int(CartProductQuantity quantity) => quantity.Value;
    public static implicit operator CartProductQuantity(int quantity) => new(quantity);
}