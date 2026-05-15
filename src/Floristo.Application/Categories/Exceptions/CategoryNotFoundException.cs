using Floristo.SharedAbstractions.Exceptions;

namespace Floristo.Application.Categories.Exceptions;

public class CategoryNotFoundException(Guid id) : FloristoException($"category with ID: {id} does not exist");