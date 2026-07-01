using Floristo.SharedAbstractions.Exceptions;

namespace Floristo.Domain.Products.Exceptions;

public class InvalidProductPublishedAtDateException() : FloristoException("Product Published At cannot be in the future");