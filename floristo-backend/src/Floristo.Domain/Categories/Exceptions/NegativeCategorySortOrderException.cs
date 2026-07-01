using Floristo.SharedAbstractions.Exceptions;

namespace Floristo.Domain.Categories.Exceptions;

public class NegativeCategorySortOrderException() : FloristoException("category sort order cannot be negative")
{
    
}