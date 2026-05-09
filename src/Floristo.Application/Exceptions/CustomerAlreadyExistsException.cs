using Floristo.SharedAbstractions.Exceptions;

namespace Floristo.Application.Exceptions;

public class CustomerAlreadyExistsException(string email) : FloristoException($"customer with email {email} already exists");