using Floristo.SharedAbstractions.Exceptions;

namespace Floristo.Application.Categories.Exceptions;

public class CategoryNotFoundException : FloristoException
{
    public CategoryNotFoundException(Guid id) : base($"category with ID: {id} does not exist")
    {
        
    }
    
    public CategoryNotFoundException(string slug) : base($"category with Slug: {slug} does not exist")
    {
        
    }
    
    public override int StatusCode => 404;
}