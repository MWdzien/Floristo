using Floristo.SharedAbstractions.Exceptions;

namespace Floristo.Domain.Products.Exceptions.Validation.Image;

public class ProductImageLimitExceededException() : FloristoException("Product Image limit exceeded");