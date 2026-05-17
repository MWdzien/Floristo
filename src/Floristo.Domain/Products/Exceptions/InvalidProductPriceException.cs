using Floristo.SharedAbstractions.Exceptions;

namespace Floristo.Domain.Products.Exceptions;

public class InvalidProductPriceException() : FloristoException("product price has to be greater than 0");