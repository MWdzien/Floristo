using Floristo.SharedAbstractions.Exceptions;

namespace Floristo.Domain.Products.Exceptions;

public class WrongProductPriceFormatException() : FloristoException("product price must have at most 2 decimal places");