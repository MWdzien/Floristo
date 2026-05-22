using Floristo.SharedAbstractions.Exceptions;

namespace Floristo.Domain.Products.Exceptions.StateChanged;

public class ProductPriceNotChangedException() : FloristoException("Product Price was not changed")
{
    public override int StatusCode => 422;
}