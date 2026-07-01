using Floristo.SharedAbstractions.Exceptions;

namespace Floristo.Domain.Categories.Exceptions;

public class WrongCategorySlugLengthException() : FloristoException("category slug must be between 3 and 100 characters")
{
    
}