using Floristo.SharedAbstractions.Exceptions;

namespace Floristo.Domain.Categories.Exceptions;

public class WrongCategoryNameLengthException() : FloristoException("category name must be between 3 and 100 characters")
{
    
}