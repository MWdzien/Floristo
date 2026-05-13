using Floristo.SharedAbstractions.Exceptions;

namespace Floristo.Domain.Customers.Exceptions;

public class EmptyCustomerEmailException() : FloristoException("customer email cannot be empty")
{
    
}