using Floristo.SharedAbstractions.Exceptions;

namespace Floristo.Domain.Products.Exceptions;

public class ProductAlreadyDeactivatedException() : FloristoException("product is already deactivated")
{
    public override int StatusCode => 409;
}