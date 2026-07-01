using Floristo.SharedAbstractions.Exceptions;

namespace Floristo.Domain.Categories.Exceptions;

public class WrongCategoryImageUrlLengthException() : FloristoException("category image url cannot exceed 2048 characters")
{
    
}