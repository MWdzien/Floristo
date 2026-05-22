using Floristo.SharedAbstractions.Exceptions;

namespace Floristo.Domain.Products.Exceptions.ProductImage;

public class WrongProductImageUrlFormatException() : FloristoException("product image url must be valid http or https address");