using Floristo.SharedAbstractions.Exceptions;

namespace Floristo.Application.Customers.Exceptions;

public class CustomerEmailNotUniqueException(string email)
    : FloristoException($"customer with email {email} already exists")
{
    public override int StatusCode => 409;
}