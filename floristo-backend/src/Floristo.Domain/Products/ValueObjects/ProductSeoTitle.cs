using Floristo.Domain.Products.Exceptions;

namespace Floristo.Domain.Products.ValueObjects;

public record ProductSeoTitle
{
    public string Value { get; }

    public ProductSeoTitle(string value)
    {
        if (string.IsNullOrWhiteSpace(value))
        {
            throw new EmptyProductSeoTitleException();
        }

        if (value.Length is < 3 or > 70)
        {
            throw new WrongProductSeoTitleLengthException();
        }
        
        Value = value;
    }
    
    public static implicit operator string(ProductSeoTitle seoTitle) => seoTitle.Value;
    public static implicit operator ProductSeoTitle(string seoTitle) => new ProductSeoTitle(seoTitle);
}