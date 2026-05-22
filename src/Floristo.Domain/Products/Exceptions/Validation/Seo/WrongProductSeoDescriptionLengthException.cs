using Floristo.SharedAbstractions.Exceptions;

namespace Floristo.Domain.Products.Exceptions;

public class WrongProductSeoDescriptionLengthException() : FloristoException("product seo description length must be between 10 and 160 characters");