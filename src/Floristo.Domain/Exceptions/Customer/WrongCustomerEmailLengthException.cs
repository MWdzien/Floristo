using Floristo.SharedAbstractions.Exceptions;

namespace Floristo.Domain.Exceptions;

public class WrongCustomerEmailLengthException() : FloristoException("customer email has to be between 5 and 254 characters long")
{
    
}