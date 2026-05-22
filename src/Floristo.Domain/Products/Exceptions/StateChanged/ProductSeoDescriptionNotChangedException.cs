using Floristo.SharedAbstractions.Exceptions;

namespace Floristo.Domain.Products.Exceptions.StateChanged;

public class ProductSeoDescriptionNotChangedException() : FloristoException("Product Seo Description was not changed")
{
    public override int StatusCode => 422;
}