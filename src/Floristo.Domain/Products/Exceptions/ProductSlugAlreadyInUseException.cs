using Floristo.SharedAbstractions.Exceptions;

namespace Floristo.Domain.Products.Exceptions;

public class ProductSlugAlreadyInUseException() : FloristoException("product slug is already in use")
{
    public override int StatusCode => 422;
}