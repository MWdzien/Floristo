using Floristo.SharedAbstractions.Exceptions;

namespace Floristo.Domain.Products.Exceptions;

public class EmptyProductIdException() : FloristoException("product id cannot be empty");