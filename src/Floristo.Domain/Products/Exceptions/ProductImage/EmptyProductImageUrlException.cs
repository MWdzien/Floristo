using Floristo.SharedAbstractions.Exceptions;

namespace Floristo.Domain.Products.Exceptions.ProductImage;

public class EmptyProductImageUrlException() : FloristoException("product image url cannot be empty");