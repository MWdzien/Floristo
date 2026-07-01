using Floristo.Domain.Categories.Exceptions;

namespace Floristo.Domain.Categories.ValueObjects;

public record CategorySeoTitle
{
    public string Value { get; }

    public CategorySeoTitle(string value)
    {
        if (string.IsNullOrWhiteSpace(value))
        {
            throw new EmptyCategorySeoTitleException();
        }

        if (value.Length > 70)
        {
            throw new WrongCategorySeoTitleLengthException();
        }
        
        Value = value;
    }
    
    public static implicit operator string(CategorySeoTitle seoTitle) => seoTitle.Value;
    public static implicit operator CategorySeoTitle(string seoTitle) => new CategorySeoTitle(seoTitle);
}