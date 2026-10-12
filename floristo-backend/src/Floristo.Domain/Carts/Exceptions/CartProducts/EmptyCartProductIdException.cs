using Floristo.SharedAbstractions.Exceptions;

namespace Floristo.Domain.Carts.Exceptions;

public class EmptyCartProductIdException() : FloristoException("Cart Product Id cannot be empty");