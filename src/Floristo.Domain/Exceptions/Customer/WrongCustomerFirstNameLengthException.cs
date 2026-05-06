using Floristo.SharedAbstractions.Exceptions;

namespace Floristo.Domain.Exceptions;

public class WrongCustomerFirstNameLengthException() : FloristoException("customer first name has to be between 2 and 50 characters long")
{
    
}