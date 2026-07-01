using Floristo.SharedAbstractions.Exceptions;

namespace Floristo.Application.Customers.Exceptions;

public class CustomerNotFoundException(Guid Id) : FloristoException($"customer with ID: {Id} does not exist")
{
    public override int StatusCode => 404;
}