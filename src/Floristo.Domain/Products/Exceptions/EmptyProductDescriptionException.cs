using Floristo.SharedAbstractions.Exceptions;

namespace Floristo.Domain.Products.Exceptions;

public class EmptyProductDescriptionException() : FloristoException("product description cannot be empty");