using Floristo.SharedAbstractions.Exceptions;

namespace Floristo.Application.Categories.Exceptions;

public class CategoryHasChildrenException(Guid id) 
    : FloristoException($"Category {id} cannot be deleted because it has children");