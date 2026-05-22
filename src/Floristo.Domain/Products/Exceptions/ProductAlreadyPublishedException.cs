using Floristo.Domain.Products.ValueObjects;
using Floristo.SharedAbstractions.Exceptions;

namespace Floristo.Domain.Products.Exceptions;

public class ProductAlreadyPublishedException() : FloristoException("product is already published")
{
    public override int StatusCode => 409;
}