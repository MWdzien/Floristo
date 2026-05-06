using Floristo.SharedAbstractions.Exceptions;

namespace Floristo.Domain.Exceptions;

public class EmptyCustomerEmailException() : FloristoException("customer email cannot be empty")
{
    
}