using Floristo.SharedAbstractions.Exceptions;

namespace Floristo.Domain.Products.Exceptions;

public class WrongProductDescriptionLengthException() : FloristoException("product description length must be between 10 and 2000 characters");