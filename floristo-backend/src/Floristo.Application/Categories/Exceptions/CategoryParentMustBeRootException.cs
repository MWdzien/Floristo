using Floristo.SharedAbstractions.Exceptions;

namespace Floristo.Application.Categories.Exceptions;

public class CategoryParentMustBeRootException(Guid parentId) : FloristoException($"Category {parentId} cannot be used as parent because it is not a root category");