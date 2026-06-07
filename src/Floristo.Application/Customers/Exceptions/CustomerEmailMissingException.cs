using Floristo.SharedAbstractions.Exceptions;

namespace Floristo.Domain.Customers.Exceptions;

public class CustomerEmailMissingException() : FloristoException("Cannot create customer profile: authenticated user has no email address")
{
    public override int StatusCode => 422;
}