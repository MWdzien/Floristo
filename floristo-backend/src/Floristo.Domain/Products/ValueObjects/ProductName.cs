using Floristo.Domain.Products.Exceptions;

namespace Floristo.Domain.Products.ValueObjects;

public record ProductName
{
    public string Value { get; }

    public ProductName(string value)
    {
        if (string.IsNullOrWhiteSpace(value))
        {
            throw new EmptyProductNameException();
        }

        if (value.Length is < 3 or > 100)
        {
            throw new WrongProductNameLengthException();
        }
        
        Value = value;
    }
    
    public static implicit operator string(ProductName name) => name.Value; 
    public static implicit operator ProductName(string name) => new(name);
}