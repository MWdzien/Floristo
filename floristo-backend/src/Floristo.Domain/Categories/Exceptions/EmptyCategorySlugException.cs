using Floristo.SharedAbstractions.Exceptions;

namespace Floristo.Domain.Categories.Exceptions;

public class EmptyCategorySlugException() : FloristoException("category slug cannot be empty")
{
    
}