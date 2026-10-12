using Floristo.Domain.Carts.Exceptions;

namespace Floristo.Domain.Carts.ValueObjects.Carts;

public record CartCreatedAt
{
    public DateTime Value { get; }

    public CartCreatedAt(DateTime value)
    {
        if (value > DateTime.Now)
        {
            throw new CartCreatedAtInFutureException();
        }
        
        Value = value;
    }

    public static implicit operator DateTime(CartCreatedAt createdAt) => createdAt.Value;
    public static implicit operator CartCreatedAt(DateTime createdAt) => new(createdAt);
    
}