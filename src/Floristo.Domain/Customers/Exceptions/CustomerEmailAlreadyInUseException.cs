using Floristo.SharedAbstractions.Exceptions;

namespace Floristo.Domain.Customers.Exceptions;

public class CustomerEmailAlreadyInUseException() : FloristoException("customers email already in use")
{
    public override int StatusCode => 422;
}