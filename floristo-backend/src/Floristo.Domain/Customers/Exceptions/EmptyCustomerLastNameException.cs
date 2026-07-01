using Floristo.SharedAbstractions.Exceptions;

namespace Floristo.Domain.Customers.Exceptions;

public class EmptyCustomerLastNameException() : FloristoException("customer last name cannot be empty")
{
    
}