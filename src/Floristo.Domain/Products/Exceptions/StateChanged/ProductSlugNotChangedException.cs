using Floristo.SharedAbstractions.Exceptions;

namespace Floristo.Domain.Products.Exceptions;

public class ProductSlugNotChangedException() : FloristoException("product slug was not changed")
{
    public override int StatusCode => 422;
}