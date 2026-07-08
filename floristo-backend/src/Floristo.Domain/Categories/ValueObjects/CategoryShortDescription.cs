using Floristo.Domain.Categories.Exceptions;

namespace Floristo.Domain.Categories.ValueObjects;

public record CategoryShortDescription
{
    public string Value { get; }

    public CategoryShortDescription(string value)
    {
        if (string.IsNullOrWhiteSpace(value))
        {
            throw new EmptyCategoryShortDescriptionException();
        }

        if (value.Length > 200)
        {
            throw new WrongCategoryShortDescriptionLengthException();
        }
        
        Value = value;
    }
    
    public static implicit operator string(CategoryShortDescription description) => description.Value;
    public static implicit operator CategoryShortDescription(string description) => new CategoryShortDescription(description);
}