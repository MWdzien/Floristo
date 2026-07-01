using Floristo.SharedAbstractions.Exceptions;

namespace Floristo.Domain.Products.Exceptions;

public class EmptyProductSeoDescriptionException() : FloristoException("product seo description cannot be empty");