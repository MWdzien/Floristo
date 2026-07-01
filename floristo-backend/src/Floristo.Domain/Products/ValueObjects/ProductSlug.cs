using System.Text.RegularExpressions;
using Floristo.Domain.Products.Exceptions;

namespace Floristo.Domain.Products.ValueObjects;

public record ProductSlug
{
    public string Value { get; }

    private static readonly Regex SlugRegex = new(@"^[a-z0-9]+(-[a-z0-9]+)*$", RegexOptions.Compiled);
    
    public ProductSlug(string value)
    {
        if (string.IsNullOrWhiteSpace(value))
        {
            throw new EmptyProductSlugException();
        }

        if (value.Length is < 3 or > 100)
        {
            throw new WrongProductSlugLengthException();
        }

        if (!SlugRegex.IsMatch(value))
        {
            throw new WrongProductSlugFormatException();
        }
        
        Value = value;
    }
    
    public static implicit operator string(ProductSlug slug) => slug.Value;
    public static implicit operator ProductSlug(string slug) => new ProductSlug(slug);
}