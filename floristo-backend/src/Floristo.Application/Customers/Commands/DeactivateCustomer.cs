using Floristo.SharedAbstractions.Commands;

namespace Floristo.Application.Customers.Commands;

public record DeactivateCustomer(Guid Id) : ICommand;