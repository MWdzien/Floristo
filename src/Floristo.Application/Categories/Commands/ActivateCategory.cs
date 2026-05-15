using Floristo.SharedAbstractions.Commands;

namespace Floristo.Application.Categories.Commands;

public record ActivateCategory(Guid Id) : ICommand;