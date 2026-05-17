using Floristo.Domain.Products.Exceptions;

namespace Floristo.Domain.Products.ValueObjects;

public record ProductDeactivatedAt
{
    public DateTime Value { get; }

    public ProductDeactivatedAt(DateTime value)
    {
        if (value > DateTime.UtcNow)
        {
            throw new InvalidProductDeactivatedAtDateException();
        }
        
        Value = value;
    }
    
    public static implicit operator DateTime(ProductDeactivatedAt deactivatedAt) => deactivatedAt.Value;
    public static implicit operator ProductDeactivatedAt(DateTime dateTime) => new(dateTime);
}