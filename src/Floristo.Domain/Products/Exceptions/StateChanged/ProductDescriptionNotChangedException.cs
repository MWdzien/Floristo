using Floristo.SharedAbstractions.Exceptions;

namespace Floristo.Domain.Products.Exceptions.StateChanged;

public class ProductDescriptionNotChangedException() : FloristoException("Product Description was not changed")
{
    public override int StatusCode => 422;
}