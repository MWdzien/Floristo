using System.Text.RegularExpressions;
using Floristo.Domain.Exceptions;

namespace Floristo.Domain.ValueObjects;

public record CustomerEmail
{
    public string Value { get; }

    private static readonly Regex EmailRegex = new(@"^[^@\s]+@[^@\s]+\.[^@\s]+$", RegexOptions.Compiled);

    public CustomerEmail(string value)
    {
        if (string.IsNullOrWhiteSpace(value))
        {
            throw new EmptyCustomerEmailException();
        }
        
        if (value.Length < 5 || value.Length > 254)
        {
            throw new WrongCustomerEmailLengthException();
        }

        if (!EmailRegex.IsMatch(value))
        {
            throw new WrongCustomerEmailFormatException();
        }
        
        Value = value;
    }
    
    public static implicit operator string(CustomerEmail email) => email.Value;
    public static implicit operator CustomerEmail(string email) => new CustomerEmail(email);
}