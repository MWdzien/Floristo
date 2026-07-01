using Floristo.SharedAbstractions.Exceptions;

namespace Floristo.Domain.Products.Exceptions;

public class WrongProductSlugFormatException() : FloristoException("product slug must contain only lowercase letters, digits, and hyphens");