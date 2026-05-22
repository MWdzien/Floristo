using Floristo.SharedAbstractions.Exceptions;

namespace Floristo.Domain.Products.Exceptions.StateChanged;

public class ProductDimensionsNotChangedException() : FloristoException("Product Dimensions were not changed")
{
    public override int StatusCode => 422;
}