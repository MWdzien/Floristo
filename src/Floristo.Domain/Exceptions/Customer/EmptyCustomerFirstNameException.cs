using Floristo.SharedAbstractions.Exceptions;

namespace Floristo.Domain.Exceptions;

public class EmptyCustomerFirstNameException() : FloristoException("customers first name cannot be empty");