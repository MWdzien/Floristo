using Floristo.Domain.Customers.Exceptions;

namespace Floristo.Domain.Customers.ValueObjects;

public record CustomerFirstName
{
    public string Value { get; }

    public CustomerFirstName(string value)
    {
        if (string.IsNullOrWhiteSpace(value))
        {
            throw new EmptyCustomerFirstNameException();
        }

        if (value.Length < 2 || value.Length > 50)
        {
            throw new WrongCustomerFirstNameLengthException();
        }
        
        Value = value;
    }
    
    public static implicit operator string(CustomerFirstName firstName) => firstName.Value; 
    public static implicit operator CustomerFirstName(string firstName) => new(firstName); 
}