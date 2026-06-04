using Floristo.SharedAbstractions.Commands;

namespace Floristo.Application.Customers.Commands;

public record CreateCustomer(string FirstName, string LastName) : ICommand;