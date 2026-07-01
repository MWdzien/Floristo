using Floristo.SharedAbstractions.Commands;

namespace Floristo.Application.Categories.Commands;

public record PublishCategory(Guid Id) : ICommand;