using Floristo.Domain.Products.Exceptions;

namespace Floristo.Domain.Products.ValueObjects;

public record ProductSortOrder
{
    public int Value { get; }

    public ProductSortOrder(int value)
    {
        if (value < 0)
        {
            throw new NegativeProductSortOrderException();
        }
        
        Value = value;
    }
    
    public static implicit operator int(ProductSortOrder sortOrder) => sortOrder.Value;
    public static implicit operator ProductSortOrder(int sortOrder) => new ProductSortOrder(sortOrder);
}