using Floristo.SharedAbstractions.Exceptions;

namespace Floristo.Domain.Categories.Exceptions;

public class WrongCategoryShortDescriptionLengthException() : FloristoException("category description cannot exceed 200 characters");