using Floristo.Domain.Products.Exceptions.ProductImage;

namespace Floristo.Domain.Products.ValueObjects.ProductImage;

public record ProductImageId
{
    public Guid Value { get; }

    public ProductImageId(Guid value)
    {
        if (value == Guid.Empty)
        {
            throw new EmptyProductImageIdException();
        }
        
        Value = value;
    }
    
    public static implicit operator Guid(ProductImageId id) => id.Value;
    public static implicit operator ProductImageId(Guid id) => new(id);
}