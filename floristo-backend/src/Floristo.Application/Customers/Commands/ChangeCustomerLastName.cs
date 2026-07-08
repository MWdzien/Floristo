using Floristo.SharedAbstractions.Commands;

namespace Floristo.Application.Customers.Commands;

public record ChangeCustomerLastName(string LastName) : ICommand;