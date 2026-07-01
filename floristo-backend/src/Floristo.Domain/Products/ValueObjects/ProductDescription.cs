using Floristo.Domain.Products.Exceptions;

namespace Floristo.Domain.Products.ValueObjects;

public record ProductDescription
{
    public string Value { get; }

    public ProductDescription(string value)
    {
        if (string.IsNullOrWhiteSpace(value))
        {
            throw new EmptyProductDescriptionException();
        }

        if (value.Length is < 10 or > 2000)
        {
            throw new WrongProductDescriptionLengthException();
        }
        
        Value = value;
    }
    
    public static implicit operator string(ProductDescription description) => description.Value;
    public static implicit operator ProductDescription(string description) => new ProductDescription(description);
}