using Floristo.SharedAbstractions.Exceptions;

namespace Floristo.Domain.Products.Exceptions;

public class InvalidProductUnpublishedAtDateException() : FloristoException("product unpublished at cannot be in the future");