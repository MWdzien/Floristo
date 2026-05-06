using Floristo.SharedAbstractions.Exceptions;

namespace Floristo.Domain.Exceptions;

public class WrongCustomerEmailFormatException() : FloristoException("customer email has to have a proper format")
{
    
}