using Floristo.SharedAbstractions.Exceptions;

namespace Floristo.Domain.Products.Exceptions;

public class CannotDeleteActiveProductException() : FloristoException("product must be deactivated before deletion")
{
    public override int StatusCode => 409;
}