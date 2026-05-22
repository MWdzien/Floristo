using Floristo.SharedAbstractions.Exceptions;

namespace Floristo.Domain.Products.Exceptions.StateChanged;

public class ProductSeoTitleNotChangedException() : FloristoException("Product Seo Title was not changed")
{
    public override int StatusCode => 422;
}