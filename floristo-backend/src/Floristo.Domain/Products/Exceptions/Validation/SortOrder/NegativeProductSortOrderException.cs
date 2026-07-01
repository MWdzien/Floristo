using Floristo.SharedAbstractions.Exceptions;

namespace Floristo.Domain.Products.Exceptions;

public class NegativeProductSortOrderException() : FloristoException("product sort order cannot be negative");