using Floristo.Domain.Products.Exceptions.ProductImage;

namespace Floristo.Domain.Products.ValueObjects.ProductImage;

public record ProductImageUrl
{
    public string Value { get; }

    public ProductImageUrl(string value)
    {
        if (string.IsNullOrWhiteSpace(value))
        {
            throw new EmptyProductImageUrlException();
        }

        if (value.Length > 2048)
        {
            throw new WrongProductImageUrlLengthException();
        }

        if (!Uri.TryCreate(value, UriKind.Absolute, out var uri)
            || uri.Scheme is not ("http" or "https"))
        {
            throw new WrongProductImageUrlFormatException();
        }
        
        Value = value;
    }
    
    public static implicit operator string(ProductImageUrl value) => value.Value;
    public static implicit operator ProductImageUrl(string value) => new(value);
    
}