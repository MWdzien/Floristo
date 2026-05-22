using Floristo.SharedAbstractions.Exceptions;

namespace Floristo.Domain.Products.Exceptions.StateChanged;

public class ProductEngravingAlreadyDisallowedException() : FloristoException("This product already has Engraving disallowed")
{
    public override int StatusCode => 422;
}