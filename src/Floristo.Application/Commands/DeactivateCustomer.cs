using Floristo.SharedAbstractions.Commands;

namespace Floristo.Application.Commands;

public record DeactivateCustomer(Guid Id) : ICommand;