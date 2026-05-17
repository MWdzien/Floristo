using Floristo.Domain.Products.Exceptions;

namespace Floristo.Domain.Products.ValueObjects;

public class ProductPrice
{
    public decimal Value { get; }

    public ProductPrice(decimal value)
    {
        if (value <= 0)
        {
            throw new InvalidProductPriceException();
        }

        if (value % 0.01m != 0)
        {
            throw new WrongProductPriceFormatException();
        }
        
        Value = value;
    }

    public static implicit operator decimal(ProductPrice price) => price.Value;
    public static implicit operator ProductPrice(decimal price) => new(price);
}