using Floristo.Domain.Exceptions;

namespace Floristo.Domain.ValueObjects;

public record CustomerDeactivatedAt
{
    public DateTime Value { get; }

    public CustomerDeactivatedAt(DateTime value)
    {
        if (value > DateTime.UtcNow)
        {
            throw new InvalidCustomerDeactivatedAtDateException();
        }
        
        Value = value;
    }
    
    public static implicit operator DateTime(CustomerDeactivatedAt deactivatedAt) => deactivatedAt.Value;
    public static implicit operator CustomerDeactivatedAt(DateTime dateTime) => new(dateTime);
}