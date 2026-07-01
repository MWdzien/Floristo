using Floristo.Domain.Categories.Exceptions;

namespace Floristo.Domain.Categories.ValueObjects;

public record CategoryName
{
    public string Value { get; }

    public CategoryName(string value)
    {
        if (string.IsNullOrWhiteSpace(value))
        {
            throw new EmptyCategoryNameException();
        }

        if (value.Length is < 3 or > 100)
        {
            throw new WrongCategoryNameLengthException();
        }
        
        Value = value;
    }
    
    public static implicit operator string(CategoryName name) => name.Value;
    public static implicit operator CategoryName(string name) => new CategoryName(name);
}