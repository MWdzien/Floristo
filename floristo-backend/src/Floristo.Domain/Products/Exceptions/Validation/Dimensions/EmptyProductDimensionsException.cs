using Floristo.SharedAbstractions.Exceptions;

namespace Floristo.Domain.Products.Exceptions;

public class EmptyProductDimensionsException() : FloristoException("product dimensions must have at least one nonempty dimension");