using Floristo.SharedAbstractions.Exceptions;

namespace Floristo.Domain.Categories.Exceptions;

public class InvalidCategoryDeactivatedAtDateException() : FloristoException("category deactivated at cannot be in the future")
{
    
}