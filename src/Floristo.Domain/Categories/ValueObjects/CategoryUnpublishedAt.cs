using Floristo.Domain.Categories.Exceptions;

namespace Floristo.Domain.Categories.ValueObjects;

public record CategoryUnpublishedAt
{
    public DateTime Value { get; }

    public CategoryUnpublishedAt(DateTime value)
    {
        if (value > DateTime.UtcNow)
        {
            throw new InvalidCategoryUnpublishedAtDateException();
        }
        
        Value = value;
    }
    
    public static implicit operator DateTime(CategoryUnpublishedAt unpublishedAt) => unpublishedAt.Value;
    public static implicit operator CategoryUnpublishedAt(DateTime dateTime) => new(dateTime);
}