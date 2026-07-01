using Floristo.SharedAbstractions.Exceptions;

namespace Floristo.Domain.Categories.Exceptions;

public class CannotDeletePublishedCategoryException() : FloristoException("category must be unpublished before deletion");