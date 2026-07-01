using Floristo.SharedAbstractions.Exceptions;

namespace Floristo.Domain.Customers.Exceptions;

public class CustomerNotActiveException() : FloristoException("cannot modify deactivated customer")
{
    public override int StatusCode => 422;
}