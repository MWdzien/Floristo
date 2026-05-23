using Floristo.SharedAbstractions.Exceptions;

namespace Floristo.Application.Products.Exceptions;

public class ProductNotFoundException(Guid id) : FloristoException($"Product with ID: {id} does not exist")
{
    public override int StatusCode => 404;
}