using Floristo.SharedAbstractions.Exceptions;

namespace Floristo.Domain.Categories.Exceptions;

public class WrongCategoryDescriptionLengthException() : FloristoException("category description cannot exceed 1000 characters")
{
    
}