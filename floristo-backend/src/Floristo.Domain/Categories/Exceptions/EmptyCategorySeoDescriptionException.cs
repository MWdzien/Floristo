using Floristo.SharedAbstractions.Exceptions;

namespace Floristo.Domain.Categories.Exceptions;

public class EmptyCategorySeoDescriptionException() : FloristoException("category seo description cannot be empty")
{
    
}