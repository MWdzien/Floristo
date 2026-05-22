using Floristo.SharedAbstractions.Exceptions;

namespace Floristo.Domain.Products.Exceptions.ProductImage;

public class WrongProductImageUrlLengthException() : FloristoException("product image url cannot exceed 2048 characters");