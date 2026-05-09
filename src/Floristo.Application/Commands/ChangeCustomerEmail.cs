using Floristo.SharedAbstractions.Commands;

namespace Floristo.Application.Commands;

public record ChangeCustomerEmail(Guid Id, string Email) : ICommand;