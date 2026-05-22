using Floristo.SharedAbstractions.Exceptions;

namespace Floristo.Domain.Products.Exceptions;

public class ProductCreatedAtInFutureException() : FloristoException("product created at cannot be in the future");