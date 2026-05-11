using Floristo.SharedAbstractions.Exceptions;

namespace Floristo.Domain.Exceptions.Customer;

public class CustomerEmailAlreadyInUseException() : FloristoException("customers email already in use")
{
    public override int StatusCode => 422;
}