using Floristo.SharedAbstractions.Exceptions;

namespace Floristo.Domain.Categories.Exceptions;

public class WrongCategorySeoDescriptionLengthException() : FloristoException("category seo description cannot exceed 160 characters")
{
    
}