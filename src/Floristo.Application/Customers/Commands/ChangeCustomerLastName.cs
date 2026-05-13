using Floristo.SharedAbstractions.Commands;

namespace Floristo.Application.Customers.Commands;

public record ChangeCustomerLastName(Guid Id, string LastName) : ICommand;