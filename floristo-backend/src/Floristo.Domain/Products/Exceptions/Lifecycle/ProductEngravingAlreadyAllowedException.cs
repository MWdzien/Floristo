using Floristo.SharedAbstractions.Exceptions;

namespace Floristo.Domain.Products.Exceptions.Lifecycle;

public class ProductEngravingAlreadyAllowedException() : FloristoException("This product already has Engraving allowed")
{
    public override int StatusCode => 422;
}