using Floristo.Domain.Carts.Exceptions;

namespace Floristo.Domain.Carts.ValueObjects.Carts;

public record CartId
{
    public Guid Value { get; }

    public CartId(Guid value)
    {
        if (value == Guid.Empty)
            throw new EmptyCartIdException();
        
        Value = value;
    }

    public static implicit operator Guid(CartId id) => id.Value;
    public static implicit operator CartId(Guid id) => new(id);
} 