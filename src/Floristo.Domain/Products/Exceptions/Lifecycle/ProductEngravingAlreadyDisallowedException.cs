using Floristo.SharedAbstractions.Exceptions;

namespace Floristo.Domain.Products.Exceptions.Lifecycle;

public class ProductEngravingAlreadyDisallowedException() : FloristoException("This product already has Engraving disallowed")
{
    public override int StatusCode => 422;
}