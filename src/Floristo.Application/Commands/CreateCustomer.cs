using Floristo.SharedAbstractions.Commands;

namespace Floristo.Application.Commands;

public record CreateCustomer(Guid Id, string FirstName, string LastName, string Email) : ICommand;