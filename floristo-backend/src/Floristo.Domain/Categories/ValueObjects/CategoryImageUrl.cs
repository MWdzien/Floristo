using Floristo.Domain.Categories.Exceptions;

namespace Floristo.Domain.Categories.ValueObjects;

public record CategoryImageUrl
{
    public string Value { get; }

    public CategoryImageUrl(string value)
    {
        if (string.IsNullOrWhiteSpace(value))
        {
            throw new EmptyCategoryImageUrlException();
        }

        if (value.Length > 2048)
        {
            throw new WrongCategoryImageUrlLengthException();
        }

        if (!Uri.TryCreate(value, UriKind.Absolute, out var uri)
            || uri.Scheme is not ("http" or "https"))
        {
            throw new WrongCategoryImageUrlFormatException();
        }
        
        Value = value;
    }
    
    public static implicit operator string(CategoryImageUrl imageUrl) => imageUrl.Value;
    public static implicit operator CategoryImageUrl(string imageUrl) => new CategoryImageUrl(imageUrl);
}