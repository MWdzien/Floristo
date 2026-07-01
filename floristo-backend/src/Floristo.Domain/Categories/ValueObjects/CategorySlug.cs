using System.Text.RegularExpressions;
using Floristo.Domain.Categories.Exceptions;

namespace Floristo.Domain.Categories.ValueObjects;

public record CategorySlug
{
    public string Value { get; }

    private static readonly Regex SlugRegex = new(@"^[a-z0-9]+(-[a-z0-9]+)*$", RegexOptions.Compiled);
    
    public CategorySlug(string value)
    {
        if (string.IsNullOrWhiteSpace(value))
        {
            throw new EmptyCategorySlugException();
        }

        if (value.Length is < 3 or > 100)
        {
            throw new WrongCategorySlugLengthException();
        }

        if (!SlugRegex.IsMatch(value))
        {
            throw new WrongCategorySlugFormatException();
        }
        
        Value = value;
    }
    
    public static implicit operator string(CategorySlug slug) => slug.Value;
    public static implicit operator CategorySlug(string slug) => new CategorySlug(slug);
}