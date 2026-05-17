using Floristo.SharedAbstractions.Exceptions;

namespace Floristo.Domain.Products.Exceptions;

public class InvalidProductDeactivatedAtDateException() : FloristoException("product deactivated at cannot be in the future");