using Floristo.SharedAbstractions.Exceptions;

namespace Floristo.Domain.Products.Exceptions.ProductImage;

public class EmptyProductImageIdException() : FloristoException("product image id cannot be empty");