using Floristo.SharedAbstractions.Exceptions;

namespace Floristo.Domain.Products.Exceptions;

public class CannotDeletePublishedProductException() : FloristoException("Product has to be unpublished before deletion");