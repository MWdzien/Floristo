using Floristo.SharedAbstractions.Commands;

namespace Floristo.Application.Categories.Commands;

public record DeleteCategorySeoDescription(Guid Id) : ICommand;