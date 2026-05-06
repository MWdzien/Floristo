using Floristo.SharedAbstractions.Exceptions;

namespace Floristo.Domain.Exceptions;

public class WrongCustomerLastNameLengthException() : FloristoException("customer last name has to be between 2 and 100 characters long")
{
    
}