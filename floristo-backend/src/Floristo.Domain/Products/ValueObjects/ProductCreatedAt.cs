using Floristo.Domain.Products.Exceptions;

namespace Floristo.Domain.Products.ValueObjects;

public record ProductCreatedAt
{
    public DateTime Value { get; }

    public ProductCreatedAt(DateTime value)
    {
        if (value > DateTime.UtcNow)
        {
            throw new ProductCreatedAtInFutureException();
        }
        
        Value = value;
    }
    
    public static implicit operator DateTime(ProductCreatedAt createdAt) => createdAt.Value;
    public static implicit operator ProductCreatedAt(DateTime dateTime) => new(dateTime);
}