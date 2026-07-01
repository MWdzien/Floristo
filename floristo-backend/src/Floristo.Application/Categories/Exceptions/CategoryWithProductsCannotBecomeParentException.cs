using Floristo.SharedAbstractions.Exceptions;

namespace Floristo.Application.Categories.Exceptions;

public class CategoryWithProductsCannotBecomeParentException(Guid parentId) 
    : FloristoException($"Category {parentId} cannot be used as parent because it has products");