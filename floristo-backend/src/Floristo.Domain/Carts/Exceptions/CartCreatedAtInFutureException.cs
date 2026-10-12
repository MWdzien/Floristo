using Floristo.SharedAbstractions.Exceptions;

namespace Floristo.Domain.Carts.Exceptions;

public class CartCreatedAtInFutureException() : FloristoException("Cart Created At cannot be in the future");