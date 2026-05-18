using Floristo.SharedAbstractions.Exceptions;

namespace Floristo.Domain.Categories.Exceptions;

public class InvalidCategoryUnpublishedAtDateException() : FloristoException("category unpublished at cannot be in the future")
{
    
}