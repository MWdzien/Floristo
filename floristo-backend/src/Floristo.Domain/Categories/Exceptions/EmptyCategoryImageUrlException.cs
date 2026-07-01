using Floristo.SharedAbstractions.Exceptions;

namespace Floristo.Domain.Categories.Exceptions;

public class EmptyCategoryImageUrlException() : FloristoException("category image url cannot be empty")
{
    
}