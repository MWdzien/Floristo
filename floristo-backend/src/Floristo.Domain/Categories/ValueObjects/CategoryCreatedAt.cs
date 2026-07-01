using Floristo.Domain.Categories.Exceptions;

namespace Floristo.Domain.Categories.ValueObjects;

public record CategoryCreatedAt
{
    public DateTime Value { get; }

    public CategoryCreatedAt(DateTime value)
    {
        if (value > DateTime.UtcNow)
        {
            throw new CategoryCreatedAtInFutureException();
        }
        
        Value = value;
    }
    
    public static implicit operator DateTime(CategoryCreatedAt createdAt) => createdAt.Value;
    public static implicit operator CategoryCreatedAt(DateTime dateTime) => new(dateTime);
}