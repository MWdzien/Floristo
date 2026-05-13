using Floristo.SharedAbstractions.Exceptions;

namespace Floristo.Domain.Categories.Exceptions;

public class WrongCategoryImageUrlFormatException() : FloristoException("category image url must be valid http or https address")
{
    
}