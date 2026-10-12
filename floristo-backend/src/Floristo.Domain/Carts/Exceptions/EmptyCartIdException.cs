using Floristo.SharedAbstractions.Exceptions;

namespace Floristo.Domain.Carts.Exceptions;

public class EmptyCartIdException() : FloristoException("Cart Id cannot be empty");