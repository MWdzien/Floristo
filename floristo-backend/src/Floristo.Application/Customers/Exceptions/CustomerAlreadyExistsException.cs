using Floristo.SharedAbstractions.Exceptions;

namespace Floristo.Domain.Customers.Exceptions;

public class CustomerAlreadyExistsException(string identityId) : FloristoException($"Customer for identity: {identityId} already exists")
{
    public override int StatusCode => 409;
}