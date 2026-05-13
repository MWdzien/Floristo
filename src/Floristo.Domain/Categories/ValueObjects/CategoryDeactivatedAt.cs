using Floristo.Domain.Categories.Exceptions;

namespace Floristo.Domain.Categories.ValueObjects;

public record CategoryDeactivatedAt
{
    public DateTime Value { get; }

    public CategoryDeactivatedAt(DateTime value)
    {
        if (value > DateTime.UtcNow)
        {
            throw new InvalidCategoryDeactivatedAtDateException();
        }
        
        Value = value;
    }
    
    public static implicit operator DateTime(CategoryDeactivatedAt deactivatedAt) => deactivatedAt.Value;
    public static implicit operator CategoryDeactivatedAt(DateTime dateTime) => new(dateTime);
}