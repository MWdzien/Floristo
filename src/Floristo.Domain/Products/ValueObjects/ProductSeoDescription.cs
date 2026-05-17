using Floristo.Domain.Products.Exceptions;

namespace Floristo.Domain.Products.ValueObjects;

public record ProductSeoDescription
{
    public string Value { get; }

    public ProductSeoDescription(string value)
    {
        if (string.IsNullOrWhiteSpace(value))
        {
            throw new EmptyProductSeoDescriptionException();
        }

        if (value.Length is < 10 or > 160)
        {
            throw new WrongProductSeoDescriptionLengthException();
        }
        
        Value = value;
    }
    
    public static implicit operator string(ProductSeoDescription seoDescription) => seoDescription.Value;
    public static implicit operator ProductSeoDescription(string seoDescription) => new ProductSeoDescription(seoDescription);
}