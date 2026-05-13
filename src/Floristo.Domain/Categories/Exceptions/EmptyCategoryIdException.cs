using Floristo.SharedAbstractions.Exceptions;

namespace Floristo.Domain.Categories.Exceptions;

public class EmptyCategoryIdException() : FloristoException("category id cannot be empty")
{
    
}