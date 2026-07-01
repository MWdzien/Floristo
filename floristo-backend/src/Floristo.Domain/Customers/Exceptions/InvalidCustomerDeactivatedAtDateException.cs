using Floristo.SharedAbstractions.Exceptions;

namespace Floristo.Domain.Customers.Exceptions;

public class InvalidCustomerDeactivatedAtDateException() : FloristoException("customers deactivatedAt cannot be in the future")
{
    
}