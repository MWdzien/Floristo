using Floristo.SharedAbstractions.Exceptions;

namespace Floristo.Domain.Categories.Exceptions;

public class EmptyCategorySeoTitleException() : FloristoException("category seo title cannot be empty")
{
    
}