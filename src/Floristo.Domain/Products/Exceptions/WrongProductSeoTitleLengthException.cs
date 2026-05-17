using Floristo.SharedAbstractions.Exceptions;

namespace Floristo.Domain.Products.Exceptions;

public class WrongProductSeoTitleLengthException() : FloristoException("product seo title length must be between 3 and 70 characters");