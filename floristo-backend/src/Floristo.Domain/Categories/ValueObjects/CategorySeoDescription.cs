using Floristo.Domain.Categories.Exceptions;

namespace Floristo.Domain.Categories.ValueObjects;

public record CategorySeoDescription
{
    public string Value { get; }

    public CategorySeoDescription(string value)
    {
        if (string.IsNullOrWhiteSpace(value))
        {
            throw new EmptyCategorySeoDescriptionException();
        }

        if (value.Length > 160)
        {
            throw new WrongCategorySeoDescriptionLengthException();
        }
        
        Value = value;
    }
    
    public static implicit operator string(CategorySeoDescription seoDescription) => seoDescription.Value;
    public static implicit operator CategorySeoDescription(string seoDescription) => new CategorySeoDescription(seoDescription);
}