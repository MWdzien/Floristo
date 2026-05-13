using Floristo.SharedAbstractions.Exceptions;

namespace Floristo.Domain.Categories.Exceptions;

public class EmptyCategoryDescriptionException() : FloristoException("category description cannot be empty")
{
    
}