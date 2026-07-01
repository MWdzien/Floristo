using Floristo.SharedAbstractions.Exceptions;

namespace Floristo.Domain.Products.Exceptions.Lifecycle;

public class ProductGiftTagAlreadyAllowedException() : FloristoException("This product already has Gift Tag allowed")
{
    public override int StatusCode => 422;
}