using Floristo.SharedAbstractions.Exceptions;

namespace Floristo.Domain.Exceptions;

public class InvalidCustomerDeactivatedAtDateException() : FloristoException("customers deactivatedAt cannot be in the future")
{
    
}