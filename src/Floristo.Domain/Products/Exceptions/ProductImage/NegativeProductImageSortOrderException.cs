using Floristo.SharedAbstractions.Exceptions;

namespace Floristo.Domain.Products.Exceptions.ProductImage;

public class NegativeProductImageSortOrderException() : FloristoException("product image sort orders has to be greater than 0");