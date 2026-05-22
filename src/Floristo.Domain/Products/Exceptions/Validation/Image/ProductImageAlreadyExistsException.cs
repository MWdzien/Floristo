using Floristo.SharedAbstractions.Exceptions;

namespace Floristo.Domain.Products.Exceptions.Validation.Image;

public class ProductImageAlreadyExistsException() : FloristoException("Product already has this image");