using Floristo.Domain.Products.ValueObjects;
using Floristo.SharedAbstractions.Exceptions;

namespace Floristo.Domain.Products.Exceptions;

public class ProductAlreadyActiveException() : FloristoException("product is already active")
{
    public override int StatusCode => 409;
}