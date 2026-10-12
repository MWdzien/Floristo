using Floristo.SharedAbstractions.Exceptions;

namespace Floristo.Domain.Carts.Exceptions;

public class InvalidCartProductQuantityException() : FloristoException("Cart Product Quantity has to be greater than 0");