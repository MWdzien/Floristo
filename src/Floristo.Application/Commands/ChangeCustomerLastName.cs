using Floristo.SharedAbstractions.Commands;

namespace Floristo.Application.Commands;

public record ChangeCustomerLastName(Guid Id, string LastName) : ICommand;