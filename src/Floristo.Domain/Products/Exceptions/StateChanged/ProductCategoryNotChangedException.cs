using Floristo.SharedAbstractions.Exceptions;

namespace Floristo.Domain.Products.Exceptions.StateChanged;

public class ProductCategoryNotChangedException() : FloristoException("Product Category was not changed")
{
    public override int StatusCode => 422;
}