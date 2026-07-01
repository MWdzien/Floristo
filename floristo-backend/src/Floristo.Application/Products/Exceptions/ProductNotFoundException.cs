using Floristo.SharedAbstractions.Exceptions;

namespace Floristo.Application.Products.Exceptions;

public class ProductNotFoundException : FloristoException
{
    public ProductNotFoundException(Guid id) : base($"Product with ID: {id} does not exist")
    {
    }
    
    public ProductNotFoundException(string slug) : base($"Product with Slug: {slug} does not exist")
    {
    }
    
    public override int StatusCode => 404;
}