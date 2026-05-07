using Floristo.SharedAbstractions.Exceptions;

namespace Floristo.Domain.Exceptions;

public class CustomerCreatedAtInFutureException() : FloristoException("customer cannot be created in the future")
{
    
}