using Floristo.SharedAbstractions.Exceptions;

namespace Floristo.Domain.Products.Exceptions;

public class ProductAlreadyUnpublishedException() : FloristoException("product is already unpublished")
{
    public override int StatusCode => 409;
}