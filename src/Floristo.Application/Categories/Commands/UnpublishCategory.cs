using Floristo.SharedAbstractions.Commands;

namespace Floristo.Application.Categories.Commands;

public record UnpublishCategory(Guid Id) : ICommand;