using Floristo.SharedAbstractions.Exceptions;

namespace Floristo.Domain.Products.Exceptions;

public class ProductSlugNotChangedException() : FloristoException("product slug is the same as previous")
{
    public override int StatusCode => 422;
}