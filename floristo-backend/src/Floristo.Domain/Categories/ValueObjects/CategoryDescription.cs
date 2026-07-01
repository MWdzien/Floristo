using Floristo.Domain.Categories.Exceptions;

namespace Floristo.Domain.Categories.ValueObjects;

public record CategoryDescription
{
    public string Value { get; }

    public CategoryDescription(string value)
    {
        if (string.IsNullOrWhiteSpace(value))
        {
            throw new EmptyCategoryDescriptionException();
        }

        if (value.Length > 1000)
        {
            throw new WrongCategoryDescriptionLengthException();
        }
        
        Value = value;
    }
    
    public static implicit operator string(CategoryDescription description) => description.Value;
    public static implicit operator CategoryDescription(string description) => new CategoryDescription(description);
}