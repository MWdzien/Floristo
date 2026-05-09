using Floristo.SharedAbstractions.Exceptions;

namespace Floristo.Domain.Exceptions;

public class CustomerAlreadyDeactivatedException() : FloristoException("customer is already deactivated")
{
    
}