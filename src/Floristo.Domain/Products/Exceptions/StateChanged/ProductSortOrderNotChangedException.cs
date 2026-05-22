using Floristo.SharedAbstractions.Exceptions;

namespace Floristo.Domain.Products.Exceptions.StateChanged;

public class ProductSortOrderNotChangedException() : FloristoException("Product Sort Order was not changed")
{
    public override int StatusCode => 422;
}