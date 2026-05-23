using Floristo.SharedAbstractions.Exceptions;

namespace Floristo.Application.Categories.Exceptions;

public class CategoryHasProductsException(Guid categoryId) : FloristoException($"Category with id '{categoryId}' has products assigned and cannot be deleted");