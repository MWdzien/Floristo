using Floristo.SharedAbstractions.Exceptions;

namespace Floristo.Domain.Customers.Exceptions;

public class CustomerAlreadyExistsException(Guid Id) : FloristoException($"Customer with Id: {Id} already exists")
{
    public override int StatusCode => 409;
}