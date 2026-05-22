using Floristo.SharedAbstractions.Exceptions;

namespace Floristo.Domain.Products.Exceptions;

public class EmptyProductSeoTitleException() : FloristoException("product seo title cannot be empty");