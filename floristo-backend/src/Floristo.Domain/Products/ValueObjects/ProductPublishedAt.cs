using Floristo.Domain.Products.Exceptions;

namespace Floristo.Domain.Products.ValueObjects;

public record ProductPublishedAt
{
    public DateTime Value { get; }

    public ProductPublishedAt(DateTime value)
    {
        if (value > DateTime.UtcNow)
        {
            throw new InvalidProductPublishedAtDateException();
        }
        
        Value = value;
    }
    
    public static implicit operator DateTime(ProductPublishedAt publishedAt) => publishedAt.Value;
    public static implicit operator ProductPublishedAt(DateTime dateTime) => new(dateTime);
}