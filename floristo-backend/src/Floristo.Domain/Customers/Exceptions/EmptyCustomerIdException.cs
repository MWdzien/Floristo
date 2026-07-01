using Floristo.SharedAbstractions.Exceptions;

namespace Floristo.Domain.Customers.Exceptions;

public class EmptyCustomerIdException() : FloristoException("customer id cannot be empty")
{
    
}