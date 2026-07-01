using Floristo.SharedAbstractions.Exceptions;

namespace Floristo.Domain.Products.Exceptions;

public class WrongProductSlugLengthException() : FloristoException("product slug length must be between 3 and 100 characters");