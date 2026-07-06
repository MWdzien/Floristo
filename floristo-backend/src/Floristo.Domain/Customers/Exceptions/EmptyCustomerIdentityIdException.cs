using Floristo.SharedAbstractions.Exceptions;

namespace Floristo.Domain.Customers.Exceptions;

public class EmptyCustomerIdentityIdException() : FloristoException("customer identity id cannot be empty");