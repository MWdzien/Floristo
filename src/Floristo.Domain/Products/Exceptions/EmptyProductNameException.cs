using Floristo.SharedAbstractions.Exceptions;

namespace Floristo.Domain.Products.Exceptions;

public class EmptyProductNameException() : FloristoException("product name cannot be empty");