using Floristo.SharedAbstractions.Exceptions;

namespace Floristo.Domain.Categories.Exceptions;

public class InvalidCategoryPublishedAtDateException() : FloristoException("Category Published At cannot be in the future")
{
    
}