using Floristo.Domain.Products.Exceptions;

namespace Floristo.Domain.Products.ValueObjects;

public record ProductUnpublishedAt
{
    public DateTime Value { get; }

    public ProductUnpublishedAt(DateTime value)
    {
        if (value > DateTime.UtcNow)
        {
            throw new InvalidProductUnpublishedAtDateException();
        }
        
        Value = value;
    }
    
    public static implicit operator DateTime(ProductUnpublishedAt unpublishedAt) => unpublishedAt.Value;
    public static implicit operator ProductUnpublishedAt(DateTime dateTime) => new(dateTime);
}