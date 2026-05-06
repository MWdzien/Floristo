using Floristo.SharedAbstractions.Exceptions;

namespace Floristo.Domain.Exceptions;

public class EmptyCustomerLastNameException() : FloristoException("customer last name cannot be empty")
{
    
}