using Floristo.Domain.Exceptions;

namespace Floristo.Domain.ValueObjects;

public record CustomerCreatedAt
{
    public DateTime Value { get; }

    public CustomerCreatedAt(DateTime value)
    {
        if (value > DateTime.UtcNow)
        {
            throw new CustomerCreatedAtInFutureException();
        }
        
        Value = value;
    }
    
    public static implicit operator DateTime(CustomerCreatedAt createdAt) => createdAt.Value;
    public static implicit operator CustomerCreatedAt(DateTime dateTime) => new(dateTime);
}