using Floristo.SharedAbstractions.Commands;

namespace Floristo.Application.Customers.Commands;

public record ChangeCustomerEmail(Guid Id, string Email) : ICommand;