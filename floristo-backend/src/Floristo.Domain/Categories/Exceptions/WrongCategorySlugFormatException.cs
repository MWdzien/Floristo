using Floristo.SharedAbstractions.Exceptions;

namespace Floristo.Domain.Categories.Exceptions;

public class WrongCategorySlugFormatException() : FloristoException("category slug must contain only lowercase letters, digits, and hyphens")
{
    
}