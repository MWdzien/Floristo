using Floristo.SharedAbstractions.Commands;

namespace Floristo.Application.Categories.Commands;

public record DeleteCategorySeoTitle(Guid Id) : ICommand;