using Floristo.SharedAbstractions.Exceptions;

namespace Floristo.Domain.Customers.Exceptions;

public class WrongCustomerIdentityIdLengthException() : FloristoException("customer identity id cannot exceed 128 characters");