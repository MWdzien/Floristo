using Floristo.SharedAbstractions.Exceptions;

namespace Floristo.Domain.Products.Exceptions.Lifecycle;

public class ProductGiftTagAlreadyDisallowedException() : FloristoException("This product already has Gift Tag disallowed")
{
    public override int StatusCode => 422;
}