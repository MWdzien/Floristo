using Floristo.SharedAbstractions.Exceptions;

namespace Floristo.Domain.Products.Exceptions;

public class WrongProductNameLengthException() : FloristoException("product name length must be between 3 and 100 characters");