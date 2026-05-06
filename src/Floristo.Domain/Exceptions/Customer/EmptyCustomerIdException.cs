using Floristo.SharedAbstractions.Exceptions;

namespace Floristo.Domain.Exceptions;

public class EmptyCustomerIdException() : FloristoException("customer id cannot be empty")
{
    
}