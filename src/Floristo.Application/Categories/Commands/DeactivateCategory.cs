using Floristo.SharedAbstractions.Commands;

namespace Floristo.Application.Categories.Commands;

public record DeactivateCategory(Guid Id) : ICommand;