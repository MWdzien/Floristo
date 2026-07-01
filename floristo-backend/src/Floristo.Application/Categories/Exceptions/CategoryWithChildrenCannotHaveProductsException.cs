using Floristo.SharedAbstractions.Exceptions;

namespace Floristo.Application.Categories.Exceptions;

public class CategoryWithChildrenCannotHaveProductsException(Guid categoryId) : 
    FloristoException($"Category {categoryId} cannot have products because it has child categories");