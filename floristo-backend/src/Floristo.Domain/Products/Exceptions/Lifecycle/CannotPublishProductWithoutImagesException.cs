using Floristo.SharedAbstractions.Exceptions;

namespace Floristo.Domain.Products.Exceptions;

public class CannotPublishProductWithoutImagesException() : FloristoException("Cannot publish a Product without any Images");