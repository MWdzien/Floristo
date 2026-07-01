using Floristo.SharedAbstractions.Exceptions;

namespace Floristo.Domain.Products.Exceptions.Validation.Image;

public class ProductImageSortOrderAlreadyTakenException() : FloristoException("Product Image Sort Order is already taken");