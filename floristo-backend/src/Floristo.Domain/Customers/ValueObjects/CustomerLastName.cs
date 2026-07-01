using Floristo.Domain.Customers.Exceptions;

namespace Floristo.Domain.Customers.ValueObjects;

public record CustomerLastName
{
    public string Value { get; }

    public CustomerLastName(string value)
    {
        if (string.IsNullOrWhiteSpace(value))
        {
            throw new EmptyCustomerLastNameException();
        }

        if (value.Length < 2 || value.Length > 100)
        {
            throw new WrongCustomerLastNameLengthException();
        }
        
        Value = value;
    }
    
    public static implicit operator string(CustomerLastName lastName) => lastName.Value; 
    public static implicit operator CustomerLastName(string lastName) => new(lastName); 
}