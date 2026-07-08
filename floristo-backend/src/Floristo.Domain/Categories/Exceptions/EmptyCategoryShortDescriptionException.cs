using Floristo.SharedAbstractions.Exceptions;

namespace Floristo.Domain.Categories.Exceptions;

public class EmptyCategoryShortDescriptionException() : FloristoException("category short description cannot be empty");