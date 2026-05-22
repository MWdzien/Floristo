using Floristo.SharedAbstractions.Exceptions;

namespace Floristo.Domain.Products.Exceptions.StateChanged;

public class ProductNameNotChangedException() : FloristoException("Product Name was not changed")
{
    public override int StatusCode => 422;
}