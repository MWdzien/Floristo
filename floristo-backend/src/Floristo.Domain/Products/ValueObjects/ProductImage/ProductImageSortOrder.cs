using Floristo.Domain.Products.Exceptions.ProductImage;

namespace Floristo.Domain.Products.ValueObjects.ProductImage;

public record ProductImageSortOrder
{
    public int Value { get; }

    public ProductImageSortOrder(int value)
    {
        if (value < 0)
        {
            throw new NegativeProductImageSortOrderException();
        }
        
        Value = value;
    }
    
    public static implicit operator int(ProductImageSortOrder order) => order.Value;
    public static implicit operator ProductImageSortOrder(int order) => new(order);
}