using Floristo.Domain.Categories.Exceptions;

namespace Floristo.Domain.Categories.ValueObjects;

public record CategoryPublishedAt
{
    public DateTime Value { get; }

    public CategoryPublishedAt(DateTime value)
    {
        if (value > DateTime.UtcNow)
        {
            throw new InvalidCategoryPublishedAtDateException();
        }
        
        Value = value;
    }
    
    public static implicit operator DateTime(CategoryPublishedAt publishedAt) => publishedAt.Value;
    public static implicit operator CategoryPublishedAt(DateTime dateTime) => new(dateTime);
}