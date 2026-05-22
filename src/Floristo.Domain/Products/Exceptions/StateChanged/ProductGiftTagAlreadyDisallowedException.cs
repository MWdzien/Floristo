using Floristo.SharedAbstractions.Exceptions;

namespace Floristo.Domain.Products.Exceptions.StateChanged;

public class ProductGiftTagAlreadyDisallowedException() : FloristoException("This product already has Gift Tag disallowed")
{
    public override int StatusCode => 422;
}