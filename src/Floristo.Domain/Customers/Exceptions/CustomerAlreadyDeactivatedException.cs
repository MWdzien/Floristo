using Floristo.SharedAbstractions.Exceptions;

namespace Floristo.Domain.Customers.Exceptions;

public class CustomerAlreadyDeactivatedException() : FloristoException("customer is already deactivated")
{
    public override int StatusCode => 409;
}