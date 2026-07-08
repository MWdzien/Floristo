using Floristo.SharedAbstractions.Commands;

namespace Floristo.Application.Customers.Commands;

public record ChangeCustomerFirstName(string FirstName) : ICommand;