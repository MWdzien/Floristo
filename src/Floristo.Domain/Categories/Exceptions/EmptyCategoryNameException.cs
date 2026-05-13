using Floristo.SharedAbstractions.Exceptions;

namespace Floristo.Domain.Categories.Exceptions;

public class EmptyCategoryNameException() : FloristoException("category name cannot be empty")
{
    
}