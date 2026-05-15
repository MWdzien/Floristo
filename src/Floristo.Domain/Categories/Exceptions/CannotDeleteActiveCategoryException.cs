using Floristo.SharedAbstractions.Exceptions;

namespace Floristo.Domain.Categories.Exceptions;

public class CannotDeleteActiveCategoryException() : FloristoException("category must be deactivated before deletion");