using Floristo.SharedAbstractions.Exceptions;

namespace Floristo.Domain.Categories.Exceptions;

public class CategoryCreatedAtInFutureException() : FloristoException("category created at cannot be in the future")
{
    
}