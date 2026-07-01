using Floristo.SharedAbstractions.Exceptions;

namespace Floristo.Domain.Categories.Exceptions;

public class WrongCategorySeoTitleLengthException() : FloristoException("category seo title cannot exceed 70 characters")
{
    
}