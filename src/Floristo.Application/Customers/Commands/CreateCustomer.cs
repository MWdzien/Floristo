using Floristo.SharedAbstractions.Commands;

namespace Floristo.Application.Customers.Commands;

public record CreateCustomer(Guid Id, string FirstName, string LastName, string Email) : ICommand;