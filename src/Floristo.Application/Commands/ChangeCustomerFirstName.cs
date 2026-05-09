using Floristo.SharedAbstractions.Commands;

namespace Floristo.Application.Commands;

public record ChangeCustomerFirstName(Guid Id, string FirstName) : ICommand;