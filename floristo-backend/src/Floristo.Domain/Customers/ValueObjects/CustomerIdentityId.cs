using Floristo.Domain.Customers.Exceptions;

namespace Floristo.Domain.Customers.ValueObjects;

public record CustomerIdentityId
{
    public string Value { get; }

    public CustomerIdentityId(string value)
    {
        if (string.IsNullOrWhiteSpace(value))
        {
            throw new EmptyCustomerIdentityIdException();
        }

        if (value.Length > 128)
        {
            throw new WrongCustomerIdentityIdLengthException();
        }
        
        Value = value;
    }
    
    public static implicit operator string(CustomerIdentityId identityId) => identityId.Value; 
    public static implicit operator CustomerIdentityId(string identityId) => new(identityId); 
}